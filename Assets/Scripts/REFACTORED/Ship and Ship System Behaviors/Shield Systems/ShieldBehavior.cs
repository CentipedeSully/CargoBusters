using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : ShipSubsystem, IShieldSubsystemBehavior
{
    //Declarations
    [Header ("Shield Settings")]
    [SerializeField] private int _shieldMaxValue;
    [SerializeField] private int _shieldCurrentValue;
    [SerializeField] private bool _isShieldFull = true;

    [Header("Regeneration Settings")]
    [SerializeField] private int _regenAmountPerTick = 1;
    [Tooltip("Determines how long shield regeneration is stunted once shields are damaged")]
    [SerializeField] private float _regenDelaySeconds = 6f;
    [Tooltip("The amount of time that passes")]
    [SerializeField] private float _regenTickDurationSeconds = 1.5f;
    [SerializeField] private bool _isShieldRegenerating = false;
    [SerializeField] private bool _isShieldDestablized = false;

    
    //events
    public delegate void ShieldEvent();
    public event ShieldEvent OnDamaged;
    public event ShieldEvent OnRegenTicked;
    public event ShieldEvent OnShieldFull;
    public event ShieldEvent OnShieldBroken;


    //references
    private IEnumerator _regenCounter;

    //MonoBehaviours





    //Internal Utils
    private void UpdateShieldStatus()
    {
        if (_shieldCurrentValue == _shieldMaxValue)
            _isShieldFull = true;
        else
            _isShieldFull = false;
    }

    private void EnterRegenIfShieldStableAndNotFull()
    {
        if (_isShieldFull == false && _isShieldRegenerating  == false && _isShieldDestablized == false)
        {
            _isShieldRegenerating = true;
            InvokeRepeating("TickRegen", _regenTickDurationSeconds, _regenTickDurationSeconds);
        }
    }

    private void TickRegen()
    {
        SetCurrentValue(_shieldCurrentValue + _regenAmountPerTick);
        if (_isShieldFull)
            ExitRegen();
    }

    private void ExitRegen()
    {
        _isShieldRegenerating = false;
        CancelInvoke();
    }

    private void DestabilizeShield()
    {
        if (_isShieldRegenerating)
            ExitRegen(); //Also Cancels any other previous "DestabilizeShield" Invokes

        _isShieldDestablized = true;
        Invoke("RestabilizeShield", _regenDelaySeconds);

    }

    private void RestabilizeShield()
    {
        _isShieldDestablized = false;
        EnterRegenIfShieldStableAndNotFull();
    }

    //External Utils
    public void DamageShields(int damage)
    {
        InterruptShieldingProcess();
    }

    public void DisableShields()
    {
        DisableSubsystem();
    }

    public void EnableShields()
    {
        EnableSubsystem();
        EnterRegenIfShieldStableAndNotFull();
    }

    public void EnterDebug()
    {
        if (_showDebug == false)
            ToggleDebugMode();
    }

    public void ExitDebug()
    {
        if (_showDebug == true)
            ToggleDebugMode();
    }

    public int GetCurrentValue()
    {
        return _shieldCurrentValue;
    }

    public int GetMaxValue()
    {
        return _shieldMaxValue;
    }

    public void InitializeGameManagerDependentReferences()
    {
        throw new System.NotImplementedException();
    }

    public void InterruptShieldingProcess()
    {
        ExitRegen();
    }

    public bool IsShieldsDisabled()
    {
        return _isDisabled;
    }

    public void LogAllData()
    {
        throw new System.NotImplementedException();
    }

    public void SetCurrentValue(int newValue)
    {
        _shieldCurrentValue = Mathf.Clamp(newValue, 0, _shieldMaxValue);
        UpdateShieldStatus();
    }

    public void SetMaxValue(int newValue)
    {
        _shieldMaxValue = Mathf.Max(1, newValue);
        SetCurrentValue(_shieldCurrentValue);
    }

    public void SetParentShipAndInitializeAwakeReferences(AbstractShip parent)
    {
        _parentShip = parent;
    }
}
