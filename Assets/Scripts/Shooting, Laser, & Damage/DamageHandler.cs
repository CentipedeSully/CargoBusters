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

            else _hullSystemControllerRef.DamageHull(value);
        }

        else _hullSystemControllerRef.DamageHull(value);
    }

}
