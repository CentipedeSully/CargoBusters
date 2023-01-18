using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimatorController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _triggerName = "OnExplosion";
    [SerializeField] private string _animExplosionStateName;
    [SerializeField] private float _animationDuration = 1;

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
            if (animationsList[i].name == _animExplosionStateName)
                _animationDuration = animationsList[i].length;
        }
    }

    public void TriggerExplosion()
    {
        GetComponent<Animator>().SetTrigger(_triggerName);
    }

    public float GetAnimationDuration()
    {
        return _animationDuration;
    }


}
