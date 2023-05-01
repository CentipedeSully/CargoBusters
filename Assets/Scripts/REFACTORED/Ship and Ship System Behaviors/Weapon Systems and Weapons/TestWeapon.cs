using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : AbstractShipWeapon
{
    //Declarations
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private InstanceTracker _instanceTrackerRef;


    //Monobehavior



    //Interface Utils
    //...


    //Utils
    protected override void FireProjectile()
    {
        //Create Projectile and
        //set projectile container as parent transform

        Debug.Log($"{_weaponName} fired a Projectile");

        //
    }



    //DEbugging




}
