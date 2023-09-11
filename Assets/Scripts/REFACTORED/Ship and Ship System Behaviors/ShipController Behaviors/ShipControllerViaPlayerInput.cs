using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControllerViaPlayerInput : MonoBehaviour, IShipController
{
    //Declarations
    [SerializeField] private bool _isInitialized = false;
    [SerializeField] private bool _showDebug = false;

    //references
    private AbstractShip _parent;
    private IEngineSubsystemBehavior _engineBehaviorRef;
    private IWeaponsSubsystemBehavior _weaponBehaviorRef;
    private InputReader _inputReaderReference;




    //Monbehaviours
    //...


    //Interface Utils
     public void InitializeReferences(AbstractShip parent)
    {
        _parent = parent;
        _engineBehaviorRef = _parent.GetEnginesBehavior();
        _weaponBehaviorRef = _parent.GetWeaponsBehavior();
        _inputReaderReference = GameManager.Instance.GetInputReader();
        _isInitialized = true;
    }

    public void RemoveController()
    {
        Destroy(this);
    }

    public void DetermineDecisions()
    {
        //Decisions are made by the InputSystem via UnityEvents. Not Here.
        //...
    }

    public void CommunicateDecisionsToSubsystems()
    {
        if (_isInitialized)
        {
            _engineBehaviorRef.SetStrafeInput(_inputReaderReference.GetPlayerStrafeInput());
            _engineBehaviorRef.SetThrustInput(_inputReaderReference.GetPlayerThrustInput());
            _engineBehaviorRef.SetTurnInput(_inputReaderReference.GetPlayerTurnInput());

            _weaponBehaviorRef.SetShootInput(_inputReaderReference.GetPlayerShootInput());
        }

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
