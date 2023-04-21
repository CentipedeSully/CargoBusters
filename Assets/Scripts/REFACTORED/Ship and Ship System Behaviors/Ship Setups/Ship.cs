using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHullBehavior
{
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
    void EnterDebug();

    void ExitDebug();

    bool IsDebugActive();
}

public interface IEngineBehavior
{
    bool IsEngineDisabled();

    void DisableEngines();

    void EnableEngines();

    int GetTurnSpeed();

    int GetForwardsSpeed();

    void Turn(int lateralDirection);

    void MoveForwards(int magnitude);

    void SetTurnSpeed(int newValue);

    void SetForwardsSpeed(int newValue);


    //Debugging
    void EnterDebug();

    void ExitDebug();

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
}

public interface IDeathBehavior
{
    void ReportDeath();

    void TriggerDeathSequence();

    float GetDeathSequenceDuration();



    //Debugging
    void EnterDebug();

    void ExitDebug();

    bool IsDebugActive();
}

public interface IShipController
{
    void ReadInput();
    void CommunicateMoveInput();
    void CommunicateShootInput();
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

public abstract class Ship : MonoBehaviour, IDisableable, IDamageable
{
    //Declarations
    [SerializeField] private string _name = "Unnamed";
    [SerializeField] private string _faction = "Independent";
    [SerializeField] private bool _isPlayer = false;
    [SerializeField] private bool _isShipDisabled = false;
    [SerializeField] private bool _debugMode = false;
    private bool _debugFlag = false;

    [Space(20)]
    [SerializeField] private int _forwardsInput;
    [SerializeField] private int _turnInput;
    [SerializeField] private bool _shootInput;


    //behavior references
    [SerializeField] private IHullBehavior _hullBehavior;
    [SerializeField] private IEngineBehavior _engineBehavior;
    [SerializeField] private IShieldBehavior _shieldBehavior;
    [SerializeField] private IWeaponsBehavior _weaponsBehavior;
    [SerializeField] private ICargoBehavior _cargoBehavior;
    [SerializeField] private IWarpBehavior _warpBehavior;
    [SerializeField] private IBusterBehavior _busterBehavior;
    [SerializeField] private IDeathBehavior _deathBehavior;
    [SerializeField] private IShipController _shipController;



    //Monobehaviours
    private void Update()
    {
        WatchDebugMode();
        _shipController.ReadInput();
        _shipController.CommunicateMoveInput();
        _shipController.CommunicateShootInput();
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

    public void TakeDamage(int damage, bool preserveShip)
    {
        SufferDamage(damage, preserveShip);
    }




    //Utils
    public void DisableShip()
    {
        if (_isShipDisabled == false)
        {
            _isShipDisabled = true;
            _engineBehavior.DisableEngines();
            _shieldBehavior.DisableShields();
            _weaponsBehavior.DisableWeapons();
            _cargoBehavior.DisableCargoSecurity();
            _warpBehavior.DisableWarping();
            _busterBehavior.DisableBuster();
        }
    }

    public void EnableShip()
    {
        if (_isShipDisabled == true)
        {
            _isShipDisabled = false;
            _engineBehavior.EnableEngines();
            _shieldBehavior.EnableShields();
            _weaponsBehavior.EnableWeapons();
            _cargoBehavior.EnableCargoSecurity();
            _warpBehavior.EnableWarping();
            _busterBehavior.EnableBuster();
        }
    }

    public void Die()
    {
        _deathBehavior.ReportDeath();
        _deathBehavior.TriggerDeathSequence();
    }

    public void SufferDamage(int value, bool preserveShip)
    {
        if (_shieldBehavior.GetCurrentValue() > 0)
            _shieldBehavior.DamageShields(value);

        else
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
    }


    private void WatchDebugMode()
    {
        if (_debugMode != _debugFlag)
        {
            if (_debugMode)
                EnterDebugMode();
            else ExitDebugMode();

            _debugFlag = _debugMode;
        }
    }

    public void EnterDebugMode()
    {
        _debugMode = true;
        _hullBehavior.EnterDebug();
        _engineBehavior.EnterDebug();
        _shieldBehavior.EnterDebug();
        _weaponsBehavior.EnterDebug();
        _cargoBehavior.EnterDebug();
        _warpBehavior.EnterDebug();
        _busterBehavior.EnterDebug();
        _deathBehavior.EnterDebug();
    }

    public void ExitDebugMode()
    {
        _debugMode = false;
        _hullBehavior.ExitDebug();
        _engineBehavior.ExitDebug();
        _shieldBehavior.ExitDebug();
        _weaponsBehavior.ExitDebug();
        _cargoBehavior.ExitDebug();
        _warpBehavior.ExitDebug();
        _busterBehavior.ExitDebug();
        _deathBehavior.ExitDebug();
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

    public bool _IsPlayer()
    {
        return _isPlayer;
    }

    public void SetForwardsInput(int newValue)
    {
        _forwardsInput = newValue;
    }

    public void SetTurnInput(int newValue)
    {
        _turnInput = newValue;
    }

    public void SetShootInput(bool newValue)
    {
        _shootInput = newValue;
    }

    
    public IHullBehavior GetHullBehavior()
    {
        return _hullBehavior;
    }

    public IEngineBehavior GetEngineBehavior()
    {
        return _engineBehavior;
    }

    public IShieldBehavior GetShieldBehavior()
    {
        return _shieldBehavior;
    }

    public IWeaponsBehavior GetWeaponBehavior()
    {
        return _weaponsBehavior;
    }

    public ICargoBehavior GetCargoBehavior()
    {
        return _cargoBehavior;
    }

    public IWarpBehavior GetWarpBehavior()
    {
        return _warpBehavior;
    }

    public IBusterBehavior GetBusterBehavior()
    {
        return _busterBehavior;
    }
    
}
