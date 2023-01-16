using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HullSystemController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isHullCritical = false;
    [SerializeField] private bool _isHullDestroyed = false;
    private IntegrityBehavior _hullIntegrityRef;
    private IntegrityThresholdEvaluator _integrityThresholdRef;

    [Header("Events")]
    public UnityEvent<int> OnHullDamaged;
    public UnityEvent OnHullDamagedWhileCritical;
    public UnityEvent OnHullDestroyed;


    //Monobehaviors
    private void Awake()
    {
        _hullIntegrityRef = GetComponent<IntegrityBehavior>();
        _integrityThresholdRef = GetComponent<IntegrityThresholdEvaluator>();
    }


    //Utilities
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

}
