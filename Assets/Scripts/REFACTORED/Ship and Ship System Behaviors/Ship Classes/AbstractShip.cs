using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//NonSubsystem Interfaces
public interface IShipController
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    void DetermineDecisions();

    void CommunicateDecisionsToSubsystems();




    //Debugging
    void ToggleDebug();

    bool IsDebugActive();
}

public interface IHullBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    void DisableShipFromCriticalDamage();

    void EnableShip();

    void DamageHull(int damage);

    void DamageHullToNearDeath();

    int GetCurrentValue();

    int GetMaxValue();

    float GetCriticalThresholdPercent();

    float GetIntegrityPercent();

    void SetMaxValue(int newValue);

    void SetCurrentValue(int newValue);

    void SetCurrentValueToCritical();

    void SetCriticalThresholdPercent(float newThreshold);


    //Debugging
    void ToggleDebugMode();

    bool IsDebugActive();
}

public interface IDeathBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    void TriggerDeathSequence();

    float GetDeathSequenceDuration();

    bool IsDying();



    //Debugging
    void ToggleDebugMode();

    bool IsDebugActive();
}



//Subsystem Interfaces and Class Definitions
public interface IEngineSubsystemBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    bool IsEngineDisabled();

    void DisableEngines();

    void EnableEngines();

    void MoveIfEnginesEnabled();

    void SetTurnInput(float newValue);

    void SetThrustInput(float newValue);

    void SetStrafeInput(float newValue);

    float GetTurnSpeed();

    float GetThrustForce();

    float GetStrafeForce();

    void SetThustForce(float newValue);

    void SetStrafeForce(float newValue);

    void SetTurnSpeed(float newValue);


    //Debugging
    void ToggleDebugMode();

    bool IsDebugActive();

}

public interface IShieldSubsystemBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    bool IsShieldsDisabled();

    void DisableShields();

    void EnableShields();

    void InterruptShieldingProcess();

    void DamageShields(int damage);

    int GetCurrentValue();

    int GetMaxValue();

    void SetMaxValue(int newValue);

    void SetCurrentValue(int newValue);


    //Debugging
    void EnterDebug();

    void ExitDebug();

    bool IsDebugActive();

    void LogAllData();
}

public interface IWeaponsSubsystemBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    bool IsWeaponsDisabled();

    void DisableWeapons();

    void EnableWeapons();

    void FireWeapons();

    int GetWeaponCount();

    void AddWeapon(string weaponName, int slot);

    IShipWeaponry RemoveWeapon(int slot);

    IShipWeaponry GetWeaponFromPosition(int slot);

    int GetAvailablePosition();

    int GetPositionCount();



    //Debugging
    void ToggleDebugMode();

    bool IsDebugActive();
}

public interface ICargoSubsystemBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    bool IsCargoSecurityDisabled();

    void DisableCargoSecurity();

    void EnableCargoSecurity();

    
    
    //Debugging
    void EnterDebug();

    void ExitDebug();

    bool IsDebugActive();

    void LogAllData();
}

public interface IWarpSubsystemBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    bool IsWarpDisabled();

    void DisableWarping();

    void EnableWarping();

    void InterruptWarp();

    void BeginWarp();



    //Debugging
    void EnterDebug();

    void ExitDebug();

    bool IsDebugActive();

    void LogAllData();
}

public interface IBusterSubsystemBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    bool IsBusterDisabled();

    void DisableBuster();

    void EnableBuster();

    void BustCargo();

    void InterruptBuster();



    //Debugging
    void EnterDebug();

    void ExitDebug();

    bool IsDebugActive();

    void LogAllData();
}

public class ShipSubsystem : MonoBehaviour
{
    //Declarations
    [Header("Subsystem Overview")]
    [SerializeField] protected string _name = "Unnamed Subsystem";
    [SerializeField] protected AbstractShip _parentShip;
    [SerializeField] protected bool _isDisabled = false;
    [SerializeField] protected bool _showDebug = false;

    public delegate void SubsystemEvent();
    public event SubsystemEvent OnSubsystemDisabled;
    public event SubsystemEvent OnSubsystemEnabled;


    //Constructors
    //...


    //MonoBehaviours
    //...


    //Interface Utils
    //...



    //Utils
    public string GetName()
    {
        return _name;
    }

    public void SetName(string newName)
    {
        _name = newName;
    }

    public AbstractShip GetParentShip()
    {
        return _parentShip;
    }

    public bool IsSubsystemDisabled()
    {
        return _isDisabled;
    }

