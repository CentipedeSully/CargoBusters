using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class ShieldBehavior : ShipSubsystem, IShieldSubsystemBehavior
{
    //Declarations
    [Header("Shield Settings")]
    [SerializeField] private bool _startShieldsFull = true;
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

    [Header("Debug Commands")]
    [SerializeField] private int _debugModifierValue;
    [SerializeField] private bool _damageShieldCmd;
    [SerializeField] private bool _setMaxShieldCmd;
    [SerializeField] private bool _setCurrentShieldCmd;


    //events
    public delegate void ShieldEvent();
    public event ShieldEvent OnDamaged;
    public event ShieldEvent OnRegenTicked;
    public event ShieldEvent OnShieldFull;
    public event ShieldEvent OnShieldBroken;


    //references
    //...



    //MonoBehaviours
    private void Awake()
    {
        if (_startShieldsFull)
            SetCurrentValue(_shieldMaxValue);
        else
            UpdateShieldStatus();
    }

    private void Start()
    {
        if (_isShieldFull == false && _isDisabled == false)
            RestabilizeShield();
    }

    private void Update()
    {
        if (_showDebug)
            ListenForDebugCommands();
    }




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
        if (_isShieldFull == false && _isShieldRegenerating == false && _isShieldDestablized == false && _isDisabled == false)
        {
            STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shield Regen Started...");
            _isShieldRegenerating = true;
            InvokeRepeating("TickRegen", _regenTickDurationSeconds, _regenTickDurationSeconds);
        }
    }

    private void TickRegen()
    {
        SetCurrentValue(_shieldCurrentValue + _regenAmountPerTick);
        OnRegenTicked?.Invoke();
        STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shield Regenerated {_regenAmountPerTick} point");

        if (_isShieldFull)
        {
            STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shields Full");
            ExitRegen();
            OnShieldFull?.Invoke();
        }

    }

    private void ExitRegen()
    {
        _isShieldRegenerating = false;
        CancelInvoke();
        STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shield Regeneration Stopped");
    }

    private void DestabilizeShield()
    {
        STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shield Destabilization in Progress...");

        if (_isShieldRegenerating)
            ExitRegen(); //Also Cancels any other previous "RestabilizeShields" Invokes

        if (_isShieldDestablized)
            CancelInvoke(); //Make certain to not Invoke "RestabilizeShields" multiple times

        _isShieldDestablized = true;
        STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shield Destabilization Complete");

        if (_isDisabled == false)
        {
            STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Beginning Shield Restabilization...");
            Invoke("RestabilizeShield", _regenDelaySeconds);
        }


    }

    private void RestabilizeShield()
    {
        STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shield Stabilized");
        _isShieldDestablized = false;
        EnterRegenIfShieldStableAndNotFull();
    }



    //External Utils
    public void DamageShields(int damage)
    {
        if (_isDisabled == false)
        {
            damage = Mathf.Max(0, damage);
            STKDebugLogger.LogStatement(_showDebug, $"Applying {damage} damage to {_parentShip.GetName()} Shield Subsystem...");
            SetCurrentValue(_shieldCurrentValue - damage);
            InterruptShieldingProcess();
            OnDamaged?.Invoke();

            if (_shieldCurrentValue == 0)
            {
                STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shield Broken!");
                OnShieldBroken?.Invoke();
            }

        }

        else
            STKDebugLogger.LogWarning("Cant apply shield damage to disabled shields subsystem");
    }

    public void DisableShields()
    {
        STKDebugLogger.LogStatement(_showDebug, $"Disabling {_parentShip.GetName()} Shield Subsystem...");
        DisableSubsystem();
        DestabilizeShield();
        SetCurrentValue(0);
        STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shields Disabled");
    }

    public void EnableShields()
    {
        STKDebugLogger.LogStatement(_showDebug, $"Enabling {_parentShip.GetName()} Shield Subsystem...");
        EnableSubsystem();
        STKDebugLogger.LogStatement(_showDebug, $"{_parentShip.GetName()} Shields Enabled");
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
        //...
    }

    public void InterruptShieldingProcess()
    {
        DestabilizeShield();
    }

    public bool IsShieldsDisabled()
    {
        return _isDisabled;
    }

    public void LogAllData()
    {
        //...
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



    //Debug Utils
    private void ListenForDebugCommands()
    {
        if (_damageShieldCmd)
        {
            _damageShieldCmd = false;
            DamageShields(_debugModifierValue);
        }

        if (_setCurrentShieldCmd)
        {
            _setCurrentShieldCmd = false;
            SetCurrentValue(_debugModifierValue);
        }

        if (_setMaxShieldCmd)
        {
            _setMaxShieldCmd = false;
            SetMaxValue(_debugModifierValue);
        }
    }
}

