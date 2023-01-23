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
    [SerializeField] private float _absoluteTargetDistance = Mathf.Infinity;
    [SerializeField] private float _busterRadius = 2;
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
    /*
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
    */

    private void Update()
    {
        FindTarget();
        BustTargetCargoSystemOnInput();        
    }


    //Utilites
    private void FindTarget()
    {
        //OverlapCircle
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, _busterRadius);

        //if target NOT set, then Save the first Valid targetShip, if one exists
        if (_isTargetAvailable == false)
        {
            foreach (Collider2D  detectedCollider in collidersInRange)
            {
                if (IsObjectTargetable(detectedCollider.gameObject))
                    SetTarget(detectedCollider.gameObject);
            }
        }

        //else if target out of range, reset targeting data
        else
        {
            CalculateAbsoluteDistanceFromTarget();
            if (_absoluteTargetDistance > _busterRadius)
                ResetTargeter();
        }
    }

    private bool IsObjectTargetable(GameObject possibleTarget)
    {
        ShipInformation targetsShipInfoRef = possibleTarget.GetComponent<ShipInformation>();
        int selfShipID = transform.parent.parent.GetComponent<ShipInformation>().GetShipID();

        //is this object a ship?
        if (targetsShipInfoRef == null)
            return false;

        else
        {
            CargoSystemController targetsCargoController = possibleTarget.GetComponent<ShipSystemReferencer>().GetCargoObject().GetComponent<CargoSystemController>();

            //if the target isn't the buster's ship itself, and if the cargo sysytem is offline and isn't busted yet
            if (targetsShipInfoRef.GetShipID() != selfShipID && !targetsCargoController.IsCargoSecuritySystemOnline() && !targetsCargoController.IsCargoBusted())
                return true;

            else return false;
        }
    }

    private void SetTarget(GameObject shipObject)
    {
        if (shipObject == null)
            Debug.LogError("CargoBusterERROR: Tried to set target to null value");
        else
        {
            _isTargetAvailable = true;
            _targetShip = shipObject;
            _targetShipID = shipObject.GetComponent<ShipInformation>().GetShipID();
            CalculateAbsoluteDistanceFromTarget();
        }
    }

    private void CalculateAbsoluteDistanceFromTarget()
    {
        _absoluteTargetDistance = Mathf.Abs(Vector2.Distance(_targetShip.transform.position, transform.position));
    }

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
        _absoluteTargetDistance = Mathf.Infinity;
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
