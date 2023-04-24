using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHullBehavior : ShipSubsystem, IHullBehavior
{
    //Declarations
    [Header("Hull Attributes")]
    [SerializeField] [Min(0)] int _currentValue;
    [SerializeField] [Min(1)] int _maxValue;
    [SerializeField] [Range(0, 1)] float _remainingIntegrityPercentage;
    [SerializeField] [Range(0, 1)] float _criticalThresholdPercentage;
    [SerializeField] bool _isHullCritical;

    [Header("Debugging Commands")]
    [SerializeField] [Min(0)] int _modificationFactor = 1;
    [SerializeField] [Range(0, 1)] float _percentModifier;
    [SerializeField] bool _decreaseHull;
    [SerializeField] bool _increaseHull;
    [SerializeField] bool _decreaseToNearDeath;
    [SerializeField] bool _disableShip;
    [SerializeField] bool _enableShip;
    [SerializeField] bool _changeMaxHull;
    [SerializeField] bool _changeCritThreshold;
    [SerializeField] bool _damageToCritical;

    //events
    public delegate void BasicHullBehaviorEvent();
    public event BasicHullBehaviorEvent OnHullDestroyed;



    //Monobehaviours
    private void Start()
    {
        RecalculateHullIntegrity();
        UpdateHullState();
    }

    private void Update()
    {
        if (_showDebug)
            RunDebugCommands();
    }



    //Interface Utils
    public void SetParent(Ship parent)
    {
        _parentShip = parent;
    }

    public void DamageHull(int damage)
    {
        if (IsDebugActive())
            LogResponse($"recieved Damage: {damage}");

        if (_isHullCritical)
            DisableShipFromCriticalDamage();

        SetCurrentValue(_currentValue - damage);
        if (_currentValue == 0)
        {
            if (_showDebug)
                LogResponse("hull Destroyed.");
            OnHullDestroyed?.Invoke();
            _parentShip.Die();
        }
            

    }

    public void DamageHullToNearDeath()
    {
        if (_currentValue > 1 && _maxValue > 1)
        {
            _currentValue = 1;
            RecalculateHullIntegrity();
            UpdateHullState();

            if (_showDebug)
                LogResponse("damaged to Near Death");

            DisableShipFromCriticalDamage();
        }


    }

    public void DisableShipFromCriticalDamage()
    {
        //play disabledFromDamage Feedback

        if (_showDebug)
            LogResponse("has been disabled via suffering critical damage");
        DisableSubsystem();
        _parentShip.DisableShip();
    }

    public void EnableShip()
    {
        //play enabled Feedback

        EnableSubsystem();
        _parentShip.EnableShip();
    }

    public float GetCriticalThresholdPercent()
    {
        return _criticalThresholdPercentage;
    }

    public float GetIntegrityPercent()
    {
        return _remainingIntegrityPercentage;
    }

    public int GetCurrentValue()
    {
        return _currentValue;
    }

    public int GetMaxValue()
    {
        return _maxValue;
    }

    public void SetCriticalThresholdPercent(float newThreshold)
    {
        if (newThreshold > 0 && newThreshold < 1)
        {
            _criticalThresholdPercentage = newThreshold;
            UpdateHullState();
        }
    }

    public void SetCurrentValue(int newValue)
    {
        _currentValue = Mathf.Clamp(newValue, 0, _maxValue);

        RecalculateHullIntegrity();
        UpdateHullState();
    }

    public void SetCurrentValueToCritical()
    {
        if (_criticalThresholdPercentage > 0 && _remainingIntegrityPercentage > _criticalThresholdPercentage)
        {
            Debug.Log( "Checking Critical Hull Calculation: " + Mathf.FloorToInt((float)_maxValue * _criticalThresholdPercentage));
            if (Mathf.FloorToInt((float)_maxValue * _criticalThresholdPercentage) > 0)
            {
                _remainingIntegrityPercentage = _criticalThresholdPercentage;
                _currentValue = Mathf.FloorToInt((float)_maxValue * _remainingIntegrityPercentage);
                if (IsDebugActive())
                    LogResponse($"Hull set to critical. New Hull: {_currentValue}");

                UpdateHullState();
            }
            else
                Debug.Log("Ignoring SetHullToCritical command: Crit Threshold or MaxHull too low to damage critically without being fatal damage");
           
        }
    }

    public void SetMaxValue(int newValue)
    {
        if (newValue > 0)
        {
            _maxValue = newValue;
            if (_currentValue > _maxValue)
                _currentValue = _maxValue;

            RecalculateHullIntegrity();
            UpdateHullState();
        }
           
    }

    public void RepairHull(int value)
    {
        if (value >= 0)
        {
            SetCurrentValue(_currentValue + value);

            if (_isDisabled)
                EnableShip();
        }
    }



    //Utils
    private void RecalculateHullIntegrity()
    {
        //Recalculate integrity
        _remainingIntegrityPercentage = (float)_currentValue/_maxValue;
        if (IsDebugActive())
            LogResponse($"current Hull Percentage: {_remainingIntegrityPercentage}");
    }

    private void UpdateHullState()
    {
        if (_remainingIntegrityPercentage <= _criticalThresholdPercentage && _isHullCritical == false)
        {
            _isHullCritical = true;
            if (IsDebugActive())
                LogResponse(" HULL CRITICAL");
        }
        else if (_remainingIntegrityPercentage > _criticalThresholdPercentage && _isHullCritical == true)
        {
            _isHullCritical = false;
            if (IsDebugActive())
                LogResponse(" HULL STABILIZED");
        }
    }



    //Debugging
    private void LogResponse(string response)
    {
        if (_parentShip != null)
            Debug.Log(_parentShip.GetName() + " " + response);
        else
        {
            Debug.LogError($"NULL_PARENT_SHIP on obejct: {this.gameObject}, system: {_name}");
            Debug.Log("NULL_SHIP " + response);
        }
    }

    private void RunDebugCommands()
    {
        if (_decreaseHull)
        {
            DamageHull(_modificationFactor);
            _decreaseHull = false;
        }

        if (_increaseHull)
        {
            RepairHull(_modificationFactor);
            _increaseHull = false;
        }

        if (_decreaseToNearDeath)
        {
            DamageHullToNearDeath();
            _decreaseToNearDeath = false;
        }

        if (_disableShip)
        {
            DisableShipFromCriticalDamage();
            _disableShip = false;
        }

        if (_enableShip)
        {
            EnableShip();
            _enableShip = false;
        }

        if (_changeMaxHull)
        {
            SetMaxValue(_modificationFactor);
            _changeMaxHull = false;
        }

        if (_changeCritThreshold)
        {
            SetCriticalThresholdPercent(_percentModifier);
            _changeCritThreshold = false;
        }

        if (_damageToCritical)
        {
            SetCurrentValueToCritical();
            _damageToCritical = false;
        }

    }

}
