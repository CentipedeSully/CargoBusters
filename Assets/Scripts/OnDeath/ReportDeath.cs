using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportDeath : MonoBehaviour
{
    [SerializeField] private InstanceCounter _instanceCounterRef;


    private void Awake()
    {
        _instanceCounterRef = GameObject.Find("Game Management Utilities").GetComponent<InstanceCounter>();
    }

    public void ReportDeathToCounter()
    {
        _instanceCounterRef.DecrementCount();
    }
}
