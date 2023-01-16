using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OldHealthBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _isDead = false;
    private OldDamageHandler _damageHandlerRef;

    public UnityEvent<GameObject> _OnDeath;


    //Monobehaviors
    private void Awake()
    {
        _damageHandlerRef = GetComponent<OldDamageHandler>();
    }

    private void OnEnable()
    {
       // _damageHandlerRef.OnHealthDamaged.AddListener(DamageHealth);
    }

    private void OnDisable()
    {
        //_damageHandlerRef.OnHealthDamaged.RemoveListener(DamageHealth);
    }

    private void Start()
    {
        SetCurrentHealth(_maxHealth);
    }



    //Utilities
    public void SetMaxHealth(int value)
    {
        if (value > 0)
        {
            _maxHealth = value;

            if (_maxHealth < _currentHealth)
                SetCurrentHealth(_maxHealth);
        }
           
    }

    public void SetCurrentHealth(int value)
    {
        if (value > 0)
            _currentHealth = value;
    }

    public void ModifyCurrentHealth(int valueToAdd)
    {
        //Debug.Log($"{gameObject.name} Sustained Damage: {valueToAdd}");
        _currentHealth += valueToAdd;
        Mathf.Clamp(_currentHealth, 0, _maxHealth);
        //Debug.Log($"{gameObject.name} Current Health: {_currentHealth}");
        if (_currentHealth <= 0 && _isDead == false)
        {
            _isDead = true;
            _OnDeath?.Invoke(gameObject);
        }
    }

    public void DamageHealth(int value)
    {
        _currentHealth -= value;
        Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth <= 0 && _isDead == false)
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
