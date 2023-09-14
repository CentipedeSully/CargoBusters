using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

//NonSubsystem Interfaces
public interface IShipController
{
    void InitializeReferences(AbstractShip parentShip);

    void RemoveController();

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

    void RepairHull(int value);

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

public struct WeaponDetails
{
    public int _slot;
    public IShipWeaponry _weapon;
    public AbstractShip _parentShip;

    public WeaponDetails(IShipWeaponry newWeapon, int slotAssignment, AbstractShip parent)
    {
        _weapon = newWeapon;
        _slot = slotAssignment;
        _parentShip = parent;
    }
}

public interface IWeaponsSubsystemBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();



    bool IsWeaponsDisabled();

    void DisableWeapons();

    void EnableWeapons();

    void SetShootInput(bool value);

    bool GetShootInput();

    int GetWeaponCount();

    int GetSlotCount();

    void AddWeapon(string weaponName, int slot);

    void AddWeaponSlot(Vector2 SlotPosition);

    void RemoveWeapon(int slot);

    AbstractShipWeapon GetWeaponAtSlot(int slot);

    List<int> GetUnoccupiedSlots();

    List<int> GetOccupiedSlots();

    Dictionary<int,AbstractShipWeapon> GetAllWeapons();

    Dictionary<int,Transform> GetAllSlots();





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

public interface IPhaseSubsystemBehavior
{
    void SetParentShipAndInitializeAwakeReferences(AbstractShip parent);

    void InitializeGameManagerDependentReferences();

    bool IsPhasingDisabled();

    void EnablePhasing();

    void DisablePhasing();


    void PhaseShift(int actionCode);

    bool IsPhaseAvailable();

    int GetPhaseMax();

    void SetPhaseMax(int value);

    int GetAvailablePhases();

    void ReplenishPhases();

    void EmptyPhases();

    void IncreasePhases(int value);

    void DecreasePhases(int value);


    float GetRange();

    void SetRange(float value);

    void SetPhaseInput(bool input);


    //Debugging

    void ToggleDebugMode();


    bool IsDebugActive();

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
    int GetInstanceID();

    void TakeDamage(int value, bool negateDeath);

    string GetTag();
}

public interface IRepairable
{
    int GetInstanceID();
    void RepairDamage(int value);
    string GetTag();
}



//Ship Definition
public abstract class AbstractShip : MonoBehaviour, IDisableable, IDamageable, IRepairable, IScannable
{
    //Declarations
    [Header("Ship Info")]
    [SerializeField] protected string _name = "Unnamed";
    [SerializeField] protected string _faction = "Independent";
    [SerializeField] protected bool _isPlayer = false;
    [SerializeField] protected bool _isShipDisabled = false;
    [SerializeField] protected bool _debugMode = false;
    protected bool _debugFlag = false;
    private bool _isControlErrorThrown = false;
    [SerializeField] protected int _instanceID;

    //behavior references
    protected IShipController _shipController;
    protected IHullBehavior _hullBehavior;
    protected IDeathBehavior _deathBehavior;
    protected ScannerBehaviour _scanner;

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
        _instanceID = GetInstanceID();
    }

    protected virtual void Start()
    {
        InitializeGameManagerSourcedReferences();
    }

    protected virtual void Update()
    {
        WatchDebugMode();
        ControlShip();
    }




    //Internal Utils
    protected virtual void InitializeAwakeBehaviorReferences()
    {
        //Initialize local personal references
        _hullBehavior = GetComponent<IHullBehavior>();
        _deathBehavior = GetComponent<IDeathBehavior>();
        _scanner = GetComponent<ScannerBehaviour>();

        _engineBehavior = GetComponent<IEngineSubsystemBehavior>();
        _shieldsBehavior = GetComponent<IShieldSubsystemBehavior>();
        _weaponsBehavior = GetComponent<IWeaponsSubsystemBehavior>();
        _cargoBehavior = GetComponent<ICargoSubsystemBehavior>();
        _warpBehavior = GetComponent<IWarpSubsystemBehavior>();
        _busterBehavior = GetComponent<IBusterSubsystemBehavior>();
        


        //Initialize component nonSubsystem references
        _hullBehavior.SetParentShipAndInitializeAwakeReferences(this);
        _deathBehavior.SetParentShipAndInitializeAwakeReferences(this);
        _scanner?.InitializeScanner(GetInstanceID());



        //Initialize component Subsystems
        _engineBehavior.SetParentShipAndInitializeAwakeReferences(this);
        _shieldsBehavior.SetParentShipAndInitializeAwakeReferences(this);
        _weaponsBehavior.SetParentShipAndInitializeAwakeReferences(this);
        //_cargoBehavior.SetParentShipAndInitializeAwakeReferences(this);
        //_warpBehavior.SetParentShipAndInitializeAwakeReferences(this);
        //_busterBehavior.SetParentShipAndInitializeAwakeReferences(this);

    }

    protected virtual void InitializeGameManagerSourcedReferences()
    {
        //Initialize component nonSubsystem references
        _hullBehavior.InitializeGameManagerDependentReferences();
        _deathBehavior.InitializeGameManagerDependentReferences();
        _scanner.InitializeScanner(GetInstanceID());



        //Initialize component Subsystems
        _engineBehavior.InitializeGameManagerDependentReferences();
        _shieldsBehavior.InitializeGameManagerDependentReferences();
        _weaponsBehavior.InitializeGameManagerDependentReferences();
        //_cargoBehavior.InitializeGameManagerDependentReferences();
        //_warpBehavior.InitializeGameManagerDependentReferences();
        //_busterBehavior.InitializeGameManagerDependentReferences();
    }

