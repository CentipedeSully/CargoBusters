using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class WeaponFactory : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> _weaponPrefabs;
    [SerializeField] private Transform _removedWeaponsContainer;

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

        STKDebugLogger.LogWarning($"Error: Weapon not found amongst possible prefabs, {desiredWeapon}");
        return null;
    }

    public bool DoesWeaponExist(string weaponName)
    {
        foreach (GameObject prefab in _weaponPrefabs)
        {
            if (prefab.GetComponent<IShipWeaponry>().GetWeaponName() == weaponName)
                return true;
        }

        return false;
    }

    public Transform GetDecomissionedWeaponsContainer()
    {
        return _removedWeaponsContainer;
    }

    public void DeleteDecomissionedWeapons()
    {
        int weaponsCount = _removedWeaponsContainer.childCount;
        if (weaponsCount > 0)
        {
            for (int i = weaponsCount - 1; i >= 0; i--)
                Destroy(_removedWeaponsContainer.GetChild(i));
        }
    }

    public void DecomissionWeapon(GameObject weaponObject)
    {
        weaponObject.SetActive(false);
        weaponObject.transform.SetParent(_removedWeaponsContainer,false);
    }

}




