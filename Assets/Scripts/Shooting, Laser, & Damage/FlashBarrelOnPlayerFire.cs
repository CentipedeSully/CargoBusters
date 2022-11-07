using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBarrelOnPlayerFire : MonoBehaviour
{
    //Decalrations
    private BarrelFlashController _barrelFlashControllerRef;



    //Monos
    private void Awake()
    {
        _barrelFlashControllerRef = GetComponent<BarrelFlashController>();
    }

    private void Update()
    {
        CommunicateShotInputToBarrelController();
    }




    //Utilites
    private void CommunicateShotInputToBarrelController()
    {
        _barrelFlashControllerRef.SetShotInput(InputDetector.Instance.GetShootInput());
    }



}
