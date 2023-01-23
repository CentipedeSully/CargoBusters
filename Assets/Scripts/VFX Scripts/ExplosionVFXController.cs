using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosionVFXController : MonoBehaviour, IExplosionVFXController
{
    //Declarations
    [SerializeField] private int _maxParticleSpawnRate = 48;
    [SerializeField] private float _explosionDuration = .15f;
    [SerializeField] private string _particleSpawnRateFieldName = "Particle Spawn Rate";
    //[SerializeField] private string _vfxRadiusFieldName = "Radius";
    [SerializeField] private float _radius = 1;
    private float _timePassed;
    private bool _isExplosionStarted = false;
    private bool _isExplosionOver = false;

    private VisualEffect _vfxExplosionReference;

    




    //Monobehaviors
    private void Awake()
    {
        _vfxExplosionReference = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        CountTime();
    }




    //Utilites
    private void CountTime()
    {
        if (_isExplosionStarted)
        {
            if (_timePassed >= _explosionDuration)
            {
                StopVFX();
            }

            else
            {
                SetVFXParticleSpawnRate((int)Mathf.Lerp(_maxParticleSpawnRate, _maxParticleSpawnRate, _timePassed / _explosionDuration));
                SetVFXRadius(Mathf.Lerp(0, _radius, _timePassed / _explosionDuration));
                _timePassed += Time.deltaTime;
            }


        }

    }

    private void StopVFX()
    {
        if (_isExplosionStarted == true)
            _isExplosionOver = true;
        
    }

    public void PlayExplosionVFX()
    {
        if (_isExplosionStarted == false)
            _isExplosionStarted = true;
    }

    public float GetVFXDuration()
    {
        return _explosionDuration;
    }

    public void SetVFXDuration(float value)
    {
        _explosionDuration = value;
    }

    public void SetVFXParticleSpawnRate(int value)
    {
        _vfxExplosionReference.SetInt(_particleSpawnRateFieldName, value);
    }

    public void SetVFXRadius(float value)
    {
        _radius = value;
    }

    public bool IsExplosionOver()
    {
        return _isExplosionOver;
    }
}
