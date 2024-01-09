using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbstractShipController: MonoBehaviour, IShipController
{
    //Declarations
    [SerializeField] protected bool _isInitialized = false;
    [SerializeField] protected bool _showDebug = false;

    //references
    protected AbstractShip _parent;




    //Monbehaviours
    //...




    //Internal Utils
    protected virtual void InitializeMoreReferences()
    {

    }





    //Getters, Setters, & Commands
    public virtual void InitializeReferences(AbstractShip parent)
    {
        _parent = parent;
        InitializeMoreReferences();
        _isInitialized = true;
    }


    public virtual void RemoveController()
    {
        Destroy(this);
    }

    public abstract void DetermineDecisions();

    public abstract void CommunicateDecisionsToSubsystems();





    //Debugging
    protected virtual void LogResponse(string responseDescription)
    {
        if (_parent != null)
            Debug.Log(_parent.GetName() + " " + responseDescription);
        else
        {
            Debug.LogError($"NULL_PARENT_SHIP on obejct: {this.gameObject}, {this}");
            Debug.Log("NULL_SHIP " + responseDescription);
        }
    }

    public virtual void ToggleDebug()
    {
        if (_showDebug)
            _showDebug = false;
        else _showDebug = true;
    }

    public virtual bool IsDebugActive()
    {
        return _showDebug;
    }

}



public class ShipControllerViaPlayerInput : AbstractShipController
{
    //Declarations
    protected IEngineSubsystemBehavior _engineBehaviorRef;
    protected IWeaponsSubsystemBehavior _weaponBehaviorRef;
    protected InputReader _inputReaderReference;



    //Monobehaviors
    //...




    //Internal Utils
    protected override void InitializeMoreReferences()
    {
        _engineBehaviorRef = _parent.GetEnginesBehavior();
        _weaponBehaviorRef = _parent.GetWeaponsBehavior();
        _inputReaderReference = GameManager.Instance.GetInputReader();
    }




    //Getters Setters, & commands
    public override void DetermineDecisions()
    {
        //Inputs determined via player input Reader
    }

    public override void CommunicateDecisionsToSubsystems()
    {
        if (_isInitialized)
        {
            _engineBehaviorRef.SetStrafeInput(_inputReaderReference.GetPlayerStrafeInput());
            _engineBehaviorRef.SetThrustInput(_inputReaderReference.GetPlayerThrustInput());
            _engineBehaviorRef.SetTurnInput(_inputReaderReference.GetPlayerTurnInput());

            _weaponBehaviorRef.SetShootInput(_inputReaderReference.GetPlayerShootInput());
        }

    }




}
