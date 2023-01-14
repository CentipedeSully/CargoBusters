using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageHandler: MonoBehaviour
{
    //Declarations
    private ShipSystemReferencer _shipSystemReferencer;


    //monos
    private void Awake()
    {
        _shipSystemReferencer = GetComponent<ShipSystemReferencer>();
    }


    //Utilities
    public void ProcessDamage(float value)
    {
        //if shields available, then damage shields
        if (GetShieldIntegrityRef().GetCurrentIntegrity() > 0)
            GetShieldIntegrityRef().DecreaseIntegrity(value);

        //else damage hull
        else if (GetHullIntegrityRef().GetCurrentIntegrity() > 0)
            GetHullIntegrityRef().DecreaseIntegrity(value);

    }

    private IntegrityBehavior GetShieldIntegrityRef()
    {
        return _shipSystemReferencer.GetShieldsObject().GetComponent<IntegrityBehavior>();
    }

    private IntegrityBehavior GetHullIntegrityRef()
    {
        return _shipSystemReferencer.GetHullObject().GetComponent<IntegrityBehavior>();
    }

}
