using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelfDestructController : MonoBehaviour
{
    //Declarations
    [Header("Events")]
    public UnityEvent OnSelfDestruct;


    //Monobehaviors
    //...


    //Utilites
    public void TriggerSelfDestruct()
    {
        OnSelfDestruct?.Invoke();
    }


}
