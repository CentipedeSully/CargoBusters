using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControllerViaAi : AbstractShipController
{
    //Declarations
    [Header("Calculated Input Decisions")]
    [SerializeField] protected bool _fireCommand = false;
    [SerializeField] [Range(-1,1)] protected int _thrustInput = 0;
    [SerializeField] [Range(-1, 1)] protected int _strafeInput = 0;
    [SerializeField] [Range(-1, 1)] protected int _turnInput = 0;


    [Header("Targeting & Behaviour Settings")]
    [SerializeField] protected GameObject _targetObject;
    //[SerializeField] protected bool _isHostile = false;

    protected IEngineSubsystemBehavior _engineBehaviorRef;
    protected IWeaponsSubsystemBehavior _weaponBehaviorRef;
    protected ScannerBehaviour _scannerBehaviourRef;

    //Monobehaviors





    //Internal Utils
    protected override void InitializeMoreReferences()
    {
        _engineBehaviorRef = _parent.GetEnginesBehavior();
        _weaponBehaviorRef = _parent.GetWeaponsBehavior();
        _scannerBehaviourRef = _parent.GetScannerBehavior();
    }

    protected virtual void TargetClosestScannable()
    {
        //Get Closest Scan that's an enemy
    }

    protected virtual void PursueTarget()
    {
        //if in range, shoot at target
        
        //Adjust range from target: move towards if too far, back up if too close
    }



    //Getters Setters & Commands
    public override void CommunicateDecisionsToSubsystems()
    {
        _engineBehaviorRef.SetStrafeInput(_strafeInput);
        _engineBehaviorRef.SetThrustInput(_thrustInput);
        _engineBehaviorRef.SetTurnInput(_turnInput);

        _weaponBehaviorRef.SetShootInput(_fireCommand);
    }

    public override void DetermineDecisions()
    {
        if (_targetObject == null)
            TargetClosestScannable();

        else
            PursueTarget();
    }
}
