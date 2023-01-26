using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterReticuleController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _boolVisibilityName = "isVisible";
    [SerializeField] private Animator _reticuleAnimator;


    //Monobehaviors
    //...


    //Utilites
    public void ShowReticule()
    {
        _reticuleAnimator.SetBool(_boolVisibilityName, true);
    }

    public void HideReticule()
    {
        _reticuleAnimator.SetBool(_boolVisibilityName, false);
    }

}
