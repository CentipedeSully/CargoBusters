using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHullBehavior : IHullBehavior
{
    //Declarations
    Ship _parent;
    int _currentValue;
    int _maxValue;
    float _remainingIntegrityPercentage;
    float _criticalThresholdPercentage;
    bool _isDebugActive;


    //Constructors
    public BasicHullBehavior(Ship parent, int maxHull, int currentHull, float hullCriticalPercent, bool showDebug = false)
    {
        //accept data
        this._parent = parent;
        this._maxValue = maxHull;
        this._currentValue = currentHull;
        this._criticalThresholdPercentage = hullCriticalPercent;
        this._isDebugActive = showDebug;


        //check data
        if (_parent == null)
            Debug.LogError("Parent field of BasicHullBehavior is null. BasicHullBehavior will not reference '_parent' Ship without error");

        if (_maxValue <= 0)
            _maxValue = 1;

        if (_currentValue > _maxValue)
            _currentValue = _maxValue;
        else if (_currentValue <= 0)
            _currentValue = 1;

        if (_criticalThresholdPercentage > 1 || _criticalThresholdPercentage < 0)
            _criticalThresholdPercentage = .4f;


        //calculate current integrity
        RecalculateHullIntegrity();
            
    }




    //Interface Utils

    public void DamageHull(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void DamageHullToNearDeath()
    {
        throw new System.NotImplementedException();
    }

    public void DisableShipFromCriticalDamage()
    {
        //play disabledFromDamage Feedback
        _parent.DisableShip();
    }

    public void EnableShip()
    {
        throw new System.NotImplementedException();
    }

    public void EnterDebug()
    {
        _isDebugActive = true;
    }

    public void ExitDebug()
    {
        _isDebugActive = false;
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

    public bool IsDebugActive()
    {
        return _isDebugActive;
    }

    public void SetCriticalThresholdPercent(float newThreshold)
    {
        throw new System.NotImplementedException();
    }

    public void SetCurrentValue(int newValue)
    {
        _currentValue = Mathf.Clamp(newValue, 1, _maxValue);

        RecalculateHullIntegrity();
    }

    public void SetCurrentValueToCritical()
    {
        throw new System.NotImplementedException();
    }

    public void SetMaxValue(int newValue)
    {
        if (newValue > 0)
        {
            _maxValue = newValue;
            if (_currentValue > _maxValue)
                _currentValue = _maxValue;

            RecalculateHullIntegrity();
        }
           
    }


    //Utils
    private void RecalculateHullIntegrity() //ALSO CHECK IF THE INTEGRITY IS CRITICAL
    {
        _remainingIntegrityPercentage = Mathf.FloorToInt((_currentValue / _maxValue) * 100) / 100;
        if (_isDebugActive)
        {
            if (_parent != null)
                Debug.Log($"{_parent.name} Initialized Hull Percentage: {_remainingIntegrityPercentage}");
            else
            {
                Debug.Log($"NULL_SHIP Initialized Hull Percentage: {_remainingIntegrityPercentage}");
                Debug.LogError("Parent field of BasicHullBehavior is null.");
            }
        }
    }



}
