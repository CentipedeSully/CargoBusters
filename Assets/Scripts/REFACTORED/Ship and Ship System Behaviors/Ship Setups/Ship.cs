using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHullBehavior
{
    void SetParent(Ship parent);

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

public interface IEngineBehavior
{
    void SetParent(Ship parent);
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

public interface IShieldBehavior
{
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

public interface IWeaponsBehavior
{
    bool IsWeaponsDisabled();

    void DisableWeapons();

    void EnableWeapons();

    void FireWeapons();

    void SetDamage(int newValue);

    int GetDamage();

    void AddBlaster();

    void RemoveBlaster();

    int GetBlasterCount();

    void SetCooldown(float newValue);

    float GetCooldown();



    //Debugging
    void EnterDebug();

    void ExitDebug();

    bool IsDebugActive();

    void LogAllData();
}

public interface ICargoBehavior
{
    bool IsCargoSecurityDisabled();

    void DisableCargoSecurity();

    void EnableCargoSecurity();

    
    
    //Debugging
    void EnterDebug();

    void ExitDebug();

    bool IsDebugActive();

    void LogAllData();
}

public interface IWarpBehavior
{
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

public interface IBusterBehavior
{
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

public interface IDeathBehavior
{
    void SetParent(Ship parent);

    void SetInstanceTracker(IInstanceTracker instanceTracker);

    void TriggerDeathSequence();

    float GetDeathSequenceDuration();

    bool IsDying();



    //Debugging
    void ToggleDebugMode();

    bool IsDebugActive();
}

public interface IShipController
{
    void SetParent(Ship parent);

    void DetermineDecisions();

    void CommunicateDecisionsToSubsystems();


    //Debugging
    void ToggleDebug();

    bool IsDebugActive();
}

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

public class ShipSubsystem : MonoBehaviour, IDisableable
{
    //Declarations
    [Header("Subsystem Overview")]
    [SerializeField] protected string _name = "Unnamed Subsystem";
    [SerializeField] protected Ship _parentShip;
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
    public bool IsDisabled()
    {
        return IsSubsystemDisabled();
    }

    public void DisableEntity()
    {
        DisableSubsystem();
    }

    public void EnableEntity()
    {
        EnableSubsystem();
    }



    //Utils
    public string GetName()
    {
        return _name;
    }

    public void SetName(string newName)
    {
        _name = newName;
    }

    public Ship GetParentShip()
    {
        return _parentShip;
    }

    public bool IsSubsystemDisabled()
    {
        return _isDisabled;
    }

    public void DisableSubsystem()
    {
        _isDisabled = true;
        OnSubsystemDisabled?.Invoke();
    }

    public void EnableSubsystem()
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

public abstract class Ship : MonoBehaviour, IDisableable, IDamageable
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
    [Header("Behavior References")]
    [SerializeField] protected IShipController _shipController;
    [SerializeField] protected IHullBehavior _hullBehavior;
    [SerializeField] protected IEngineBehavior _engineBehavior;
    [SerializeField] protected IDeathBehavior _deathBehavior;




    //Monobehaviours
    protected virtual void Awake()
    {
        InitializeBehaviors();
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
    protected virtual void InitializeBehaviors()
    {
        _shipController = GetComponent<IShipController>();
        _hullBehavior = GetComponent<IHullBehavior>();
        _engineBehavior = GetComponent<IEngineBehavior>();
        _deathBehavior = GetComponent<IDeathBehavior>();

        _shipController.SetParent(this);
        _hullBehavior.SetParent(this);
        _engineBehavior.SetParent(this);
        _deathBehavior.SetParent(this);
        _deathBehavior.SetInstanceTracker(GameManager.Instance.GetInstanceTracker());
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
            _isShipDisabled = true;
            _engineBehavior.DisableEngines();
        }
    }

    public virtual void EnableShip()
    {
        if (_isShipDisabled == true)
        {
            _isShipDisabled = false;
            _engineBehavior.EnableEngines();
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

    
    public IHullBehavior GetHullBehavior()
    {
        return _hullBehavior;
    }

    public IEngineBehavior GetEngineBehavior()
    {
        return _engineBehavior;
    }

    public IDeathBehavior GetDeathBehavior()
    {
        return _deathBehavior;
    }
    
}
