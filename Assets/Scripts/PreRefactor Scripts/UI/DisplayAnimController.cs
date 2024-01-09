using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAnimController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isEnabled = true;
    [SerializeField] private string _boolVisiblityName = "isVisible";
    [SerializeField] private string _triggerPositiveName = "OnPositive";
    [SerializeField] private bool _doesTriggerExist = false;
    private bool _isVisible = false;
    private Animator _animatorRef;



    //Monobehaviors
    private void Awake()
    {
        _animatorRef = GetComponent<Animator>();
    }



    //Utilies
    public void EnableDisplay()
    {
        _isEnabled = true;
    }

    public void DisableDisplay()
    {
        _isEnabled = false;
    }

    public void ShowDisplay()
    {
        if (_isEnabled)
        {
            _animatorRef.SetBool(_boolVisiblityName, true);
            _isVisible = true;
        }
    }

    public void HideDisplay()
    {
        if (_isEnabled)
        {
            _animatorRef.SetBool(_boolVisiblityName, false);
            _isVisible = false;
        }
    }

    public void TriggerPositiveEffect()
    {
        if (_isEnabled)
        {
            if (_doesTriggerExist)
                _animatorRef.SetTrigger(_triggerPositiveName);

        }
    }

    public void ToggleDisplay()
    {
        if (_isEnabled)
        {
            if (_isVisible)
            {
                HideDisplay();
                _isVisible = false;
            }
            else
            {
                ShowDisplay();
                _isVisible = true;
            }
        }
     
    }

}
