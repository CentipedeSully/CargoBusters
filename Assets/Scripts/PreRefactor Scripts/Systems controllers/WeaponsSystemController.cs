using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponsSystemController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _shotCommand = false;
    [SerializeField] private bool _isWeaponsOnline = true;

    [Header("Events")]
    public UnityEvent<bool> OnShotCommand;


    //monobehaviors
    private void Update()
    {
        if (_isWeaponsOnline)
            OnShotCommand?.Invoke(_shotCommand);

        else OnShotCommand?.Invoke(false);
    }


    //Utilites
    public void SetShotCommand(bool value)
    {
        _shotCommand = value;
    }

    public void DisableWeapons()
    {
        _isWeaponsOnline = false;
    }

    public void EnableWeapons()
    {
        _isWeaponsOnline = true;
    }
}
