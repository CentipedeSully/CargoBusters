using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldsManager : MonoBehaviour
{
    //Declarations
    private IntegrityBehavior _shieldsIntegrityRef;
    private Timer _shieldsRegenDelayTimerRef;
    private Regenerator _shieldsRegeneratorRef;
    private IntegrityBehavior _hullIntegrityRef;

    private bool _isShieldsEnabled = true;

    //Monobehaviors
    private void Awake()
    {
        InitializeReferences();
    }

    private void OnEnable()
    {
        //Restart the Regen Timer whenever the shields are damaged
        _shieldsIntegrityRef.OnIntegrityDecreased += InterruptRegenDelaytimer;

        //Restart Regen Timer on Hull Damage
        _hullIntegrityRef.OnIntegrityDecreased += InterruptRegenDelaytimer;

        //Cancel Regeneration whenever Shields are damaged
        _shieldsIntegrityRef.OnIntegrityDecreased += InterruptRegenerator;

        //Begin Regenerating whenever the Regen timer completes
        _shieldsRegenDelayTimerRef.OnTimerExpired.AddListener(EnterRegeneration);

        //End regen if Integrity At Max
        _shieldsIntegrityRef.OnIntegrityIncreased += EndRegenOnIntegrityFull;
    }

    private void Start()
    {
        StartTimerUntilRegen();
    }

    private void OnDisable()
    {
        //unSub
        _shieldsIntegrityRef.OnIntegrityDecreased -= InterruptRegenDelaytimer;

        //unSub
        _hullIntegrityRef.OnIntegrityDecreased -= InterruptRegenDelaytimer;

        //unSub
        _shieldsIntegrityRef.OnIntegrityDecreased -= InterruptRegenerator;

        //unSub
        _shieldsRegenDelayTimerRef.OnTimerExpired.RemoveListener(EnterRegeneration);

        //usSub
        _shieldsIntegrityRef.OnIntegrityIncreased -= EndRegenOnIntegrityFull;
    }




    //Utilities
    private void InitializeReferences()
    {
        _shieldsIntegrityRef = GetComponent<IntegrityBehavior>();
        _shieldsRegenDelayTimerRef = GetComponent<Timer>();
        _shieldsRegeneratorRef = GetComponent<Regenerator>();
        _hullIntegrityRef = transform.parent.GetComponent<ShipSystemReferencer>().GetHullObject().GetComponent<IntegrityBehavior>();
    }

    private void InterruptRegenDelaytimer(float value)
    {
        _shieldsRegenDelayTimerRef.RestartTimer();
    }

    private void EnterRegeneration()
    {
        if (_shieldsIntegrityRef.GetCurrentIntegrity() < _shieldsIntegrityRef.GetMaxIntegrity() && _isShieldsEnabled)
            _shieldsRegeneratorRef.StartRegen();
    }

    private void InterruptRegenerator(float value)
    {
        _shieldsRegeneratorRef.StopRegen();
    }

    private void DisableShields()
    {
        _isShieldsEnabled = false;
    }

    private void EnableShields()
    {
        _isShieldsEnabled = true;
    }

    private void EndRegenOnIntegrityFull(float value)
    {
        if (_shieldsIntegrityRef.GetCurrentIntegrity() == _shieldsIntegrityRef.GetMaxIntegrity())
            _shieldsRegeneratorRef.StopRegen();
    }

    private void StartTimerUntilRegen()
    {
        if (_isShieldsEnabled && _shieldsIntegrityRef.GetCurrentIntegrity() < _shieldsIntegrityRef.GetMaxIntegrity())
            _shieldsRegenDelayTimerRef.StartOrResumeTimer();
    }
}
