using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptShieldRegenDelayOnHullDamaged : MonoBehaviour
{
    //Declarations
    private IntegrityBehavior _hullIntegrityRef;
    private Timer _shieldRegenDelayTimer;




    //Monos
    private void Awake()
    {
        InitializeReferences();
    }


    //Utils
    private void InitializeReferences()
    {
        //timer
        _shieldRegenDelayTimer = transform.parent.GetComponent<ShipSystemReferencer>().GetShieldsObject().GetComponent<Timer>();

        //hull Integrity
        _hullIntegrityRef = GetComponent<IntegrityBehavior>();
    }

    public void ResetShieldRegenTimerOnHullDamaged(float value)
    {
        _shieldRegenDelayTimer.RestartTimer();
    }




}
