using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    //Declarations
    private ThrusterToggler _thrusterTogglerReference;
    private Vector2 _moveDirection = Vector2.zero;


    //Monobehaviors
    private void Awake()
    {
        _thrusterTogglerReference = GetComponent<ThrusterToggler>();
    }

    private void Update()
    {
        FireThrustersBasedOnMoveDirection();
    }


    //Utilities
    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }

    public void SetMoveDirection(Vector2 newDirection)
    {
        _moveDirection = newDirection;
    }

    private void FireThrustersBasedOnMoveDirection()
    {
        if (_moveDirection.x > 0)
        {
            _thrusterTogglerReference.ActivateLeftStrafeThrusters();
            _thrusterTogglerReference.DeactivateRightStrafeThrusters();
        }

        else if (_moveDirection.x < 0)
        {
            _thrusterTogglerReference.ActivateRightStrafeThrusters();
            _thrusterTogglerReference.DeactivateLeftStrafeThrusters();
        }

        else
        {
            _thrusterTogglerReference.DeactivateLeftStrafeThrusters();
            _thrusterTogglerReference.DeactivateRightStrafeThrusters();
        }


        if (_moveDirection.y > 0)
        {
            _thrusterTogglerReference.ActivateForwardsThrusters();
            _thrusterTogglerReference.DeactivateReverseThrusters();
        }
        else if (_moveDirection.y < 0)
        {
            _thrusterTogglerReference.ActivateReverseThrusters();
            _thrusterTogglerReference.DeactivateForwardsThrusters();
        }
        else
        {
            _thrusterTogglerReference.DeactivateForwardsThrusters();
            _thrusterTogglerReference.DeactivateReverseThrusters();
        }
    }
}
