using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VisualEffectController : MonoBehaviour, IVisualEffectToggler
{
    //Declarations
    [SerializeField] private string _vfxFieldName;
    [SerializeField] private int _maxParticleCount;
    [SerializeField] private int _minParticleCount = 0;
    private VisualEffect _vfxReference;


    //Monobehaviors
    private void Awake()
    {
        _vfxReference = GetComponent<VisualEffect>();
    }





    //Utilities
    public void ActivateVisualEffect()
    {
        _vfxReference.SetInt(_vfxFieldName, _maxParticleCount);
    }


    public void DeactivateVisualEffect()
    {
        _vfxReference.SetInt(_vfxFieldName, _minParticleCount);
    }


}
