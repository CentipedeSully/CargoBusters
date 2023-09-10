using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ConnectVfxRadiusToColliderRadius : MonoBehaviour
{
    //Declarations
    private VisualEffect _vfxReference;
    [SerializeField] private string _vfxRadiusFieldName = "Radius";
    private CircleCollider2D _colliderRef;

    //Monos
    private void Awake()
    {
        _vfxReference = GetComponent<VisualEffect>();
        _colliderRef = GetComponent<CircleCollider2D>();

        LinkVFXRadiusToCollider();
    }

    private void Update()
    {
        LinkVFXRadiusToCollider();
    }

    //Utils
    private void LinkVFXRadiusToCollider()
    {
        _vfxReference.SetFloat(_vfxRadiusFieldName, _colliderRef.radius);
    }
}
