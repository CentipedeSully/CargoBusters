using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputToShipController : MonoBehaviour, IShipController
{
    //Declarations
    [Header("Ship References(AutoFilled On Play)")]
    [SerializeField] private Ship _parent;
    private IEngineBehavior _engineBehaviorRef;
    [SerializeField] private InputReader _inputReaderReference;
    [SerializeField] private bool _isDebugActive = false;



    //Monbehaviours
    private void Awake()
    {
        _inputReaderReference = GameManager.Instance.GetInputReader();
        
    }


    //Interface Utils
    public void SetParent(Ship parent)
    {
        _parent = parent;
        _engineBehaviorRef = _parent.GetEngineBehavior();
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
        if (_isDebugActive)
            _isDebugActive = false;
        else _isDebugActive = true;
    }

    public bool IsDebugActive()
    {
        return _isDebugActive;
    }
}
