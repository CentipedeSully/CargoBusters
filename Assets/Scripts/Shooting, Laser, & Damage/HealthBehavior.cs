using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _isDead = false;

    public UnityEvent<GameObject> _OnDeath;


    //Monobehaviors






    //Utilities
    public void SetMaxHealth(float value)
    {
        if (value > 0)
        {
            _maxHealth = value;

            if (_maxHealth < _currentHealth)
                SetCurrentHealth(_maxHealth);
        }
           
    }

    public void SetCurrentHealth(float value)
    {
        if (value > 0)
            _currentHealth = value;
    }

    public void ModifyCurrentHealth(float valueToAdd)
    {
        _currentHealth += valueToAdd;
        Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth == 0)
        {
            _isDead = true;
            _OnDeath?.Invoke(gameObject);
        }
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public bool isDead()
    {
        return _isDead;
    }
}
