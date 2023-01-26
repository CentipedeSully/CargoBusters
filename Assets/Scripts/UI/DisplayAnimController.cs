using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAnimController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _boolVisiblityName = "isVisible";
    [SerializeField] private string _triggerPositiveName = "OnPositive";
    [SerializeField] private bool _doesTriggerExist = false;
    private Animator _animatorRef;



    //Monobehaviors
    private void Awake()
    {
        _animatorRef = GetComponent<Animator>();
    }



    //Utilies
    public void ShowDisplay()
    {
        _animatorRef.SetBool(_boolVisiblityName, true);
    }

    public void HideDisplay()
    {
        _animatorRef.SetBool(_boolVisiblityName, false);
    }

    public void TriggerPositiveEffect()
    {
        if (_doesTriggerExist)
            _animatorRef.SetTrigger(_triggerPositiveName);
            
    }

}
