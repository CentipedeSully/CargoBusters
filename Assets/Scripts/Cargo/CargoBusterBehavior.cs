using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CargoBusterBehavior : MonoBehaviour
{
    //Delcarations
    [SerializeField] private bool _isBusterOnline = true;
    [SerializeField] private bool _isBustingInProgress = false;
    [SerializeField] private bool _isTargetAvailable = false;
    private GameObject _targetShip;
    [SerializeField] private int _targetShipID;
    [SerializeField] private float _bustDurationMax = 5;
    [SerializeField] private float _currentBustProgress = 0;
    [SerializeField] private bool _bustCommand = false;
    private int _shipID;

    [Header("Events")]
    public UnityEvent OnBustStarted;
    public UnityEvent OnBustInterrupted;
    public UnityEvent OnBustCompleted;


    //Monos
    private void Start()
    {
        _shipID = transform.parent.parent.GetComponent<ShipInformation>().GetShipID();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTargetAvailable == false)
        {
            if (collision.gameObject.CompareTag("Ship") && collision.gameObject.GetComponent<ShipInformation>().GetShipID() != _shipID)
            {
                //If the target's cargo is offline
                CargoSystemController targetsCargoSystems = collision.gameObject.GetComponent<ShipSystemReferencer>().GetCargoObject().GetComponent<CargoSystemController>();
                if (targetsCargoSystems.IsCargoSecuritySystemOnline() == false && targetsCargoSystems.IsCargoBusted() == false)
                {
                    _targetShipID = collision.gameObject.GetComponent<ShipInformation>().GetShipID();
                    _targetShip = collision.gameObject;
                    _isTargetAvailable = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            if (_isTargetAvailable && _targetShipID == collision.gameObject.GetComponent<ShipInformation>().GetShipID())
                ResetTargeter();
        }
    }

    private void Update()
    {
        BustTargetCargoSystemOnInput();        
    }


    //Utilites
    private void BustTargetCargoSystemOnInput()
    {
        if (_isBusterOnline && _bustCommand && _isTargetAvailable)
        {
            if  (_isBustingInProgress == false)
            {
                OnBustStarted?.Invoke();
                _isBustingInProgress = true;
            }
            else
            {
                _currentBustProgress += Time.deltaTime;

                if (_currentBustProgress >= _bustDurationMax)
                {
                    CompleteBust();
                    ResetTargeter();
                    EndBusting();
                }
            }
        }

        else if (_isBustingInProgress)
            InterruptBust();
    }

    private void ResetTargeter()
    {
        _isTargetAvailable = false;
        _targetShip = null;
        _targetShipID = 0;
    }

    private void CompleteBust()
    {
        CargoSystemController targetsCargoSystem = _targetShip.GetComponent<ShipSystemReferencer>().GetCargoObject().GetComponent<CargoSystemController>();
        targetsCargoSystem.BustCargo();
        OnBustCompleted?.Invoke();
    }

    private void EndBusting()
    {
        _isBustingInProgress = false;
        _currentBustProgress = 0;
    }

    //Externals
    public void InterruptBust()
    {
        EndBusting();
        OnBustInterrupted?.Invoke();
    }

    public void DisableBuster()
    {
        _isBusterOnline = false;
    }

    public void EnableBuster()
    {
        _isBusterOnline = true;
    }

    //Getters & Setters
    public void SetBustCommand(bool value)
    {
        _bustCommand = value;
    }


}
