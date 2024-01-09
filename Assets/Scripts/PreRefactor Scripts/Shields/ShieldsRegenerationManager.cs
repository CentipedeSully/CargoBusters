using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldsRegenerationManager : MonoBehaviour
{
    //Declarations
    private IntegrityBehavior _shieldsIntegrityRef;
    private Timer _shieldsRegenDelayTimerRef;
    private Regenerator _shieldsRegeneratorRef;

    private bool _isShieldRegenEnabled = true;

    //Monobehaviors
    private void Awake()
    {
        InitializeReferences();
    }

    private void Start()
    {
        StartTimerUntilRegen();
    }

    private void OnDisable()
    {
        //unSub
        //_shieldsIntegrityRef.OnIntegrityDecreased -= InterruptRegenDelaytimer;

        //unSub
        //_shieldsIntegrityRef.OnIntegrityDecreased -= InterruptRegenerator;

        //unSub
        //_shieldsRegenDelayTimerRef.OnTimerExpired.RemoveListener(EnterRegeneration);

        //usSub
        //_shieldsIntegrityRef.OnIntegrityIncreased -= EndRegenOnIntegrityFull;
    }




    //Utilities
    private void InitializeReferences()
    {
        _shieldsIntegrityRef = GetComponent<IntegrityBehavior>();
        _shieldsRegenDelayTimerRef = GetComponent<Timer>();
        _shieldsRegeneratorRef = GetComponent<Regenerator>();
    }

    public void InterruptRegenDelaytimer(float value)
    {
        _shieldsRegenDelayTimerRef.RestartTimer();
    }

    public void EnterRegeneration()
    {
        if (_shieldsIntegrityRef.GetCurrentIntegrity() < _shieldsIntegrityRef.GetMaxIntegrity() && _isShieldRegenEnabled)
            _shieldsRegeneratorRef.StartRegen();
    }

    public void InterruptRegenerator(float value)
    {
        _shieldsRegeneratorRef.StopRegen();
    }

    public void DisableShieldRegen()
    {
        _isShieldRegenEnabled = false;
    }

    public void EnableShieldRegen()
    {
        _isShieldRegenEnabled = true;
        StartTimerUntilRegen();
    }

    private void EndRegenOnIntegrityFull(float value)
    {
        if (_shieldsIntegrityRef.GetCurrentIntegrity() == _shieldsIntegrityRef.GetMaxIntegrity())
            _shieldsRegeneratorRef.StopRegen();
    }

    private void StartTimerUntilRegen()
    {
        if (_isShieldRegenEnabled && _shieldsIntegrityRef.GetCurrentIntegrity() < _shieldsIntegrityRef.GetMaxIntegrity())
            _shieldsRegenDelayTimerRef.StartOrResumeTimer();
    }
}
