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

    private void OnEnable()
    {
        _hullIntegrityRef.OnIntegrityDecreased += ResetShieldRegenTimerOnHullDamaged;
    }

    private void OnDisable()
    {
        _hullIntegrityRef.OnIntegrityDecreased -= ResetShieldRegenTimerOnHullDamaged;
    }


    //Utils
    private void InitializeReferences()
    {
        //timer
        _shieldRegenDelayTimer = transform.parent.GetComponent<ShipSystemReferencer>().GetShieldsObject().GetComponent<Timer>();

        //hull Integrity
        _hullIntegrityRef = GetComponent<IntegrityBehavior>();
    }

    private void ResetShieldRegenTimerOnHullDamaged(float value)
    {
        _shieldRegenDelayTimer.RestartTimer();
    }




}
