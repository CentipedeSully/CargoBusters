using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RandomizeVFXPropertyOnStart : MonoBehaviour
{
    //Declarations
    [Header("Randomize Int Property Settings")]
    [SerializeField] private bool _isRandomizeIntPropertyEnabled = false;
    [SerializeField] private int _minValueint;
    [SerializeField] private int _maxValueInt;
    [SerializeField] private int _randomizedValueInt;
    [SerializeField] private string _vfxFieldNameInt = "ParticleSpawnRate";

    [Header("Randomize Float Property Settings")]
    [SerializeField] private bool _isRandomizeFloatPropertyEnabled = false;
    [SerializeField] private float _minValueFloat;
    [SerializeField] private float _maxValueFloat;
    [SerializeField] private float _randomizedValueFloat;
    [SerializeField] private string _vfxFieldNameFloat = "Particle Lifetime";


    private VisualEffect _vfxReference;
    private VisualEffectController _vfxControllerRef;



    //Monobehaviors
    private void Awake()
    {
        _vfxReference = GetComponent<VisualEffect>();
        _vfxControllerRef = GetComponent<VisualEffectController>();
    }

    private void Start()
    {
        TryRandomizeIntProperty();
        TryRandomizeFloatProperty();
    }

    //Utilites
    private void TryRandomizeIntProperty()
    {
        if (_isRandomizeIntPropertyEnabled)
        {
            _randomizedValueInt = Random.Range(_minValueint, _maxValueInt);
            _vfxReference.SetInt(_vfxFieldNameInt, _randomizedValueInt);

            //Save new value to controller if it exists
            if (_vfxControllerRef != null)
                _vfxControllerRef.SetMaxCount(_randomizedValueInt);
        }
    }

    private void TryRandomizeFloatProperty()
    {
        if (_isRandomizeFloatPropertyEnabled)
        {
            _randomizedValueFloat = Random.Range(_minValueFloat, _maxValueFloat);
            _vfxReference.SetFloat(_vfxFieldNameFloat, _randomizedValueFloat);

            //VFXController doesn't support and float-field properties. No need to set anything 
        }
    }
}
