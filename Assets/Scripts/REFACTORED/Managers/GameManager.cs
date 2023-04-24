using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SullysToolkit;

public class GameManager : MonoSingleton<GameManager>
{
    //Declarations
    [SerializeField] private InputReader _inputReaderReference;




    //Monobehaviours
    //...



    //Utils
    protected override void InitializeAdditionalFields()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        if (_inputReaderReference == null)
            _inputReaderReference = GetComponent<InputReader>();
    }



    //Getters & Setters
    public InputReader GetInputReader()
    {
        return _inputReaderReference;
    }





}
