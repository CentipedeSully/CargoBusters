using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> _weaponPrefabs;


    //Monobehaviours
    //...



    //Interface Utils
    //...




    //Utils
    public GameObject CreateWeaponObject(string desiredWeapon)
    {
        foreach (GameObject prefab in _weaponPrefabs)
        {
            if (prefab.GetComponent<IShipWeaponry>().GetWeaponName() == desiredWeapon)
                return Instantiate(prefab);
        }

        Debug.LogError($"Error: Weapon not found amongst possible prefabs, {desiredWeapon}");
        return null;
    }




}




