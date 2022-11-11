using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BarrelFlashController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _shotInput = false;
    [SerializeField] private int _VfxMaxParticleCount = 8;

    [SerializeField] private string _vfxParticleSpawnRateFieldName = "Particle Spawn Rate";
    private VisualEffect _vfxBarrelFlash;




    //Monobehaviors
    private void Awake()
    {
        _vfxBarrelFlash = GetComponent<VisualEffect>();
    }




    //Utilities
    public void SetShotInput(bool input)
    {
        _shotInput = input;
        ToggleShotViaInput();
    }

    private void ToggleShotViaInput()
    {
        if (_shotInput)
            _vfxBarrelFlash.SetInt(_vfxParticleSpawnRateFieldName, _VfxMaxParticleCount);
        else _vfxBarrelFlash.SetInt(_vfxParticleSpawnRateFieldName, 0);

    }





}
