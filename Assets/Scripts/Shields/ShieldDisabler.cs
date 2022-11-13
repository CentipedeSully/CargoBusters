using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDisabler : MonoBehaviour
{
    //Declarations
    private ShieldsRegenerationManager _shieldRegeneratorRef;



    //mono
    private void Awake()
    {
        _shieldRegeneratorRef = GetComponent<ShieldsRegenerationManager>();
    }



    //Utilities
    public void DisableShieldRegeneration()
    {
        _shieldRegeneratorRef.DisableShieldRegen();
    }


    public void EnableShieldRegeneration()
    {
        _shieldRegeneratorRef.EnableShieldRegen();
    }



}
