using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControllerViaAi : AbstractShipController
{
    //Declarations
    [Header("Calculated Input Decisions")]
    [SerializeField] protected bool _fireWeapons = false;
    [SerializeField] [Range(-1,1)] protected int _thrustInput = 0;
    [SerializeField] [Range(-1, 1)] protected int _strafeInput = 0;


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
        //Get Scan that's an enemy
    }

    protected virtual void PursueTarget()
    {

    }



    //Getters Setters & Commands
    public override void CommunicateDecisionsToSubsystems()
    {
        //
    }

    public override void DetermineDecisions()
    {
        if (_targetObject == null)
            TargetClosestScannable();

        else
            PursueTarget();
    }
}
