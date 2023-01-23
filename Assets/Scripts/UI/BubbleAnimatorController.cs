using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnimatorController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _isFillingBool = "isBubbleFilling";
    [SerializeField] private string _isFullBool = "isBubbleFull";
    private Animator _animatorRef;


    //Monobehaviors



    //Utilities
    public void FillBubble()
    {
        _animatorRef.SetBool(_isFullBool, true);
    }

    public void StartFillingBubble()
    {
        _animatorRef.SetBool(_isFillingBool, true);
    }

    //empty
    //endcharge//

}
