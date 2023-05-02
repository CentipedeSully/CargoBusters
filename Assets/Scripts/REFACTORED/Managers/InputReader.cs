using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    //Declarations
    [SerializeField] private float _thrustInput;
    [SerializeField] private float _strafeInput;
    [SerializeField] private float _turnInput;
    [SerializeField] private bool _shootInput;



    //Monobehaviours
    //...




    //Utils
    public void ReadPlayerThrustAndStrafeInput(InputAction.CallbackContext context)
    {
        _thrustInput = context.ReadValue<Vector2>().y;
        _strafeInput = context.ReadValue<Vector2>().x;
    }

    public void ReadPlayerTurnInput(InputAction.CallbackContext context)
    {
        _turnInput = context.ReadValue<float>();
    }

    public void ReadPlayerShootInput(InputAction.CallbackContext context)
    {
        _shootInput = context.ReadValueAsButton();
    }


    public float GetPlayerThrustInput()
    {
        return _thrustInput;
    }

    public float GetPlayerStrafeInput()
    {
        return _strafeInput;
    }

    public float GetPlayerTurnInput()
    {
        return _turnInput;
    }

    public bool GetPlayerShootInput()
    {
        return _shootInput;
    }


}