    protected virtual void ControlShip()
    {
        if (_shipController != null)
        {
            _shipController.DetermineDecisions();
            _shipController.CommunicateDecisionsToSubsystems();
        }
        else if (_isControlErrorThrown == false)
        {
            _isControlErrorThrown = true;
            STKDebugLogger.LogWarning($"Ship {GetName()} has no ShipController. Expect zero player/ai activity");
        }
            

    }

    protected virtual void SufferDamage(int value, bool preserveShip)
    {
        //try damaging shields first
        if (_shieldsBehavior != null)
        {
            if (_shieldsBehavior.GetCurrentValue() > 0)
                _shieldsBehavior.DamageShields(value);
            else
            {
                if (preserveShip == true)
                    DamageShipButNegateDeath(value);

                else _hullBehavior.DamageHull(value);
            }
        }

        //Damage hull if no shield system exists
        else if (preserveShip == true)
            DamageShipButNegateDeath(value);

        else _hullBehavior.DamageHull(value);
        
    }

    protected virtual void DamageShipButNegateDeath(int value)
    {
            if (_hullBehavior.GetCurrentValue() < value)
            {
                _hullBehavior.DamageHullToNearDeath();
                _hullBehavior.DisableShipFromCriticalDamage();
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

    protected virtual void RemoveShipController()
    {
        _shipController.RemoveController();
        _shipController = null;
        ResetControlError();
    }

    protected virtual void ResetControlError()
    {
        _isControlErrorThrown = false;
    }

    protected virtual void MakeShipUnplayable()
    {
        if (_isPlayer)
        {
            _isPlayer = false;
            GameManager.Instance.GetPlayerManager().ClearPlayerShip();
        }
    }

    protected virtual void MakeShipPlayable()
    {
        if (_isPlayer == false)
        {
            _isPlayer = true;
            GameManager.Instance.GetPlayerManager().SetShipAsPlayer(this);
        }
    }

    protected virtual bool DoesPlayerShipAlreadyExist()
    {
        return GameManager.Instance.GetPlayerManager().DoesPlayerShipExist();
    }



    //Getters, Setters, & Commands
    public string GetName()
    {
        return _name;
    }

    public string GetTag()
    {
        return tag;
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

    public virtual void Die()
    {
        _deathBehavior.TriggerDeathSequence();
    }

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

    public virtual void RepairDamage(int value)
    {
        _hullBehavior.RepairHull(value);
    }

    public virtual void DisableShip()
    {
        if (_isShipDisabled == false)
        {
            //disable all subsystems
            _isShipDisabled = true;
            _engineBehavior.DisableEngines();
            _shieldsBehavior.DisableShields();
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
            _shieldsBehavior.EnableShields();
            _weaponsBehavior.EnableWeapons();
            //_cargoBehavior.EnableCargoSecurity();
            //_warpBehavior.EnableWarping();
            //_busterBehavior.EnableBuster();
        }
    }

    public virtual void MakeShipAiControlled()
    {
        if (_shipController != null)
            RemoveShipController();

        _shipController = gameObject.AddComponent<ShipControllerViaAi>();
        _shipController.InitializeReferences(this);

        if (_isPlayer)
            MakeShipUnplayable();

    }

    public virtual void MakeShipPlayerControlled()
    {
        if (DoesPlayerShipAlreadyExist() == false)
        {
            if (_shipController != null)
                RemoveShipController();

            //Add new playerControlled Ship Controller
            _shipController = gameObject.AddComponent<ShipControllerViaPlayerInput>();
            _shipController.InitializeReferences(this);

            MakeShipPlayable();
        }

        else
        {
            if (_isPlayer)
                STKDebugLogger.LogWarning($"Attempted to make ship {_name} playable when it's already the player ship");
            else
                STKDebugLogger.LogWarning($"Failed to make ship {_name} playable. Ship {GameManager.Instance.GetPlayerManager().GetPlayerShip()._name}" +
                                          $" is already playable");
        }
            
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

    public ScannerBehaviour GetScannerBehavior()
    {
        return _scanner;
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

    public IPhaseSubsystemBehavior GetPhaseBehavior()
    {
        return null;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public string GetNameAsScannedObject()
    {
        return GetName();
    }


    //Debugging
    public virtual void EnterDebugMode()
    {
        _debugMode = true;
        Debug.Log("Entered DebugMode");
        //if (_hullBehavior.IsDebugActive() == false)
        //    _hullBehavior.ToggleDebugMode();
        //if (_engineBehavior.IsDebugActive() == false)
        //    _engineBehavior.ToggleDebugMode();
        //if (_deathBehavior.IsDebugActive() == false)
        //    _deathBehavior.ToggleDebugMode();
        //if (_weaponsBehavior.IsDebugActive() == false)
        //    _weaponsBehavior.ToggleDebugMode();

    }

    public virtual void ExitDebugMode()
    {
        _debugMode = false;
        Debug.Log("Exited DebugMode");
        //if (_hullBehavior.IsDebugActive())
        //    _hullBehavior.ToggleDebugMode();
        //if (_engineBehavior.IsDebugActive())
        //    _engineBehavior.ToggleDebugMode();
        //if (_deathBehavior.IsDebugActive())
        //    _deathBehavior.ToggleDebugMode();
        //if (_weaponsBehavior.IsDebugActive())
        //    _weaponsBehavior.ToggleDebugMode();
    }

 
}
