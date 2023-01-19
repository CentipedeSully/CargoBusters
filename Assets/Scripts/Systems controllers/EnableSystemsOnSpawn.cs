using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSystemsOnSpawn : MonoBehaviour
{
    //Declarations
    [SerializeField] private DisableColliders _colliderDisablerRef;
    [SerializeField] private SystemDisabler _systemDisablerRef;
    [SerializeField] private bool _isShipReady = false;
    [SerializeField] private string _spawnAnimStateName = "WarpIntro_anim";
    private float _animWaitDuration;

    //Monobehaviors
    private void Start()
    {
        FindAnimationDuration();
        DisableShipBehaviors();
        StartCoroutine(EnableShipBehaviorsAfterSpawn());
    }



    //Utilites
    private void FindAnimationDuration()
    {
        AnimationClip[] animationClipsCollection = GetComponent<Animator>().runtimeAnimatorController.animationClips;
        foreach (AnimationClip animClip in animationClipsCollection)
        {
            if (animClip.name == _spawnAnimStateName)
            {
                _animWaitDuration = animClip.length;
                break;
            }
        }

        if (_animWaitDuration == 0)
            Debug.LogError("Failed To retrieve animation duration. No animation of name '" + _spawnAnimStateName + "' found.");
    }

    private IEnumerator EnableShipBehaviorsAfterSpawn()
    {
        yield return new WaitForSeconds(_animWaitDuration);
        _isShipReady = true;
        EnableShipBehaviors();
    }

    public void DisableShipBehaviors()
    {
        _colliderDisablerRef.DisableCompositeCollider();
        _systemDisablerRef.DisableAllSystems();
    }

    public void EnableShipBehaviors()
    {
        _colliderDisablerRef.EnableCompositeCollider();
        _systemDisablerRef.EnableAllSystems();

    }



}
