using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBarrelOnFire : MonoBehaviour
{
    //Decalrations
    private BarrelFlashController _barrelFlashControllerRef;
    private LightFlasher _lightControllerRef;
    [SerializeField] private float _flashDuration = .1f;
    private bool _isShooting = false;
    private float _timePassed = 0;



    //Monos
    private void Awake()
    {
        _barrelFlashControllerRef = GetComponent<BarrelFlashController>();
        _lightControllerRef = GetComponent<LightFlasher>();
    }

    private void Update()
    {
        CountTimeIfShooting();
    }



    //Utilites
    public void TriggerFlash()
    {
        StartFlash();
        _lightControllerRef.FlashLight();
        
    }

    private void StopFlash()
    {
        _barrelFlashControllerRef.SetShotInput(false);
    }

    private void StartFlash()
    {
        _barrelFlashControllerRef.SetShotInput(true);

        //regardless of the current "isShooting" context, reset time passed.
        _timePassed = 0;
        _isShooting = true;
        
    }

    private void CountTimeIfShooting()
    {
        if (_isShooting)
        {
            if (_timePassed >= _flashDuration)
            {
                _isShooting = false;
                _timePassed = 0;
                StopFlash();
            }
            else _timePassed += Time.deltaTime;
        }
    }
}
