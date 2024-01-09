using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public interface IShipWeaponry
{
    void SetShootCommand(bool input);

    void FireWeaponOnCommand();

    void SetParentSubsystemAndInitialize(IWeaponsSubsystemBehavior weaponSubsystem, AbstractShip parentShip);

    string GetWeaponName();

    int GetDamage();

    float GetCooldown();

    float GetWarmup();

    string GetWeaponDesc();

    void SetDamage(int value);

    void SetCooldown(float value);

    void SetWarmup(float value);

    void SetSubsystemOnlineStatus(bool newStatus);

}

public abstract class AbstractShipWeapon : MonoBehaviour, IShipWeaponry
{
    //Declarations
    [Header("Weapon MetaData")]
    [SerializeField] protected AbstractShip _parentShip;
    [SerializeField] protected IWeaponsSubsystemBehavior _parentWeaponSubsystemInterface;
    [SerializeField] protected string _weaponName = "Unnamed Weapon";
    [SerializeField] protected string _weaponDescription = "Undefined Description";
    [SerializeField] protected int _damage;
    [SerializeField] protected float _cooldownTime = .5f;
    [SerializeField] protected float _warmupTime = 0;
    [SerializeField] protected bool _shootCommand = false;
    [SerializeField] protected bool _isSubsystemOnline = true;
    [SerializeField] protected bool _isWarmingUp = false;
    [SerializeField] protected bool _isFiring = false;
    [SerializeField] protected bool _isCooledDown = true;


    public delegate void ShipWeaponEvent();
    public event ShipWeaponEvent OnWarmupEntered;
    public event ShipWeaponEvent OnWarmupCanceled;
    public event ShipWeaponEvent OnWarmupCompleted;

    public event ShipWeaponEvent OnCooldownEntered;
    public event ShipWeaponEvent OnCooldownCompleted;


    [Header("Debug Utils")]
    [SerializeField] protected bool _isDebugActive = false;



    //Monobehaviours
    private void Update()
    {
        if (_isSubsystemOnline)
            FireWeaponOnCommand();
    }




    //Interal Utils
    protected virtual void WarmWeapon()
    {  
        _isWarmingUp = true;
        OnWarmupEntered?.Invoke();
        if (_warmupTime > 0)
        {
            LogStatement($"{_weaponName} on {_parentShip.GetName()} began warming up");
            Invoke("StartFiringWeapon", _warmupTime);
        }
        else
            StartFiringWeapon();
    }

    protected virtual void CancelWarmup()
    {
        if (_isWarmingUp)
        {
            _isWarmingUp = false;
            LogStatement($"{_weaponName} on {_parentShip.GetName()} canceled warmup");
            OnWarmupCanceled?.Invoke();
            CancelInvoke("StartFiringWeapon");
            
        }
            
    }

    protected virtual void StartFiringWeapon()
    {
        _isWarmingUp = false;
        _isFiring = true;

        if (_warmupTime > 0)
            LogStatement($"{_weaponName} on {_parentShip.GetName()} completed warmup");
        else
            LogStatement($"{_weaponName} on {_parentShip.GetName()} warmed up instantaneously");

        OnWarmupCompleted?.Invoke();
    }

    protected virtual void EndWeaponsActivity()
    {
        if (_isWarmingUp)
        {
            LogStatement($"{_weaponName} on {_parentShip.GetName()} aborted warmup");
            CancelWarmup();
        }

        else if (_isFiring)
        {
            LogStatement($"{_weaponName} on {_parentShip.GetName()} aborted firing");
            _isFiring = false;
            EnterCooldown();
        }
    }

    protected virtual void EnterCooldown()
    {
        _isFiring = false;
        _isCooledDown = false;
        LogStatement($"{_weaponName} on {_parentShip.GetName()} began cooling down");
        OnCooldownEntered?.Invoke();
        Invoke("EndCooldown", _cooldownTime);
        
    }

    protected virtual void EndCooldown()
    {
        _isCooledDown = true;
        LogStatement($"{_weaponName} on {_parentShip.GetName()} completed cooldown");
        OnCooldownCompleted?.Invoke();
    }

    protected abstract void PerformWeaponFireLogic();




    //Getters, Setters, & Commands
    public void SetShootCommand(bool value)
    {
        _shootCommand = value;
    }

    public void FireWeaponOnCommand()
    {
        if (_shootCommand && _isCooledDown)
        {
            if (_isFiring == false && _isWarmingUp == false)
                WarmWeapon();

            else if (_isFiring == true)
                PerformWeaponFireLogic();
        }

        else if (_shootCommand == false)
            EndWeaponsActivity();
    }

    public float GetCooldown()
    {
        return _cooldownTime;
    }

    public float GetWarmup()
    {
        return _warmupTime;
    }

    public int GetDamage()
    {
        return _damage;
    }

    public string GetWeaponName()
    {
        return _weaponName;
    }

    public string GetWeaponDesc()
    {
        return _weaponDescription;
    }

    public void SetCooldown(float value)
    {
        if (value >= 0)
            _cooldownTime = value;
    }

    public void SetWarmup(float newValue)
    {
        _warmupTime = Mathf.Max(0, newValue);
    }

    public void SetDamage(int value)
    {
        if (value >= 0)
            _damage = value;
    }

    public void SetParentSubsystemAndInitialize(IWeaponsSubsystemBehavior weaponSubsystem, AbstractShip parentShip)
    {
        _parentWeaponSubsystemInterface = weaponSubsystem;
        _isSubsystemOnline = !_parentWeaponSubsystemInterface.IsWeaponsDisabled();

        _parentShip = parentShip;
    }

    public void SetSubsystemOnlineStatus(bool newValue)
    {
        _isSubsystemOnline = newValue;

        if (_isSubsystemOnline == false)
            EndWeaponsActivity();
            
    }




    //Debug Utils
    protected void LogStatement(string statement)
    {
        STKDebugLogger.LogStatement(_isDebugActive, statement);
    }

    protected void LogWarning(string warning)
    {
        STKDebugLogger.LogWarning(warning);
    }

    protected void LogError(string error)
    {
        STKDebugLogger.LogError(error);
    }



}





