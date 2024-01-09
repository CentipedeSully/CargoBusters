using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpAnimatorController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _triggerName = "OnWarp";
    [SerializeField] private string _animStateName;
    [SerializeField] private float _animationDuration;

    //monos
    private void Awake()
    {
        FindAnimationDuration();
    }


    //Utilites
    private void FindAnimationDuration()
    {
        AnimationClip[] animationsList = GetComponent<Animator>().runtimeAnimatorController.animationClips;

        for (int i = 0; i < animationsList.Length; i++)
        {
            if (animationsList[i].name == _animStateName)
                _animationDuration = animationsList[i].length;
        }
    }

    public void TriggerWarpAnimation()
    {
        GetComponent<Animator>().SetTrigger(_triggerName);
    }

    public float GetAnimationDuration()
    {
        return _animationDuration;
    }
}
