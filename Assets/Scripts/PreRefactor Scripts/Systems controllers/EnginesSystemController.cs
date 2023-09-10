using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnginesSystemController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isEngineOnline = true;
    [SerializeField] private Vector2 _moveInput;
    [SerializeField] private float _turnInput;

    [Header("Events")]
    public UnityEvent<Vector2> OnMoveSignal;
    public UnityEvent<Vector3> OnTurnSignal;

    //Monos
    private void Update()
    {
        if (_isEngineOnline)
        {
            OnMoveSignal?.Invoke(_moveInput);
            OnTurnSignal?.Invoke(new Vector3(0,0,_turnInput));
        }
            
        else
        {
            OnMoveSignal?.Invoke(Vector2.zero);
            OnTurnSignal?.Invoke(Vector3.zero);
        }
            
    }


    //utilites
    public void DisableEngines()
    {
        _isEngineOnline = false;
    }

    public void EnableEngines()
    {
        _isEngineOnline = true;
    }

    public void SetMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    public void SetTurnInput(float turnInput)
    {
        _turnInput = turnInput;
    }

}
