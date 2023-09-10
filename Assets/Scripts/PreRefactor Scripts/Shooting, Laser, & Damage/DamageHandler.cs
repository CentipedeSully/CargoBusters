using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageHandler: MonoBehaviour
{
    //Declarations
    private bool _shieldsExist = false;

    //references
    private ShipSystemReferencer _shipSystemReferencer;
    private ShieldsSystemController _shieldsSystemControllerRef;
    private HullSystemController _hullSystemControllerRef;


    //monos
    private void Awake()
    {
        _shipSystemReferencer = GetComponent<ShipSystemReferencer>();
        _hullSystemControllerRef = _shipSystemReferencer.GetHullObject().GetComponent<HullSystemController>();


        //Cache shields info if shields exist
        if (_shipSystemReferencer.GetShieldsObject() != null)
        {
            _shieldsExist = true;
            _shieldsSystemControllerRef = _shipSystemReferencer.GetShieldsObject().GetComponent<ShieldsSystemController>();
        }
            
    }


    //Utilities
    public void ProcessDamage(int value)
    {
        if (_shieldsExist)
        {
            if (_shieldsSystemControllerRef.IsShieldsOnline())
                _shieldsSystemControllerRef.DamageShields(value);

            else
                AttemptToNegateKillIfDamageIsTooMuch(value);

        }

        else
            AttemptToNegateKillIfDamageIsTooMuch(value);
    }

    private void AttemptToNegateKillIfDamageIsTooMuch(int damage)
    {
        if (damage >= _hullSystemControllerRef.GetComponent<IntegrityBehavior>().GetCurrentIntegrity() && GetComponent<ShipInformation>().IsPlayer() == false)
        {
            if (KillNegater.Instance.IsKillNegatedRollSuccessful())
            {
                _hullSystemControllerRef.DamageHull(FindMinimalDamage(damage));
            }
            else _hullSystemControllerRef.DamageHull(damage);
        }

        else _hullSystemControllerRef.DamageHull(damage);

    }

    private int FindMinimalDamage(int damage)
    {
        int currentHull = (int)_hullSystemControllerRef.GetComponent<IntegrityBehavior>().GetCurrentIntegrity();
        int damageMitigationModifier = 0;

        while (currentHull - (damage - damageMitigationModifier) <= 0)
        {
            damageMitigationModifier++;
        }
        return damage - damageMitigationModifier;
    }
}
