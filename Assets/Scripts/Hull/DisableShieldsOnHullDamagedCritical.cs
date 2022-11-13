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

    private void OnEnable()
    {
        _hullIntegrityRef.OnIntegrityDecreased += DisableShieldsIfIntegrityCritical;
    }

    private void OnDisable()
    {
        _hullIntegrityRef.OnIntegrityDecreased -= DisableShieldsIfIntegrityCritical;
    }


    //Utilities
    private void InitializeReferences()
    {
        //Get shield disabler
        _shieldDisablerRef = transform.parent.GetComponent<ShipSystemReferencer>().GetShieldsObject().GetComponent<ShieldDisabler>();

        _hullIntegrityRef = GetComponent<IntegrityBehavior>();
    }

    private void DisableShieldsIfIntegrityCritical(float value)
    {
        if (_hullThresholdEvaluatorRef.IsIntegrityCritical())
            DisableShields();
    }

    private void DisableShields()
    {
        _shieldDisablerRef.DisableShieldRegeneration();
    }

}
