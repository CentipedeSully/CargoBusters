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

    [SerializeField] private bool _quarterTickReached = false;
    [SerializeField] private bool _halfTickReached = false;
    [SerializeField] private bool _threeQuartersTickReached = false;
    [SerializeField] private bool _finalTickReached = false;


    [Header("Events")]
    public UnityEvent OnBustStarted;
    public UnityEvent OnBustInterrupted;
    public UnityEvent OnBustProgressUpdateTick;
    public UnityEvent OnBustCompleted;


    //Monos
    private void Start()
    {
        _shipID = transform.parent.parent.GetComponent<ShipInformation>().GetShipID();
    }

    private void Update()
    {
        FindTarget();
        BustTargetCargoSystemOnInput();        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _busterRadius);
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
                TickProgressOnQuarterlyThresholdReached();
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

    private void TickProgressOnQuarterlyThresholdReached()
    {
        int normalizedProgress = (int)(_currentBustProgress / _bustDurationMax * 100);
        //Debug.Log(normalizedProgress);

        if (normalizedProgress == 25 && _quarterTickReached == false)
        {
            _quarterTickReached = true;
            OnBustProgressUpdateTick?.Invoke();
        }
        else if (normalizedProgress == 50 && _halfTickReached == false)
        {
            _halfTickReached = true;
            OnBustProgressUpdateTick?.Invoke();
        }
        else if (normalizedProgress == 75 && _threeQuartersTickReached == false)
        {
            _threeQuartersTickReached = true;
            OnBustProgressUpdateTick?.Invoke();
        }
        else if (normalizedProgress == 100 && _finalTickReached == false)
        {
            _finalTickReached = true;
            OnBustProgressUpdateTick?.Invoke();
        }
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

        _quarterTickReached = false;
        _halfTickReached = false;
        _threeQuartersTickReached = false;
        _finalTickReached = false;
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
