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
    [SerializeField] protected bool _isWarmedUp = false;
    [SerializeField] protected bool _isWarmingUp = false;
    [SerializeField] protected bool _isCooledDown = true;
    [SerializeField] protected bool _isSubsystemOnline = true;
    [SerializeField] protected bool _shootCommand = false;

    public delegate void ShipWeaponEvent();
    public event ShipWeaponEvent OnWarmupEntered;
    public event ShipWeaponEvent OnWarmupCanceled;
    public event ShipWeaponEvent OnWarmupExited;

    public event ShipWeaponEvent OnCooldownEntered;
    public event ShipWeaponEvent OnCooldownExited;


    [Header("Debug Utils")]
    [SerializeField] protected bool _isDebugActive = false;



    //Monobehaviours
    private void Update()
    {
        if (_isSubsystemOnline)
            FireWeaponOnCommand();
    }




    //Interal Utils
    protected virtual void EnterWarmupIfApplicable()
    {
        if (_isCooledDown && _isWarmedUp == false && _isWarmingUp == false)
        {
            _isWarmingUp = true;
            OnWarmupEntered?.Invoke();
            if (_warmupTime > 0)
            {
                LogStatement($"{_weaponName} on {_parentShip.GetName()} began warming up");
                Invoke("CompleteWarmup", _warmupTime);
            }
            else
                CompleteWarmup();
            
        }
    }

    protected virtual void CancelWarmup()
    {
        if (_isWarmingUp)
        {
            _isWarmingUp = false;
            LogStatement($"{_weaponName} on {_parentShip.GetName()} canceled warmup");
            OnWarmupCanceled?.Invoke();
            CancelInvoke("CompleteWarmup");
            
        }
            
    }

    protected virtual void CompleteWarmup()
    {
        _isWarmingUp = false;
        _isWarmedUp = true;

        if (_warmupTime > 0)
            LogStatement($"{_weaponName} on {_parentShip.GetName()} completed warmup");
        else
            LogStatement($"{_weaponName} on {_parentShip.GetName()} warmed up instantaneously");

        OnWarmupExited?.Invoke();
    }

    protected virtual void EnterCooldown()
    {
        _isCooledDown = false;
        LogStatement($"{_weaponName} on {_parentShip.GetName()} began cooling down");
        OnCooldownEntered?.Invoke();
        Invoke("EndCooldown", _cooldownTime);
        
    }

    protected virtual void EndCooldown()
    {
        _isCooledDown = true;
        LogStatement($"{_weaponName} on {_parentShip.GetName()} completed cooldown");
        OnCooldownExited?.Invoke();
    }

    protected virtual bool IsWeaponReady()
    {
        return _isCooledDown && _isWarmedUp;
    }

    protected abstract void PerformWeaponFire();

    protected abstract void InterruptShotByOtherMeansDueToReleasedInput();




    //Getters, Setters, & Commands
    public void SetShootCommand(bool value)
    {
        _shootCommand = value;
    }

    public void FireWeaponOnCommand()
    {
        if (_shootCommand)
        {
            EnterWarmupIfApplicable();

            if (IsWeaponReady())
            {
                _isWarmedUp = false;
                PerformWeaponFire();
            }
        }

        else
        {
            if (_isWarmingUp)
            {
                LogStatement($"{_weaponName} on {_parentShip.GetName()} aborted warmup due to released Input");
                CancelWarmup();
            }

            else if (IsWeaponReady())
            {
                LogStatement($"{_weaponName} on {_parentShip.GetName()} aborted firing due to released Input");
                _isWarmedUp = false;
            }

            else InterruptShotByOtherMeansDueToReleasedInput();
        }
        
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

        if (_isWarmingUp && _isSubsystemOnline == false)
            CancelWarmup();
            
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









    //Utils
    

}

public abstract class AbstractBlasterWeapon : AbstractShipWeapon
{
    //Declarations
    public event ShipWeaponEvent OnProjectileFired;


    //Monobehaviours
    //...



    //Internal Utils
    protected override void PerformWeaponFire()
    {
        FireProjectile();
        OnProjectileFired?.Invoke();
        EnterCooldown();
    }

    protected abstract void FireProjectile();

    protected override void InterruptShotByOtherMeansDueToReleasedInput()
    {
        //nothing. Blasters aren't continuous weapons
    }

    //Getters Setters, & Commands
    //...

}


public abstract class AbstractLaserWeapon : AbstractShipWeapon
{
    //Declarations
    [SerializeField] protected bool _isShotStarted = false;


    public event ShipWeaponEvent OnLaserFireEntered;
    public event ShipWeaponEvent OnLaserFireExited;


    //Monobehaviours
    //...


    //Internal Utils
    protected override void PerformWeaponFire()
    {
        if (_isShotStarted == false)
        {
            _isShotStarted = true;
            OnLaserFireEntered?.Invoke();
        }

        FireLaser();
    }

    protected override void InterruptShotByOtherMeansDueToReleasedInput()
    {
        _isShotStarted = false;
        OnLaserFireExited?.Invoke();
        EnterCooldown();
    }

    protected abstract void FireLaser();



    //Getters Setters, & Commands
    //...


}

