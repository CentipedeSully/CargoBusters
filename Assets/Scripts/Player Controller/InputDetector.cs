using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using SullysToolkit;

public class InputDetector : MonoSingleton<InputDetector>
{
    [SerializeField] private Vector2 _moveInput;
    [SerializeField] private float _turnInput;
    [SerializeField] private bool _shootInput;
    [SerializeField] private bool _boostInput;
    [SerializeField] private bool _warpInput;
    
    //Read inputs from the Input Action via UnityEvents assigned from the Inspector
    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
            _moveInput = context.ReadValue<Vector2>();
        else _moveInput = Vector2.zero;
    }

    public void ReadShootInput(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
            _shootInput = true;
        else _shootInput = false;
    }

    public void ReadBoostInput(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
            _boostInput = true;
        else _boostInput = false;
    }

    public void ReadTurnInput(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
            _turnInput = context.ReadValue<float>();
        else _turnInput = 0;
    }

    public void ReadWarpInput(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
            _warpInput = true;
        else _warpInput = false;
    }


    //Getters
    public Vector2 GetMoveInput()
    {
        return _moveInput;
    }

    public bool GetShootInput()
    {
        return _shootInput;
    }

    public bool GetBoostInput()
    {
        return _boostInput;
    }

    public float GetTurnInput()
    {
        return _turnInput;
    }

    public bool GetWarpInput()
    {
        return _warpInput;
    }
}
