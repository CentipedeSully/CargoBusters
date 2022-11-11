using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{

    //Decalrations
    [SerializeField] private Light2D _2dLighRef;

    [SerializeField] private float _startingIntensity = 0;
    [SerializeField] private float _transitionDuration = .2f;

    [SerializeField] private AnimationCurve _flashIntensityCurve;
    private bool _isTransitioning = false;
    private float _timePassed = 0;



    //monobehaviors
    private void Awake()
    {
        _2dLighRef = GetComponent<Light2D>();
    }

    private void Start()
    {
        SetupStartingIntensity();
    }

    private void Update()
    {
        CountTimeIfFlashingLight();
    }






    //Utilites
    private void CountTimeIfFlashingLight()
    {
        if (_isTransitioning)
        {
            if (_timePassed >= _transitionDuration)
            {
                ResetFlashUtils();
            }
            else
            {
                
                _2dLighRef.intensity = CalculateValueOnCurve();
                _timePassed += Time.deltaTime;
            }
        }
    }

    private void ResetFlashUtils()
    {
        _isTransitioning = false;
        _timePassed = 0;
    }

    private void SetupStartingIntensity()
    {
        _2dLighRef.intensity = _startingIntensity;
    }

    public void FlashLight()
    {
        if (_isTransitioning)
            ResetFlashUtils();

        _isTransitioning = true;
    }

    private float CalculateValueOnCurve()
    {
        if (_transitionDuration > 0)
            return _flashIntensityCurve.Evaluate(_timePassed / _transitionDuration);
        else return _flashIntensityCurve.Evaluate(_transitionDuration);
    }
}
