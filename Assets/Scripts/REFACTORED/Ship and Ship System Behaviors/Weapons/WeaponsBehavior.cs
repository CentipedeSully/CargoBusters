using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IShipWeaponry
{
    void FireWeapon();

    void SetParentSubsystemAndInitialize(IWeaponsSubsystemBehavior weaponSubsystem);

    string GetWeaponName();

    int GetDamage();

    float GetCooldown();

    string GetWeaponType();

    void SetDamage(int value);

    void SetCooldown(float value);

    void SetSubsystemOnlineStatus(bool newStatus);

}

public abstract class ShipWeapon : MonoBehaviour, IShipWeaponry
{
    //Declarations
    [SerializeField] protected string _weaponName = "Unnamed Weapon";
    [SerializeField] protected string _weaponType = "Undefined Type";
    [SerializeField] protected int _damage;
    [SerializeField] protected float _cooldown = .5f;
    [SerializeField] protected bool _isWeaponReady = true;
    [SerializeField] protected bool _isSubsystemOnline = true;
    [SerializeField] protected IWeaponsSubsystemBehavior _parentSubsystem;




    //Monobehaviours
    //...




    //Interface Utils
    public void FireWeapon()
    {
        if (_isSubsystemOnline && _isWeaponReady)
        {
            FireProjectile();
            EnterCooldown();
        }
    }

    public float GetCooldown()
    {
        return _cooldown;
    }

    public int GetDamage()
    {
        return _damage;
    }

    public string GetWeaponName()
    {
        return _weaponName;
    }

    public string GetWeaponType()
    {
        return _weaponType;
    }

    public void SetCooldown(float value)
    {
        if (value >= 0)
            _cooldown = value;
    }

    public void SetDamage(int value)
    {
        if (value >= 0)
            _damage = value;
    }

    public void SetParentSubsystemAndInitialize(IWeaponsSubsystemBehavior weaponSubsystem)
    {
        _parentSubsystem = weaponSubsystem;
        _isSubsystemOnline = !_parentSubsystem.IsWeaponsDisabled();
    }

    public void SetSubsystemOnlineStatus(bool newValue)
    {
        _isSubsystemOnline = newValue;
    }




    //Utils
    protected abstract void FireProjectile();

    protected virtual void EnterCooldown()
    {
        _isWeaponReady = false;
        Invoke("ReadyWeapon", _cooldown);
    }

    protected void ReadyWeapon()
    {
        _isWeaponReady = true;
    }

}

public class WeaponsBehavior : ShipSubsystem, IWeaponsSubsystemBehavior
{
    //Declarations
    [SerializeField] private IShipWeaponry[] _weaponsArray;



    //Monobehaviours
    //...



    //Interface Utils
    public void AddWeapon(IShipWeaponry newWeapon, int slot)
    {
        if (slot > 0 && slot < _weaponsArray.Length)
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
        if (slot > 0 && slot < _weaponsArray.Length)
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

    public void SetParentShipAndInitializeAwakeReferences(Ship parent)
    {
        _parentShip = parent;
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




    //Debugging




}
