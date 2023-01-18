using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpExplosionAnimatorController : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _triggerName = "OnExplosion";


    //monos



    //Utilites
    public void TriggerExplosion()
    {
        GetComponent<Animator>().SetTrigger(_triggerName);
    }

}
