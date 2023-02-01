using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HullSystemController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _regenCommand = false;
    [SerializeField] private bool _isRegenUnlocked = false;
    [SerializeField] private bool _isRegenerating = false;
    [SerializeField] private bool _isRegenEnabled = true;
    [SerializeField] private bool _isRegenReady = true;
    [SerializeField] private float _regenCooldown = .5f;

    [SerializeField] private bool _isHullCritical = false;
    [SerializeField] private bool _isHullDestroyed = false;
    private IntegrityBehavior _hullIntegrityRef;
    private IntegrityThresholdEvaluator _integrityThresholdRef;

    [Header("Events")]
    public UnityEvent OnHullRegenStarted;
    public UnityEvent OnHullRegenInterrupted;
    public UnityEvent<int> OnHullDamaged;
    public UnityEvent OnHullDamagedWhileCritical;
    public UnityEvent OnHullDestroyed;
    public UnityEvent OnHullRegenDisabled;
    public UnityEvent OnHullRegenEnabled;
    public UnityEvent OnHullRegenExited;


    //Monobehaviors
    private void Awake()
    {
        _hullIntegrityRef = GetComponent<IntegrityBehavior>();
        _integrityThresholdRef = GetComponent<IntegrityThresholdEvaluator>();
    }

    private void Update()
    {
        ManagerRegenStateOnInput();   
    }


    //Utilities
    private void ManagerRegenStateOnInput()
    {
        if (_isRegenUnlocked && _isRegenEnabled && _isRegenReady)
        {
            //Start regen if pressing regen
            if (_regenCommand && _isRegenerating == false && _hullIntegrityRef.GetCurrentIntegrity() < _hullIntegrityRef.GetMaxIntegrity())
            {
                _isRegenerating = true;
                OnHullRegenStarted?.Invoke();
            }
                

            //end regen if the input is released before completion
            else if (_regenCommand == false && _isRegenerating)
            {
                _isRegenReady = false;
                Invoke("ReadyRegen",_regenCooldown);
                InterruptRegeneration();
            }
                
            //end regen if max integrity reached
            else if (_isRegenerating && _hullIntegrityRef.GetCurrentIntegrity() == _hullIntegrityRef.GetMaxIntegrity())
            {
                _isRegenReady = false;
                Invoke("ReadyRegen", _regenCooldown);
                ExitRegen();

            }

            //regen will end when integrity reaches max
        }
    }
    private void ReadyRegen()
    {
        _isRegenReady = true;
    }

    public void ExitRegen()
    {
        _isRegenerating = false;
        OnHullRegenExited?.Invoke();
    }

    public void InterruptRegeneration()
    {
        if (_isRegenerating)
        {
            _isRegenerating = false;
            OnHullRegenInterrupted?.Invoke();
        }
    }

    public void DamageHull(int damage)
    {
        if (!_isHullDestroyed)
        {
            if (!_isHullCritical)
            {
                OnHullDamaged?.Invoke(damage);
            }

            else
            {
                OnHullDamaged?.Invoke(damage);
                OnHullDamagedWhileCritical?.Invoke();
            }
        }
        
    }

    public void UpdateHullStatus()
    {
        if (_hullIntegrityRef.GetCurrentIntegrity() == 0)
        {
            _isHullDestroyed = true;
            OnHullDestroyed?.Invoke();
        }

        else if (_integrityThresholdRef.IsIntegrityCritical())
            _isHullCritical = true;

        else _isHullCritical = false;
    }

    public void UpdateHullStatus(int damage)
    {
        UpdateHullStatus();
    }

    public void DisableRegeneration()
    {
        _isRegenEnabled = false;

        if (_isRegenerating)
            InterruptRegeneration();

        OnHullRegenDisabled?.Invoke();
    }

    public void EnableRegeneration()
    {
        _isRegenEnabled = true;
        OnHullRegenEnabled?.Invoke();
    }

    public void SetRegenCommand(bool value)
    {
        _regenCommand = value;
    }

    public bool IsRegenEnabled()
    {
        return _isRegenEnabled;
    }

    public bool IsRegenUnlocked()
    {
        return _isRegenUnlocked;
    }

    public void UnlockHullRegeneration()
    {
        _isRegenUnlocked = true;
    }
}
