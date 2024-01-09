using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldsAnimatorController : MonoBehaviour
{
    //Delcarations
    [SerializeField] private string _triggerName = "OnShieldsDamaged";
    [SerializeField] private string _boolName = "isShieldsOnline";
    private Animator _shieldsAnimator;
    private int _shieldIntegrity;

    //Monobehaivors
    private void Awake()
    {
        _shieldsAnimator = GetComponent<Animator>();
    }


    //Utilites
    public void IncreaseShieldIntegrity()
    {
        _shieldIntegrity++;
        RestoreShields();
    }

    public void DamageShields()
    {
        if (_shieldIntegrity > 0)
        {
            _shieldsAnimator.SetTrigger(_triggerName);
            _shieldIntegrity--;

            if (_shieldIntegrity == 0)
                BreakShields();
        }
    }

    public void BreakShields()
    {
        _shieldsAnimator.SetBool(_boolName,false);
        if (_shieldIntegrity > 0)
            _shieldIntegrity = 0;
    }

    public void RestoreShields()
    {
        _shieldsAnimator.SetBool(_boolName, true);
    }



}
