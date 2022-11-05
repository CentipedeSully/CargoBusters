using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using SullysToolkit;

public class ShieldVFXController : MonoBehaviour
{
    //Declarations
    //[SerializeField] private int _originalParticleSpawnRate = 100;
    //[SerializeField] private float _shieldRadius = 2;

    [SerializeField] private int _currentShieldSpawnRate;
    [SerializeField] private float _currentShieldRadius;

    [SerializeField] private string _vfxSpawnRateFieldName = "Spawn Rate";
    [SerializeField] private string _vfxRadiusFieldName = "Radius";
    [SerializeField] private VisualEffect _shieldVisualEffect;


    [SerializeField] private AnimationCurve _restorationSpawnRateCurve;
    [SerializeField] private AnimationCurve _restorationRadiusCurve;
    [SerializeField] private float _restorationAnimDuration;
    [SerializeField] private bool _isShieldRestoreAnimPlaying = false;

    [SerializeField] private AnimationCurve _shieldDamageSpawnRateCurve;
    [SerializeField] private AnimationCurve _shieldDamageRadiusCurve;
    [SerializeField] private float _shieldDamageAnimDuration;
    [SerializeField] private bool _isShieldDamagedAnimPlaying = false;

    [SerializeField] private AnimationCurve _shieldBreakSpawnRateCurve;
    [SerializeField] private AnimationCurve _shieldBreakRadiusCurve;
    [SerializeField] private float _shieldBreakAnimDuration;
    [SerializeField] private bool _isShieldBreakAnimPlaying = false;


    private float _timePassed = 0;

    //Monobehaviors
    private void Update()
    {
        EvaluateCurvesOverTime();
    }




    //Utilities
    public void DepleteShields()
    {
        ApplyChangeToVFX(0, 0);
    }

    public void PlayShieldRestoreAnim()
    {
        _isShieldRestoreAnimPlaying = true;
    }

    public void PlayShieldDamagedAnim()
    {
        _isShieldDamagedAnimPlaying = true;
    }

    public void PlayShieldbreakAnim()
    {
        _isShieldBreakAnimPlaying = true;
    }


    private void EvaluateCurvesOverTime()
    {
        if (_isShieldBreakAnimPlaying && !_isShieldDamagedAnimPlaying && !_isShieldRestoreAnimPlaying)
            EvaluateShieldBreakAnimation();

        else if (_isShieldDamagedAnimPlaying && !_isShieldBreakAnimPlaying && !_isShieldRestoreAnimPlaying)
            EvaluateShieldDamageAnimation();

        else if (_isShieldRestoreAnimPlaying && !_isShieldBreakAnimPlaying && !_isShieldDamagedAnimPlaying)
            EvaluateRestorationAnimation();

    }

    private void EvaluateRestorationAnimation()
    {
        _currentShieldRadius = _restorationRadiusCurve.Evaluate(_timePassed);
        _currentShieldSpawnRate = (int)_restorationSpawnRateCurve.Evaluate(_timePassed);
        ApplyChangeToVFX(_currentShieldSpawnRate, _currentShieldRadius);

        CountRestorationAnimTime();
    }

    private void EvaluateShieldDamageAnimation()
    {
        _currentShieldRadius = _shieldDamageRadiusCurve.Evaluate(_timePassed);
        _currentShieldSpawnRate = (int)_shieldDamageSpawnRateCurve.Evaluate(_timePassed);
        ApplyChangeToVFX(_currentShieldSpawnRate, _currentShieldRadius);

        CountDamageAnimTime();
    }

    private void EvaluateShieldBreakAnimation()
    {
        _currentShieldRadius = _shieldBreakRadiusCurve.Evaluate(_timePassed);
        _currentShieldSpawnRate = (int)_shieldBreakSpawnRateCurve.Evaluate(_timePassed);
        ApplyChangeToVFX(_currentShieldSpawnRate, _currentShieldRadius);

        CountBreakAnimTime();
    }


    private void CountRestorationAnimTime()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed >= _restorationAnimDuration)
        {
            _timePassed = 0;
            _isShieldRestoreAnimPlaying = false;
        }
           
    }

    private void CountDamageAnimTime()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed >= _shieldDamageAnimDuration)
        {
            _timePassed = 0;
            _isShieldDamagedAnimPlaying = false;
        }
    }

    private void CountBreakAnimTime()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed >= _shieldBreakAnimDuration)
        {
            _timePassed = 0;
            _isShieldBreakAnimPlaying = false;
        }
    }


    private void ApplyChangeToVFX(int newSpawnRate, float newRadius)
    {
        _shieldVisualEffect.SetFloat(_vfxRadiusFieldName, newRadius);
        _shieldVisualEffect.SetInt(_vfxSpawnRateFieldName, newSpawnRate);
    }
}
