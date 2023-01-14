using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableShieldsOnHullDamagedCritical : MonoBehaviour
{
    //Declarations
    private ShieldDisabler _shieldDisablerRef;
    private IntegrityBehavior _hullIntegrityRef;
    private IntegrityThresholdEvaluator _hullThresholdEvaluatorRef;


    //monos
    private void Awake()
    {
        InitializeReferences();
    }


    //Utilities
    private void InitializeReferences()
    {
        //Get shield disabler
        _shieldDisablerRef = transform.parent.GetComponent<ShipSystemReferencer>().GetShieldsObject().GetComponent<ShieldDisabler>();

        _hullIntegrityRef = GetComponent<IntegrityBehavior>();
    }

    public void DisableShieldsIfIntegrityCritical(float value)
    {
        if (_hullThresholdEvaluatorRef!= null)
        {
            if (_hullThresholdEvaluatorRef.IsIntegrityCritical())
                DisableShields();
        }
        
    }

    private void DisableShields()
    {
        if (_shieldDisablerRef != null)
            _shieldDisablerRef.DisableShieldRegeneration();
    }

}
