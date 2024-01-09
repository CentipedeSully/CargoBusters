using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldsSystemController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isShieldsOnline = true;
    [SerializeField] private bool _isShieldsDisabled = false;
    private IntegrityBehavior _shieldIntegrityRef;

    [Header("Events")]
    public UnityEvent<int> OnShieldsDamaged;
    public UnityEvent OnShieldRegenInterrupted;
    public UnityEvent OnShieldsDisabled;
    public UnityEvent OnShieldsEnabled;


    //Monobehaviors
    private void Awake()
    {
        _shieldIntegrityRef = GetComponent<IntegrityBehavior>();
    }

    private void Start()
    {
        UpdateShieldsStatus();
    }


    //Utilites
    public bool IsShieldsOnline()
    {
        return _isShieldsOnline;
    }

    public bool IsShieldsDisabled()
    {
        return _isShieldsDisabled;
    }

    public void DisableShields()
    {
        _isShieldsDisabled = true;
        OnShieldsDisabled?.Invoke();
    }

    public void EnableShields()
    { 
        _isShieldsDisabled = false;
        OnShieldsEnabled?.Invoke();
    }

    public void DamageShields(int damage)
    {
        if (_isShieldsOnline)
        {
            OnShieldsDamaged?.Invoke(damage);
            OnShieldRegenInterrupted?.Invoke();
        }
            
    }

    public void UpdateShieldsStatus()
    {
        if (_shieldIntegrityRef.GetCurrentIntegrity() == 0)
            _isShieldsOnline = false;
        else _isShieldsOnline = true;
    }

    public void UpdateShieldsStatus(int damage)
    {
        UpdateShieldsStatus();
    }

    public void InterruptRegenOnly()
    {
        OnShieldRegenInterrupted?.Invoke();
    }

    public void InterruptRegenOnly(int damage)
    {
        InterruptRegenOnly();
    }
}
