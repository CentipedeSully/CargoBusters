using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OldDamageHandler : MonoBehaviour
{
    [SerializeField] private OldHealthBehavior _healthReference;
    [SerializeField] private OldShieldBehaviour _shieldReference;

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
