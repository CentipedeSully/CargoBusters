using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterLightController : MonoBehaviour
{
    //Declarations
    [SerializeField] private Vector2 _moveDirection;
    [SerializeField] private List<LightToggler> _forwardsLightTogglers;
    [SerializeField] private List<LightToggler> _reverseLightTogglers;
    [SerializeField] private List<LightToggler> _rightStrafeLightTogglers;
    [SerializeField] private List<LightToggler> _leftStrafeLightTogglers;


    //Monos
    private void Update()
    {
        ControlLightsBasedOnInput();
    }


    //Utilities
    public void SetMoveInput(Vector2 directionalData)
    {
        _moveDirection = directionalData;
    }

    private void ControlLightsBasedOnInput()
    {

        if (_moveDirection.x > 0)
        {
            foreach (LightToggler lightSwitch in _rightStrafeLightTogglers)
                lightSwitch.TurnLightsOff();

            foreach (LightToggler lightSwitch in _leftStrafeLightTogglers)
                lightSwitch.TurnLightsOn();
        }

        else if (_moveDirection.x < 0)
        {
            foreach (LightToggler lightSwitch in _rightStrafeLightTogglers)
                lightSwitch.TurnLightsOn();

            foreach (LightToggler lightSwitch in _leftStrafeLightTogglers)
                lightSwitch.TurnLightsOff();
        }

        else
        {
            foreach (LightToggler lightSwitch in _rightStrafeLightTogglers)
                lightSwitch.TurnLightsOff();

            foreach (LightToggler lightSwitch in _leftStrafeLightTogglers)
                lightSwitch.TurnLightsOff();
        }


        if (_moveDirection.y > 0)
        {
            foreach (LightToggler lightSwitch in _reverseLightTogglers)
                lightSwitch.TurnLightsOff();

            foreach (LightToggler lightSwitch in _forwardsLightTogglers)
                lightSwitch.TurnLightsOn();
        }
        else if (_moveDirection.y < 0)
        {
            foreach (LightToggler lightSwitch in _reverseLightTogglers)
                lightSwitch.TurnLightsOn();

            foreach (LightToggler lightSwitch in _forwardsLightTogglers)
                lightSwitch.TurnLightsOff();
        }
        else
        {
            foreach (LightToggler lightSwitch in _reverseLightTogglers)
                lightSwitch.TurnLightsOff();

            foreach (LightToggler lightSwitch in _forwardsLightTogglers)
                lightSwitch.TurnLightsOff();
        }

    }


}
