using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrusterController : MonoBehaviour
{
    private ApproachTargetBehavior _approachPlayerScriptRef;
    private OldThrusterToggler _thrusterTogglerRef;



    private void Awake()
    {
        _approachPlayerScriptRef = GetComponent<ApproachTargetBehavior>();
        _thrusterTogglerRef = GetComponent<OldThrusterToggler>();
    }


    private void Update()
    {
        //ControlThrusters();
    }


    //private void ControlThrusters()
    //{
    //    if (_approachPlayerScriptRef.GetShipInput().y > 0)
    //        _thrusterTogglerRef.ActivateForwardsThrusters();
    //    else _thrusterTogglerRef.DeactivateForwardsThrusters();
    //}
}
