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

public abstract class AbstractShipWeapon : MonoBehaviour, IShipWeaponry
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
