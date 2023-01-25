using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnimatorController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _chargingStateBoolName = "isBubbleCharging";
    [SerializeField] private string _fullnessStateBoolName = "isBubbleFull";
    private Animator _animatorRef;


    //Monobehaviors
    private void Awake()
    {
        _animatorRef = GetComponent<Animator>();
    }


    //Utilities

    //External Control Utils
    public void FillBubble()
    {
        StopChargingBubble();
        _animatorRef.SetBool(_fullnessStateBoolName, true);
    }

    public void DrainBubble()
    {
        StopChargingBubble();
        _animatorRef.SetBool(_fullnessStateBoolName, false);
    }

    public void ChargeBubble()
    {
        DrainBubble();
        _animatorRef.SetBool(_chargingStateBoolName, true);
    }

    public void StopChargingBubble()
    {
        _animatorRef.SetBool(_chargingStateBoolName, false);
    }

    //Getters
    public bool IsBubbleFull()
    {
        return _animatorRef.GetBool(_fullnessStateBoolName);
    }

    public bool IsBubbleCharging()
    {
        return _animatorRef.GetBool(_chargingStateBoolName);
    }
}
