using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    //Declarations
    private ThrusterToggler _thrusterTogglerReference;






    //Monobehaviors
    private void Awake()
    {
        _thrusterTogglerReference = GetComponent<ThrusterToggler>();
    }

    private void Update()
    {
        FireThrustersBasedOnMoveInput();
    }





    //Utilities
    private void FireThrustersBasedOnMoveInput()
    {
        if (InputDetector.Instance.GetMoveInput().x > 0)
        {
            _thrusterTogglerReference.ActivateLeftStrafeThrusters();
            _thrusterTogglerReference.DeactivateRightStrafeThrusters();
        }
            
        else if (InputDetector.Instance.GetMoveInput().x < 0)
        {
            _thrusterTogglerReference.ActivateRightStrafeThrusters();
            _thrusterTogglerReference.DeactivateLeftStrafeThrusters();
        }

        else
        {
            _thrusterTogglerReference.DeactivateLeftStrafeThrusters();
            _thrusterTogglerReference.DeactivateRightStrafeThrusters();
        }
            

        if (InputDetector.Instance.GetMoveInput().y > 0)
        {
            _thrusterTogglerReference.ActivateForwardsThrusters();
            _thrusterTogglerReference.DeactivateReverseThrusters();
        }
        else if (InputDetector.Instance.GetMoveInput().y < 0)
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
