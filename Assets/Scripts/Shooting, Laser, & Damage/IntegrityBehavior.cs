using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class IntegrityBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _name;
    [SerializeField] private float _maxIntegrity = 10;
    [SerializeField] private float _currentIntegrity = 10;
    [SerializeField] private bool _startWithFullIntegrity = true;

    [Header("Events")]
    public UnityEvent<float> OnIntegrityDecreased;
    public UnityEvent<float> OnIntegrityIncreased;
    public UnityEvent OnIntegrityMaxed;



    //Monobehaviors
    private void Start()
    {
        if (_startWithFullIntegrity)
            SetCurrentIntegrity(_maxIntegrity);
    }




    //Utilities
    public string GetName()
    {
        return _name;
    }

    public float GetMaxIntegrity()
    {
        return _maxIntegrity;
    }

    public float GetCurrentIntegrity()
    {
        return _currentIntegrity;
    }

    private void SetMaxIntegrity(float value)
    {
        if (value > 0)
            _maxIntegrity = value;

        _currentIntegrity = Mathf.Clamp(_currentIntegrity, 0, _maxIntegrity);
    }

    private void SetCurrentIntegrity(float value)
    {
        if (value > 0)
            _currentIntegrity = value;

        _currentIntegrity = Mathf.Clamp(_currentIntegrity, 0, _maxIntegrity);
    }

    public void DecreaseIntegrity(float value)
    {
        if (_currentIntegrity > 0)
        {
            _currentIntegrity -= value;

            _currentIntegrity = Mathf.Clamp(_currentIntegrity,0,_maxIntegrity);

            OnIntegrityDecreased?.Invoke(value);
        }
    }

    public void IncreaseIntegrity(float value)
    {
        if (_currentIntegrity < _maxIntegrity)
        {
            _currentIntegrity += value;

            _currentIntegrity = Mathf.Clamp(_currentIntegrity, 0, _maxIntegrity);

            OnIntegrityIncreased?.Invoke(value);

            if (_currentIntegrity == _maxIntegrity)
                OnIntegrityMaxed?.Invoke();
        }
    }

    public void FillIntegrity()
    {
        IncreaseIntegrity(_maxIntegrity);
    }

    public void EmptyIntegrity()
    {
        DecreaseIntegrity(_maxIntegrity);
    }

}