    public virtual void DisableSubsystem()
    {
        _isDisabled = true;
        OnSubsystemDisabled?.Invoke();
    }

    public virtual void EnableSubsystem()
    {
        _isDisabled = false;
        OnSubsystemEnabled?.Invoke();
    }

    public bool IsDebugActive()
    {
        return _showDebug;
    }

    public void ToggleDebugMode()
    {
        if (_showDebug)
            _showDebug = false;
        else _showDebug = true;
    }


}



//Other Interfaces
public interface IDisableable
{
    bool IsDisabled();

    void DisableEntity();

    void EnableEntity();
}

public interface IDamageable
{
    void TakeDamage(int value, bool preserveShip);
}



//Ship Definition
public abstract class AbstractShip : MonoBehaviour, IDisableable, IDamageable
{
    //Declarations
    [Header("Ship Info")]
    [SerializeField] protected string _name = "Unnamed";
    [SerializeField] protected string _faction = "Independent";
    [SerializeField] protected bool _isPlayer = false;
    [SerializeField] protected bool _isShipDisabled = false;
    [SerializeField] protected bool _debugMode = false;
    protected bool _debugFlag = false;


    //behavior references
    protected IShipController _shipController;
    protected IHullBehavior _hullBehavior;
    protected IDeathBehavior _deathBehavior;

    protected IEngineSubsystemBehavior _engineBehavior;
    protected IShieldSubsystemBehavior _shieldsBehavior;
    protected IWeaponsSubsystemBehavior _weaponsBehavior;
    protected ICargoSubsystemBehavior _cargoBehavior;
    protected IWarpSubsystemBehavior _warpBehavior;
    protected IBusterSubsystemBehavior _busterBehavior;
    




    //Monobehaviours
    protected virtual void Awake()
    {
        InitializeAwakeBehaviorReferences();
    }

    protected virtual void Start()
    {
        InitializeGameMaangerSourcedReferences();
    }

    protected virtual void Update()
    {
        WatchDebugMode();
        ControlShip();
    }



    //Interface Utils
    public bool IsDisabled()
    {
        return _isShipDisabled;
    }

    public void DisableEntity()
    {
        DisableShip();
    }

    public void EnableEntity()
    {
        EnableShip();
    }

    public virtual void TakeDamage(int damage, bool preserveShip)
    {
        SufferDamage(damage, preserveShip);
    }




    //Utils
    protected virtual void InitializeAwakeBehaviorReferences()
    {
        //Initialize local personal references
        _shipController = GetComponent<IShipController>();
        _hullBehavior = GetComponent<IHullBehavior>();
        _deathBehavior = GetComponent<IDeathBehavior>();

        _engineBehavior = GetComponent<IEngineSubsystemBehavior>();
        _shieldsBehavior = GetComponent<IShieldSubsystemBehavior>();
        _weaponsBehavior = GetComponent<IWeaponsSubsystemBehavior>();
        _cargoBehavior = GetComponent<ICargoSubsystemBehavior>();
        _warpBehavior = GetComponent<IWarpSubsystemBehavior>();
        _busterBehavior = GetComponent<IBusterSubsystemBehavior>();
        


        //Initialize component nonSubsystem references
        _shipController.SetParentShipAndInitializeAwakeReferences(this);
        _hullBehavior.SetParentShipAndInitializeAwakeReferences(this);
        _deathBehavior.SetParentShipAndInitializeAwakeReferences(this);



        //Initialize component Subsystems
        _engineBehavior.SetParentShipAndInitializeAwakeReferences(this);
        //_shieldsBehavior.SetParentShipAndInitializeAwakeReferences(this);
        _weaponsBehavior.SetParentShipAndInitializeAwakeReferences(this);
        //_cargoBehavior.SetParentShipAndInitializeAwakeReferences(this);
        //_warpBehavior.SetParentShipAndInitializeAwakeReferences(this);
        //_busterBehavior.SetParentShipAndInitializeAwakeReferences(this);

    }

    protected virtual void InitializeGameMaangerSourcedReferences()
    {
        //Initialize component nonSubsystem references
        _shipController.InitializeGameManagerDependentReferences();
        _hullBehavior.InitializeGameManagerDependentReferences();
        _deathBehavior.InitializeGameManagerDependentReferences();



        //Initialize component Subsystems
        _engineBehavior.InitializeGameManagerDependentReferences();
        //_shieldsBehavior.InitializeGameManagerDependentReferences();
        _weaponsBehavior.InitializeGameManagerDependentReferences();
        //_cargoBehavior.InitializeGameManagerDependentReferences();
        //_warpBehavior.InitializeGameManagerDependentReferences();
        //_busterBehavior.InitializeGameManagerDependentReferences();
    }

