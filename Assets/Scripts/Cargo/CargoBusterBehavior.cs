using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CargoBusterBehavior : MonoBehaviour
{
    //Delcarations
    [SerializeField] private bool _isBusterOnline = true;
    [SerializeField] private bool _isBusterReady = true;
    [SerializeField] private float _busterCooldown = .5f;
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
    [SerializeField] private int _tickPercentThreshold = 8;
    private int _ticksPassed;


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
        if (_isBusterReady && _isBusterOnline)
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
                {
                    SetTarget(detectedCollider.gameObject);
                    if (transform.parent.parent.GetComponent<ShipInformation>().IsPlayer())
                        _targetShip.GetComponent<ShipSystemReferencer>().GetBusterReticuleController().ShowReticule();
                }
                    
            }
        }

        //else if target out of range, reset targeting data
        else
        {
            CalculateAbsoluteDistanceFromTarget();
            if (_absoluteTargetDistance > _busterRadius)
            {
                if (transform.parent.parent.GetComponent<ShipInformation>().IsPlayer())
                    _targetShip.GetComponent<ShipSystemReferencer>().GetBusterReticuleController().HideReticule();
                ResetTargeter();
            }
                
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
        if (_isBusterOnline && _isBusterReady && _bustCommand && _isTargetAvailable)
        {
            if  (_isBustingInProgress == false)
            {
                OnBustStarted?.Invoke();
                _isBustingInProgress = true;
            }
            else
            {
                TickProgressOnThresholdReached();
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

    private void TickProgressOnThresholdReached()
    {
        int normalizedProgress = (int)(_currentBustProgress / _bustDurationMax * 100);
        //Debug.Log("Bust Progress: " + normalizedProgress + ", Ticks Passed: " + _ticksPassed);

        if (normalizedProgress == _tickPercentThreshold * _ticksPassed)
        {
            OnBustProgressUpdateTick?.Invoke();
            _ticksPassed++;
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
        //bust the target's cargo
        CargoSystemController targetsCargoSystem = _targetShip.GetComponent<ShipSystemReferencer>().GetCargoObject().GetComponent<CargoSystemController>();
        targetsCargoSystem.BustCargo();

        //Make Loot drop
        CargoLootDropper.Instance.DropItemsToPlayerInventory();

        if (transform.parent.parent.GetComponent<ShipInformation>().IsPlayer())
            _targetShip.GetComponent<ShipSystemReferencer>().GetBusterReticuleController().HideReticule();
        ResetTargeter();


        CooldownBuster();
        OnBustCompleted?.Invoke();
    }

    private void EndBusting()
    {
        _isBustingInProgress = false;
        _currentBustProgress = 0;
        _ticksPassed = 0;
    }

    private void CooldownBuster()
    {
        _isBusterReady = false;
        Invoke("ReadyBuster", _busterCooldown);
    }

    private void ReadyBuster()
    {
        _isBusterReady = true;
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

    public float GetBusterDuration()
    {
        return _bustDurationMax;
    }

    public void SetBusterDuration(float value)
    {
        _bustDurationMax = value;
    }


}
