using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButtonAnimController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _boolName = "OnClicked";
    private Animator _animatorRef;

    //monos
    private void Awake()
    {
        _animatorRef = GetComponent<Animator>();
    }

    //utils
    public void StopAttentionAnim()
    {
        _animatorRef.SetBool(_boolName,true);
    }

}
