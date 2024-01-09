using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemLightsAnimatorController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _boolParamName = "isHullCritical";


    //Monos



    //Utils
    public void TriggerHullCriticalLights()
    {
        GetComponent<Animator>().SetBool(_boolParamName, true);
    }

    public void EndHullCriticalLights()
    {
        GetComponent<Animator>().SetBool(_boolParamName, false);
    }


}
