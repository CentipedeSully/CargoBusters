using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSystemsOnHullCritical: MonoBehaviour
{
    //Declarations
    private Regenerator _shieldRegeneratorRef;
    private IntegrityBehavior _hullIntegrityRef;
    private IntegrityThresholdEvaluator _hullThresholdEvaluatorRef;


    //monos
    private void Start()
    {
        InitializeReferences();
    }


    //Utilities
    private void InitializeReferences()
    {
        _shieldRegeneratorRef = transform.parent.GetComponent<ShipSystemReferencer>().GetShieldsObject().GetComponent<Regenerator>();
        _hullThresholdEvaluatorRef = GetComponent<IntegrityThresholdEvaluator>();
        _hullIntegrityRef = GetComponent<IntegrityBehavior>();
    }

    public void DisableSystemsIfIntegrityCritical(float value)
    {
        if (_hullThresholdEvaluatorRef!= null)
        {
            if (_hullThresholdEvaluatorRef.IsIntegrityCritical())
            {
                transform.parent.GetComponent<ShipSystemReferencer>().GetShipInfo().DisableShip();
                DisableShields();

            }
               
        }
        
    }

    private void DisableShields()
    {
        if (_shieldRegeneratorRef != null)
            _shieldRegeneratorRef.SetRegenEnabled(false);
    }

}
