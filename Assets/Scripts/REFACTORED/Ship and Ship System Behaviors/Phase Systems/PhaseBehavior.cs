using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBehavior : ShipSubsystem, IPhaseSubsystemBehavior
{
    //Delcarations
    [Header("Phase Settings")]
    [SerializeField] private bool _phaseInput;
    [SerializeField] private bool _isPhasing;
    [SerializeField] private int _phaseCount;
    [SerializeField] private int _phaseMaximum;
    [SerializeField] private bool _isPhaseRecharging;
    [SerializeField] private float _phaseRechargeDuration;
    [SerializeField] private float _phaseRange = 3;

    [SerializeField] private int _phaseDodgeCost = 1;
    [SerializeField] private int _phaseShiftCost = 1;


    //Monobehaviors





    //Interface Utils
    public void SetPhaseInput(bool input)
    {
        _phaseInput = input;
    }

    public void DecreasePhases(int value)
    {
        if (value >= 0)
        {
            _phaseCount -= value;
            ClampPhaseCount();
        }
    }

    public void DisablePhasing()
    {
        DisableSubsystem();
    }

    public void EmptyPhases()
    {
        _phaseCount = 0;
    }

    public void EnablePhasing()
    {
        EnableSubsystem();
    }

    public int GetAvailablePhases()
    {
        return _phaseCount;
    }

    public int GetPhaseMax()
    {
        return _phaseMaximum;
    }

    public float GetRange()
    {
        return _phaseRange;
    }

    public void IncreasePhases(int value)
    {
        if (value >= 0)
        {
            _phaseCount += value;
            ClampPhaseCount();
        }
    }

    public void InitializeGameManagerDependentReferences()
    {
        //...
    }

    public bool IsPhaseAvailable()
    {
        return _phaseCount > 0;
    }

    public bool IsPhasingDisabled()
    {
        return IsSubsystemDisabled();
    }

    public void PhaseShift(int phaseCode)
    {
        if (_isPhasing == false)
            SelectPhaseActionBasedOnCode(phaseCode);

    }

    public void ReplenishPhases()
    {
        _phaseCount = _phaseMaximum;
    }

    public void SetParentShipAndInitializeAwakeReferences(AbstractShip parent)
    {
        //...
    }

    public void SetPhaseMax(int value)
    {
        if (value >=0)
        {
            _phaseMaximum = value;
            ClampPhaseCount();
        }
    }

    public void SetRange(float value)
    {
        if (value >= 0)
            _phaseRange = value;
    }





    //Utils
    private void ClampPhaseCount()
    {
        _phaseCount = Mathf.Clamp(_phaseCount, 0, _phaseMaximum);
    }

    private void SelectPhaseActionBasedOnCode(int code)
    {
        //code 0 is Dodge
        //Code 1 is FullShift
        switch (code)
        {
            case 0:
                if (_phaseCount >= _phaseDodgeCost)
                    PerformPhaseDodge();
                break;

            case 1:
                if (_phaseCount >= _phaseShiftCost)
                    PerformPhaseShift();
                break;

            default:
                break;

        }
    }

    private void PerformPhaseDodge()
    {
        //Pay cost and reflect state change
        _phaseCount -= _phaseDodgeCost;
        _isPhasing = true;



        //Perform Dodge

        //Activate Cooldown

    }

    private void PerformPhaseShift()
    {
        //Reduce Count

        //Perform Dodge

        //Activate Cooldown
    }

    private void ResetPhaseState()
    {
        _isPhaseRecharging = false;
        
    }
}
