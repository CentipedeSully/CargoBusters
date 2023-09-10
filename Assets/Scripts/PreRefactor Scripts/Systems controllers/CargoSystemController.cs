using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CargoSystemController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isCargoSecurityOnline = true;
    [SerializeField] private bool _isCargoBusted = false;

    [Header("Events")]
    public UnityEvent OnCargoSecurityDisabled;
    public UnityEvent OnCargoSecurityEnabled;

    public UnityEvent OnCargoBusted;


    //Monos
    //...



    //Utilities
    //...

    //External Control Utils
    public void DisableCargoSecurity()
    {
        _isCargoSecurityOnline = false;
        OnCargoSecurityDisabled?.Invoke();
    }

    public void EnableCargoSecurity()
    {
        _isCargoSecurityOnline = true;
        OnCargoSecurityEnabled?.Invoke();
    }

    public void BustCargo()
    {
        _isCargoBusted = true;
        OnCargoBusted?.Invoke();
    }

    //Getters & Setters
    public bool IsCargoSecuritySystemOnline()
    {
        return _isCargoSecurityOnline;
    }

    public bool IsCargoBusted()
    {
        return _isCargoBusted;
    }

}
