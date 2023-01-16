using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightToggler : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<Light2D> _Lights;
    [SerializeField] private int _maxIntensity = 5;
    [SerializeField] private int _minIntensity = 0;




    //Monobehaviors
    //...




    //Utilities
    public void TurnLightsOn()
    {
        foreach (Light2D light in _Lights)
            light.intensity = _maxIntensity;
    }

    public void TurnLightsOff()
    {
        foreach (Light2D light in _Lights)
            light.intensity = _minIntensity;
    }




}
