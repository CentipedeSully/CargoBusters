using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MakeVFXFollowTransform : MonoBehaviour
{
    //Declarations
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private string _vfxFieldNameVector3 = "Position";
    private VisualEffect _vfxReference;



    //monobehaviors
    private void Awake()
    {
        _vfxReference = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        FollowTargetTransform();
    }


    //utilites
    private void FollowTargetTransform()
    {
        _vfxReference.SetVector3(_vfxFieldNameVector3, _targetTransform.position);
    }


}
