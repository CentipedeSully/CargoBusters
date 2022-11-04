using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpOverCurve : MonoBehaviour
{
    //Delcarations
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _lerpDuration = 5;
    [SerializeField] private float _startValue = 0;
    [SerializeField] private float _currentValue = 0;
    [SerializeField] private float _endValue = 0;
    [SerializeField] private float _timePassed = 0;

    [SerializeField] private bool _isLerping = false;


    //Monos

    private void Update()
    {
        Lerp();
            
    }


    //Utils
    private void Lerp()
    {
        if (_isLerping)
        {
            _startValue = _curve.Evaluate(0);
            _endValue = _curve.Evaluate(_lerpDuration);
            _currentValue = Mathf.Lerp(_startValue,_endValue, _curve.Evaluate(_timePassed));


            CountTimePassed();
        }
    }

    private void CountTimePassed()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed >= _lerpDuration)
            ResetUtils();
    }

    private void ResetUtils()
    {
        _timePassed = 0;
        _currentValue = 0;

        _isLerping = false;
    }

}
