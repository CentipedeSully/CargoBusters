using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTargetBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject _target;
    [SerializeField] private bool _isEngagingTarget = false;
    [SerializeField] private Vector2 _calculatedInput;
    [SerializeField] private float _calculatedTurnInput = 0;
    [SerializeField] private float _tooFarFromTargetDistance = 15;
    [SerializeField] private float _tooCloseToTargetDistance = 9;
    [SerializeField] private Vector3 _forwardsDirection = Vector3.up;

    //references
    [SerializeField] private EnginesSystemController _engineControllerRef;


    //Monobehaviors
    private void Update()
    {
        ApproachTarget();
    }


    //Utils
    private void ApproachTarget()
    {
        if (_isEngagingTarget && _target != null)
        {
            CalculateEnemyInputBasedOnRelativePlayerLocation();
            UpdateEnginesController();
        }
        else if (_isEngagingTarget && _target == null)
            DisengageTarget();
    }

    private void CalculateEnemyInputBasedOnRelativePlayerLocation()
    {
        if (_target != null)
        {
            //Draw Line forwards
            Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(_forwardsDirection * 5));
            Vector3 shipForwardsDirection = transform.TransformPoint(_forwardsDirection * 5) - transform.position;
            //Debug.Log("Forwards Direction: " + shipForwardsDirection);

            //Draw Line to target
            Debug.DrawLine(transform.position, _target.transform.position, Color.red);
            Vector3 vectorToTargetLocal = _target.transform.position - transform.position;
            //Debug.Log("Target's Direction: " + vectorToTargetLocal);

            //Calculate diff btwn local forwards direction and target direction
            float angularDifference = Vector3.SignedAngle(vectorToTargetLocal, shipForwardsDirection, Vector3.forward);
            //Debug.Log("angular Difference" + anglularDifference);

            //Set turn input based on signed angular difference
            if (angularDifference > 0)
                _calculatedTurnInput = -1;
            else if (angularDifference < 0)
                _calculatedTurnInput = 1;
            else _calculatedTurnInput = 0;

            //move forwards if target isnt in range
            if (vectorToTargetLocal.magnitude >= _tooFarFromTargetDistance)
                _calculatedInput.y = 1;
            else if (vectorToTargetLocal.magnitude <= _tooCloseToTargetDistance)
                _calculatedInput.y = -1;
            else _calculatedInput.y = 0;

        }
    }

    private void UpdateEnginesController()
    {
        _engineControllerRef.SetMoveInput(_calculatedInput);
        _engineControllerRef.SetTurnInput(_calculatedTurnInput);
    }


    //External Control Utils
    public void EngageTarget(GameObject target)
    {
        if (target != null)
        {
            _target = target;
            _isEngagingTarget = true;
        }
    }

    public void DisengageTarget()
    {
        _isEngagingTarget = false;
        _target = null;
        _engineControllerRef.SetMoveInput(Vector2.zero);
        _engineControllerRef.SetTurnInput(0);
    }


    //Getters & Setters
    public bool IsEngagingTarget()
    {
        return _isEngagingTarget;
    }

    public GameObject GetCurrentTarget()
    {
        return _target;
    }
}
