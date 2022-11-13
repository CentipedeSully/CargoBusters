using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntegrityThresholdEvaluator : MonoBehaviour
{
    //declarations
    [Tooltip("An Integrity percentage at this value or above are considered 'high'")]
    [SerializeField] [Range(0, 1)] private float _highThreshold = .7f;
    [Tooltip("An Integrity percentage at this value or below are considered 'low'. Anything in between this value and the higher value is considered 'moderate'")]
    [SerializeField] [Range(0, 1)] private float _lowThreshold = .3f;

    private float _currentIntegrityPercentage = 1;

    private bool _isIntegrityHigh = false;
    private bool _isIntegrityModerate = false;
    private bool _isIntegrityLow = false;


    public event Action OnIntegrityEnteringHigh;
    public event Action OnIntegrityEnteringModerate;
    public event Action OnIntegrityEnteringLow;

    private IntegrityBehavior _integrityBehaviorRef;

    //monos
    private void Awake()
    {
        _integrityBehaviorRef = GetComponent<IntegrityBehavior>();
    }

    private void OnEnable()
    {
        _integrityBehaviorRef.OnIntegrityDecreased += RecalculateIntegrityValuesOnChange;
        _integrityBehaviorRef.OnIntegrityIncreased += RecalculateIntegrityValuesOnChange;

        OnIntegrityEnteringLow += LogEnteringLowIntegrity;
        OnIntegrityEnteringModerate += LogEnteringModerateIntegrity;
        OnIntegrityEnteringHigh += LogEnteringHighIntegrity;
    }

    private void Start()
    {
        CalculateIntegrityPercentage();
        UpdateIntegrityState();
    }

    private void OnDisable()
    {
        _integrityBehaviorRef.OnIntegrityDecreased -= RecalculateIntegrityValuesOnChange;
        _integrityBehaviorRef.OnIntegrityIncreased -= RecalculateIntegrityValuesOnChange;

        OnIntegrityEnteringLow -= LogEnteringLowIntegrity;
        OnIntegrityEnteringModerate -= LogEnteringModerateIntegrity;
        OnIntegrityEnteringHigh -= LogEnteringHighIntegrity;
    }



    //Utilities
    private void CalculateIntegrityPercentage()
    {
        if (_integrityBehaviorRef.GetCurrentIntegrity() == 0)
            _currentIntegrityPercentage = 0;
        else _currentIntegrityPercentage = _integrityBehaviorRef.GetCurrentIntegrity() / _integrityBehaviorRef.GetMaxIntegrity();
    }

    public void RecalculateIntegrityValuesOnChange(float value)
    {
        CalculateIntegrityPercentage();
        UpdateIntegrityState();
    }

    private void UpdateIntegrityState()
    {
        if (_currentIntegrityPercentage >= _highThreshold && !_isIntegrityHigh)
        {
            _isIntegrityHigh = true;
            _isIntegrityLow = false;
            _isIntegrityModerate = false;

            OnIntegrityEnteringHigh?.Invoke();
        }

        else if (_currentIntegrityPercentage < _highThreshold && _currentIntegrityPercentage > _lowThreshold && !_isIntegrityModerate)
        {
            _isIntegrityHigh = false;
            _isIntegrityLow = false;
            _isIntegrityModerate = true;

            OnIntegrityEnteringModerate?.Invoke();
        }

        else if (_currentIntegrityPercentage <= _lowThreshold && !_isIntegrityLow)
        {
            _isIntegrityHigh = false;
            _isIntegrityLow = true;
            _isIntegrityModerate = false;

            OnIntegrityEnteringLow?.Invoke();
        }
    }



    private void LogEnteringLowIntegrity()
    {
        Debug.Log($"=={_integrityBehaviorRef.GetName().ToUpper()} CRITICAL==");
    }

    private void LogEnteringModerateIntegrity()
    {
        Debug.Log($"=={_integrityBehaviorRef.GetName().ToUpper()} WARNING==");
    }

    private void LogEnteringHighIntegrity()
    {
        Debug.Log($"=={_integrityBehaviorRef.GetName().ToUpper()} OPTIMAL==");
    }


    public bool IsIntegrityCritical()
    {
        return _isIntegrityLow;
    }

    public bool IsIntegrityModerate()
    {
        return _isIntegrityModerate;
    }

    public bool IsIntegrityHigh()
    {
        return _isIntegrityHigh;
    }
}
