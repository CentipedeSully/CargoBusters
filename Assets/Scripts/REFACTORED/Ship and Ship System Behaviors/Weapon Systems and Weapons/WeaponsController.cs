using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponsController : ShipSubsystem, IWeaponsSubsystemBehavior
{
    //Declarations
    [Header("Weapons")]
    [SerializeField] private int _weaponCount;
    [SerializeField] private List<Transform> _weaponPositions;
    [SerializeField] private AbstractShipWeapon[] _weaponsReferenceList;
    [SerializeField] private WeaponFactory _weaponsFactoryReference;

    //Monobehaviours
    //...


    //Interface Utils
    public void AddWeapon(string weaponName, int position)
    {
        if (position >= 0 && position < _weaponPositions.Count)
        {
            if (GetWeaponFromPosition(position) == null)
            {
                //Ask the factory to create a new weapon
                GameObject newWeaponObject = _weaponsFactoryReference.CreateWeaponObject(weaponName);

                //bind the new weapon to the desired position.
                newWeaponObject.transform.SetParent(_weaponPositions[position]);
                newWeaponObject.transform.position = _weaponPositions[position].position;

                //Update the weapon count and weapon references
                RebuildWeaponReferences();
                UpdateWeaponCountUsingWeaponReferences();
            }
            else
                Debug.LogWarning($"Caution: position {position} already holds a weapon on ship {_parentShip.GetName()}. " +
                    $"Ignoring request to add weapon {weaponName} to position {position}");
        }
        else
            Debug.LogWarning($"Caution: position {position} doesn't exist on ship {_parentShip.GetName()}" +
                $"Ignoring request to add weapon {weaponName} to position {position}");
        
    }

    public IShipWeaponry RemoveWeapon(int position)
    {
        IShipWeaponry removedWeapon = null;
        if (position >= 0 && position < _weaponsReferenceList.Length)
        {
            removedWeapon = _weaponsReferenceList[position];
            if (removedWeapon == null)
                Debug.LogWarning($"Caution: no weapon exists at position {position} on ship {_parentShip.GetName()}." +
                    $"Ignoring request to remove weapon from position {position}");
            else
            {
                //remove the weapon from its position, rebuild the weapon reference list, and the update the weapons count

                Destroy(_weaponPositions[position].GetChild(0));

                RebuildWeaponReferences();
                UpdateWeaponCountUsingWeaponReferences();
            }
        }
        else
            Debug.LogWarning($"Caution: Attempted OutOfBounds removal of weapon at position {position} on {_parentShip.GetName()}." +
                $"Ignoring request to remove weapon from position {position}");

        return removedWeapon;
        
    }

    public void DisableWeapons()
    {
        DisableSubsystem();
    }

    public void EnableWeapons()
    {
        EnableSubsystem();
    }

    public void FireWeapons()
    {
        foreach (IShipWeaponry weapon in _weaponsReferenceList)
        {
            if (weapon != null)
                weapon.FireWeapon();
        }
    }

    public int GetWeaponCount()
    {
        return _weaponCount;
    }

    public void InitializeGameManagerDependentReferences()
    {
        _weaponsFactoryReference = GameManager.Instance.GetWeaponsFactory();
    }

    public bool IsWeaponsDisabled()
    {
        return _isDisabled;
    }

    public void SetParentShipAndInitializeAwakeReferences(AbstractShip parent)
    {
        _parentShip = parent;
        RebuildWeaponReferences();
        UpdateWeaponCountUsingWeaponReferences();
    }

    public IShipWeaponry GetWeaponFromPosition(int position)
    {
        if (position >= 0 && position < _weaponsReferenceList.Length)
            return _weaponsReferenceList[position];
        else
        {
            Debug.LogWarning($"Position {position} is out of bounds of the Subsystem:{GetName()} on ship {_parentShip.GetName()}");
            return null;
        }
            
    }

    public int GetPositionCount()
    {
        return _weaponPositions.Count;
    }

    public int GetAvailablePosition()
    {
        for (int i = 0; i < _weaponsReferenceList.Length; i++)
        {
            if (_weaponsReferenceList[i] == null)
                return i;
        }

        return -1;
    }




    //Utils
    public override void DisableSubsystem()
    {
        foreach (IShipWeaponry weapon in _weaponsReferenceList)
        {
            if (weapon != null)
                weapon.SetSubsystemOnlineStatus(false);
        }

        base.DisableSubsystem();
    }

    public override void EnableSubsystem()
    {
        foreach (IShipWeaponry weapon in _weaponsReferenceList)
        {
            if (weapon != null)
                weapon.SetSubsystemOnlineStatus(true);
        }

        base.EnableSubsystem();
    }

    public void RebuildWeaponReferences()
    {
        _weaponsReferenceList = new AbstractShipWeapon[_weaponPositions.Count];

        for (int i = 0; i < _weaponPositions.Count; i++)
            _weaponsReferenceList[i] = _weaponPositions[i].GetChild(0).gameObject.GetComponent<AbstractShipWeapon>();
    }

    public void UpdateWeaponCountUsingWeaponReferences()
    {
        int newWeaponCount = 0;
        foreach (IShipWeaponry weapon in _weaponsReferenceList)
        {
            if (weapon != null)
                newWeaponCount++;
        }

        _weaponCount = newWeaponCount;
    }


    //Debugging
    private void LogEquiptWeapons()
    {
        //int slotCount = 0;
        //foreach(IShipWeaponry weapon in _weaponsArray)
        //{
        //    if (weapon != null)
        //        Debug.Log($"{weapon.GetWeaponName()} equipt to slot {slotCount}");
        //    slotCount++;
        //}
    }

    //Testing AddWeapon.
    //Add new weapon
    //Add over preexisint weapon
    private void AddWeaponTest()
    {


    }

    //Testing RemoveWeapon
    //Remove valid weapon
    //remove nonexistent weapon

    //Test Enabling/disabling weapons
    //shoot when enabled
    //shoot when disabled

    //Test Getting weapons from slots
    //add weapons to arbitrary slots and then
    //log all weapon slots using GetWeaponFromSlot




}
