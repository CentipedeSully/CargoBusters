using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private HealthBehavior _healthReference;
    [SerializeField] private ShieldBehavior _shieldReference;

    public UnityEvent<float> OnHealthDamaged;
    public UnityEvent<float> OnShieldDamaged;



    public void DelegateDamage(float damageValue)
    {
        if (_shieldReference != null)
        {
            if (_shieldReference.IsShieldDepleted() == false)
                OnShieldDamaged?.Invoke(damageValue);

            else OnHealthDamaged?.Invoke(damageValue);
        }

        else OnHealthDamaged?.Invoke(damageValue);

    }
}
