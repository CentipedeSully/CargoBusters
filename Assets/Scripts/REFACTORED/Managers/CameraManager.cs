using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    //Declarations
    [SerializeField] private CinemachineVirtualCamera _virtualCameraReference;
    [SerializeField] private bool _isCameraFollowingTarget = false;
    [SerializeField] private Transform _followTarget;


    //Monobehaviors




    //Utils
    public Transform GetFollowTargetTransform()
    {
        return _followTarget;
    }

    public bool IsCameraFollowingTransform()
    {
        return _isCameraFollowingTarget;
    }

    public void SetFollowTargetTransform(Transform newTransform)
    {
        if (newTransform != null)
        {
            _isCameraFollowingTarget = true;
            _followTarget = newTransform;
        }
        else Debug.LogWarning($"Caution: Attempted to set a camera's follow target to a null transform. Ignoring request.");
    }

    private void FollowTargetTransform()
    {
        _virtualCameraReference.Follow = _followTarget;
    }

}
