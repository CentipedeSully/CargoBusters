using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrusterController : MonoBehaviour
{
    private ApproachPlayerBehavior _approachPlayerScriptRef;
    private ThrusterToggler _thrusterTogglerRef;



    private void Awake()
    {
        _approachPlayerScriptRef = GetComponent<ApproachPlayerBehavior>();
        _thrusterTogglerRef = GetComponent<ThrusterToggler>();
    }


    private void Update()
    {
        ControlThrusters();
    }




    private void ControlThrusters()
    {
        if (_approachPlayerScriptRef.GetShipInput().y > 0)
            _thrusterTogglerRef.ActivateForwardsThrusters();
        else _thrusterTogglerRef.DeactivateForwardsThrusters();
    }
}
