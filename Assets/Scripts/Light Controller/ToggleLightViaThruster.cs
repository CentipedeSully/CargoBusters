using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLightViaThruster : MonoBehaviour
{
    //Declarations
    [SerializeField] private ThrusterToggler _thrusterTogglerRef;
    [SerializeField] private LightToggler _forwardsLightToggler;
    [SerializeField] private LightToggler _reverseLightToggler;
    [SerializeField] private LightToggler _rightStrafeLightToggler;
    [SerializeField] private LightToggler _leftStrafeLightToggler;


    //monobehaviors
    private void Update()
    {
        ControlForwardsThrusterLight();
        ControlReverseLights();
        ControlRightStrafeLights();
        ControlLeftStrafeLights();
    }




    //utilities
    private void ControlForwardsThrusterLight()
    {
        if (_forwardsLightToggler != null)
        {
            if (_thrusterTogglerRef.IsForwardsThrustersOn())
                _forwardsLightToggler.TurnLightsOn();
            else _forwardsLightToggler.TurnLightsOff();
        }
    }

    private void ControlReverseLights()
    {
        if (_reverseLightToggler != null)
        {
            if (_thrusterTogglerRef.IsReversethrustersOn())
                _reverseLightToggler.TurnLightsOn();
            else _reverseLightToggler.TurnLightsOff();
        }
    }
   
    private void ControlRightStrafeLights()
    {
        if (_rightStrafeLightToggler != null)
        {
            if (_thrusterTogglerRef.IsRightStrafeThrustersOn())
                _rightStrafeLightToggler.TurnLightsOn();
            else _rightStrafeLightToggler.TurnLightsOff();
        }
    }

    private void ControlLeftStrafeLights()
    {
        if (_leftStrafeLightToggler != null)
        {
            if (_thrusterTogglerRef.IsLeftStrafeThrustersOn())
                _leftStrafeLightToggler.TurnLightsOn();
            else _leftStrafeLightToggler.TurnLightsOff();
        }
    }




}