    protected virtual void ControlShip()
    {
        _shipController.DetermineDecisions();
        _shipController.CommunicateDecisionsToSubsystems();
    }




    public virtual void DisableShip()
    {
        if (_isShipDisabled == false)
        {
            //disable all subsystems
            _isShipDisabled = true;
            _engineBehavior.DisableEngines();
            //_shieldsBehavior.DisableShields();
            _weaponsBehavior.DisableWeapons();
            //_cargoBehavior.DisableCargoSecurity();
            //_warpBehavior.DisableWarping();
            //_busterBehavior.DisableBuster();
        }
    }

    public virtual void EnableShip()
    {
        if (_isShipDisabled == true)
        {
            _isShipDisabled = false;
            _engineBehavior.EnableEngines();
            //_shieldsBehavior.EnableShields();
            _weaponsBehavior.EnableWeapons();
            //_cargoBehavior.EnableCargoSecurity();
            //_warpBehavior.EnableWarping();
            //_busterBehavior.EnableBuster();
        }
    }

    public virtual void Die()
    {
        _deathBehavior.TriggerDeathSequence();
    }

    public virtual void SufferDamage(int value, bool preserveShip)
    {
        
        if (preserveShip == true)
        {
            if (_hullBehavior.GetCurrentValue() < value)
            {
                _hullBehavior.DamageHullToNearDeath();
                _hullBehavior.DisableShipFromCriticalDamage();
            }

            else _hullBehavior.DamageHull(value);
        }

        else _hullBehavior.DamageHull(value);
        
    }




    protected virtual void WatchDebugMode()
    {
        if (_debugMode != _debugFlag)
        {
            if (_debugMode)
                EnterDebugMode();
            else ExitDebugMode();

            _debugFlag = _debugMode;
        }
    }

    public virtual void EnterDebugMode()
    {
        _debugMode = true;
        Debug.Log("Entered DebugMode");
        if (_hullBehavior.IsDebugActive() == false)
            _hullBehavior.ToggleDebugMode();
        if (_engineBehavior.IsDebugActive() == false)
            _engineBehavior.ToggleDebugMode();
        if (_deathBehavior.IsDebugActive() == false)
            _deathBehavior.ToggleDebugMode();
        if (_weaponsBehavior.IsDebugActive() == false)
            _weaponsBehavior.ToggleDebugMode();


    }

    public virtual void ExitDebugMode()
    {
        _debugMode = false;
        Debug.Log("Exited DebugMode");
        if (_hullBehavior.IsDebugActive())
            _hullBehavior.ToggleDebugMode();
        if (_engineBehavior.IsDebugActive())
            _engineBehavior.ToggleDebugMode();
        if (_deathBehavior.IsDebugActive())
            _deathBehavior.ToggleDebugMode();
        if (_weaponsBehavior.IsDebugActive())
            _weaponsBehavior.ToggleDebugMode();
    }




    //External Control Utils
    public string GetName()
    {
        return _name;
    }

    public void SetName(string newName)
    {
        _name = newName;
    }

    public string GetFaction()
    {
        return _faction;
    }

    public void SetFaction(string newFaction)
    {
        _faction = newFaction;
    }

    public bool IsShipDisabled()
    {
        return _isShipDisabled;
    }

    public bool IsPlayer()
    {
        return _isPlayer;
    }

    

    public IShipController GetShipControllerBehavior()
    {
        return _shipController;
    }

    public IHullBehavior GetHullBehavior()
    {
        return _hullBehavior;
    }

    public IDeathBehavior GetDeathBehavior()
    {
        return _deathBehavior;
    }



    public IEngineSubsystemBehavior GetEnginesBehavior()
    {
        return _engineBehavior;
    }

    public IShieldSubsystemBehavior GetShieldsBehavior()
    {
        return _shieldsBehavior;
    }

    public IWeaponsSubsystemBehavior GetWeaponsBehavior()
    {
        return _weaponsBehavior;
    }

    public ICargoSubsystemBehavior GetCargoBehavior()
    {
        return _cargoBehavior;
    }

    public IWarpSubsystemBehavior GetWarpBehavior()
    {
        return _warpBehavior;
    }

    public IBusterSubsystemBehavior GetBusterBehavior()
    {
        return _busterBehavior;
    }

}
