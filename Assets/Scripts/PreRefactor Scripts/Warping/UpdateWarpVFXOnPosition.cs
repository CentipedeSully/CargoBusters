using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UpdateWarpVFXOnPosition : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _positionName = "Spawn Position";
    private VisualEffect _vfxReference;


    //Monos
    private void Awake()
    {
        _vfxReference = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        _vfxReference.SetVector3(_positionName, transform.position);
    }


    //Utilities
    //...





}
