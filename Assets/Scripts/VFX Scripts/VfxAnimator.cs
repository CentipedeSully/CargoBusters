using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

public class VfxAnimator : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _floatFieldName = "Radius";
    [SerializeField] AnimationCurve _animationCurve;
    private float _maxAnimationDuration;
    [SerializeField] private float _currentDuration = 0;
    [Tooltip("If this is null, then this animator will try to find a vfx on this game object instead")]
    [SerializeField] private VisualEffect _vfxReference;
    private bool _isLerping = false;

    [Header("Events")]
    public UnityEvent OnAnimationCompleted;

    //Monobehaviors
    private void Awake()
    {
        if (_vfxReference == null)
            _vfxReference = GetComponent<VisualEffect>();
        _maxAnimationDuration = _animationCurve.keys[_animationCurve.length - 1].time;
    }

    private void Update()
    {
        Lerp();
    }


    //Utilites
    private void Lerp()
    {
        if (_isLerping)
        {
            //Set the vfx value to the curve's value based on the current time
            _vfxReference.SetFloat(_floatFieldName, Mathf.Lerp(_animationCurve.Evaluate(0),_animationCurve.Evaluate(_maxAnimationDuration),_currentDuration));
            _currentDuration += Time.deltaTime;

            if (_currentDuration >= _maxAnimationDuration)
            {
                ResetUtilities();
                OnAnimationCompleted?.Invoke();
            }
        }
    }

    public void InterruptAnimation()
    {
        ResetUtilities();
    }

    private void ResetUtilities()
    {
        _isLerping = false;
        _currentDuration = 0;
    }

    public void StartLerping()
    {
        _isLerping = true;
    }

}
