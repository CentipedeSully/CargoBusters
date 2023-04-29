using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControllerViaPlayerInput : MonoBehaviour, IShipController
{
    //Declarations
    [SerializeField] private bool _showDebug = false;

    //references
    private Ship _parent;
    private IEngineSubsystemBehavior _engineBehaviorRef;
    private InputReader _inputReaderReference;




    //Monbehaviours
    //...


    //Interface Utils
    public void SetParentShipAndInitializeAwakeReferences(Ship parent)
    {
        _parent = parent;
        _engineBehaviorRef = _parent.GetEnginesBehavior();
    }

    public void InitializeGameManagerDependentReferences()
    {
        _inputReaderReference = GameManager.Instance.GetInputReader();
    }


    public void DetermineDecisions()
    {
        //Decisions are made by the InputSystem via UnityEvents. Not Here.
        //...
    }

    public void CommunicateDecisionsToSubsystems()
    {
        _engineBehaviorRef.SetStrafeInput(_inputReaderReference.GetPlayerStrafeInput());
        _engineBehaviorRef.SetThrustInput(_inputReaderReference.GetPlayerThrustInput());
        _engineBehaviorRef.SetTurnInput(_inputReaderReference.GetPlayerTurnInput());
    }




    //Utils
    //...


    //Debugging
    private void LogResponse(string responseDescription)
    {
        if (_parent != null)
            Debug.Log(_parent.GetName() + " " + responseDescription);
        else
        {
            Debug.LogError($"NULL_PARENT_SHIP on obejct: {this.gameObject}, {this}");
            Debug.Log("NULL_SHIP " + responseDescription);
        }
    }

    public void ToggleDebug()
    {
        if (_showDebug)
            _showDebug = false;
        else _showDebug = true;
    }

    public bool IsDebugActive()
    {
        return _showDebug;
    }
}
