using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponsController : ShipSubsystem, IWeaponsSubsystemBehavior
{
    //Declarations
    [Header("Weapons")]
    [SerializeField] [Min(0)]private int _weaponSlots = 4;
    [SerializeField] private IShipWeaponry[] _weaponsArray;



    //Monobehaviours
    //...


    //Interface Utils
    public void AddWeapon(IShipWeaponry newWeapon, int slot)
    {
        if (slot >= 0 && slot < _weaponsArray.Length)
        {
            if (GetWeaponFromSlot(slot) == null)
                _weaponsArray[slot] = newWeapon;
            else
                Debug.LogError($"Error: Attempted to add {newWeapon.GetWeaponName()} at slot {slot} when the slot " +
                    $"is already occupied by {_weaponsArray[slot].GetWeaponName()}. Remove the current weapon before" +
                    $"adding a new weapon.");
        }
    }

    public IShipWeaponry RemoveWeapon(int slot)
    {
        IShipWeaponry removedWeapon = null;
        if (slot >= 0 && slot < _weaponsArray.Length)
        {
            removedWeapon = _weaponsArray[slot];
            _weaponsArray[slot] = null;
        }
        else
            Debug.LogWarning($"Caution: Attempted to remove a nonexistent weapon from slot {slot} on {_parentShip.GetName()}.");

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
        foreach (IShipWeaponry weapon in _weaponsArray)
        {
            if (weapon != null)
                weapon.FireWeapon();
        }
    }

    public int GetWeaponCount()
    {
        int weaponCount = 0;
        foreach (IShipWeaponry weapon in _weaponsArray)
        {
            if (weapon != null)
                weaponCount++;
        }

        return weaponCount;
    }

    public void InitializeGameManagerDependentReferences()
    {
        //...
    }

    public bool IsWeaponsDisabled()
    {
        return _isDisabled;
    }

    public void SetParentShipAndInitializeAwakeReferences(AbstractShip parent)
    {
        _parentShip = parent;
        _weaponsArray = new IShipWeaponry[4];

        FillSlotsWithDefaultWeapons();
    }

    public IShipWeaponry GetWeaponFromSlot(int slot)
    {
        if (slot >= 0 && slot < _weaponsArray.Length)
            return _weaponsArray[slot];
        else return null;
    }

    public int GetSlotCount()
    {
        return _weaponsArray.Length;
    }




    //Utils
    public override void DisableSubsystem()
    {
        foreach (IShipWeaponry weapon in _weaponsArray)
        {
            if (weapon != null)
                weapon.SetSubsystemOnlineStatus(false);
        }

        base.DisableSubsystem();
    }

    public override void EnableSubsystem()
    {
        foreach (IShipWeaponry weapon in _weaponsArray)
        {
            if (weapon != null)
                weapon.SetSubsystemOnlineStatus(true);
        }

        base.EnableSubsystem();
    }

    private void FillSlotsWithDefaultWeapons()
    {
        if (_weaponSlots > 0)
        {
            //Get any weapons already attached to the gameobject
            IShipWeaponry[] defaultWeapons = GetComponents<IShipWeaponry>();

            //only equipt up to the amount of slots available
            int indexLimit = Mathf.Clamp(defaultWeapons.Length, 0, _weaponSlots);

            for (int i = 0; i < indexLimit; i++)
                AddWeapon(defaultWeapons[i], i);
            
        }

    }


    //Debugging
    private void LogEquiptWeapons()
    {
        foreach(IShipWeaponry weapon in _weaponsArray)
        {
            if (weapon != null)
                Debug.Log($"{weapon.GetWeaponName()} equipt");
        }
    }

    //Testing AddWeapon.
    //Add new weapon
    //Add over preexisint weapon

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
