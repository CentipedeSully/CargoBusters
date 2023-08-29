using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;


public class WeaponsController : ShipSubsystem, IWeaponsSubsystemBehavior
{
    //Declarations
    [Header("Weapons")]
    [SerializeField] private bool _shootInput = false;
    [SerializeField] private Transform _weaponPositionParent;
    [SerializeField] private List<Transform> _baseWeaponPositions;
    [SerializeField] private string[] _slotVisualizer;
    private Dictionary<int,Transform> _weaponSlots;
    private Dictionary<int, AbstractShipWeapon> _equiptWeapons;
    private WeaponFactory _factoryReference;


    [Header("Debugging")]
    [SerializeField] private int _debugSlotIndex;
    [SerializeField] private string _debugWeaponName;
    [SerializeField] private Vector3 _debugNewSlotPosition;
    [SerializeField] private bool _addFullArsenalCmd = false;
    [SerializeField] private bool _addWeaponCmd = false;
    [SerializeField] private bool _addSlotCmd = false;
    [SerializeField] private bool _removeWeaponCmd = false;
    [SerializeField] private bool _logWeaponAtSlotCmd = false;
    [SerializeField] private bool _disableWeaponsCmd = false;
    [SerializeField] private bool _enableWeaponsCmd = false;
    [SerializeField] private bool _logWeaponCountCmd = false;
    [SerializeField] private bool _logSlotCountCmd = false;


    //events
    public delegate void WeaponryUpdateEvent(WeaponDetails aboutNewWeapon);
    public event WeaponryUpdateEvent OnWeaponAdded;
    public event WeaponryUpdateEvent OnWeaponRemoved;

    public delegate void WeaponsControllerEvent();
    public event WeaponsControllerEvent OnWeaponsDisabled;
    public event WeaponsControllerEvent OnWeaponsEnabled;
    public event WeaponsControllerEvent OnSlotAdded;



    //Monobehaviours
    private void Update()
    {
        if (_isDisabled == false)
            PassShootCommandToWeapons();

        if (_showDebug)
            ListenForDebugCommands();
    }




    //Internal Utils
    private void InitializeSlotDictionary()
    {
        _weaponSlots = new Dictionary<int, Transform>();
        for (int i = 0; i < _baseWeaponPositions.Count; i++)
            _weaponSlots.Add(i, _baseWeaponPositions[i]);
    }

    private void InitializeEquiptWeaponsDictionary()
    {
        _equiptWeapons = new Dictionary<int, AbstractShipWeapon>();
    }

    private void InitializeSlotVisualizer()
    {
        _slotVisualizer = new string[_baseWeaponPositions.Count];
        for (int i = 0; i < _slotVisualizer.Length; i++)
            _slotVisualizer[i] = "None";
    }

    private void RebuildSlotVisualizer()
    {
        _slotVisualizer = new string[_weaponSlots.Count];

        for (int i = 0; i < _slotVisualizer.Length; i++)
        {
            if (_equiptWeapons.ContainsKey(i))
                _slotVisualizer[i] = _equiptWeapons[i].GetWeaponName();
            else _slotVisualizer[i] = "None";
        }

    }

    private void ReinitializeWeapons()
    {
        //Each of the ship's weapon-prefabs are positioned under a separate transform(which determines where the gun will fire from)
        //Only one weapon should exist under each positional transform;

        foreach (KeyValuePair<int, Transform> slot in _weaponSlots)
        {
            if (slot.Value == null)
                STKDebugLogger.LogError($"Weapon slot {slot.Key} has no Transform set on ship {_parentShip.GetName()}.");
            else if (slot.Value.childCount > 0)
            {

                Transform weaponObjectTransform = slot.Value.GetChild(0);
                AbstractShipWeapon weaponReference = weaponObjectTransform.GetComponent<AbstractShipWeapon>();
                if (weaponReference != null)
                {
                    weaponReference.SetParentSubsystemAndInitialize(this, _parentShip);
                    _slotVisualizer[slot.Key] = weaponObjectTransform.GetComponent<AbstractShipWeapon>().GetWeaponName();
                    _equiptWeapons.Add(slot.Key, weaponReference);
                }

                else
                    STKDebugLogger.LogError($"No ShipWeapon component is detected on transform {weaponObjectTransform} on ship" +
                                            $" {_parentShip.GetName()}. Each weapon position may have ONLY ONE child object, which must possess" +
                                            $" a SINGLE ShipWeapon component. Ignoring this transform as a weapon position until weapons are " +
                                            $"reinitialized");
            }
        }
    }




    //Getters, Setters, & Commands
    public void AddWeaponSlot(Vector2 newLocalPosition)
    {
        Transform newPosition = Instantiate(new GameObject("Custom Position")).transform;
        newPosition.SetParent(_weaponPositionParent, false);
        newPosition.localPosition = newLocalPosition;

        int newSlotIndex = _weaponSlots.Count;
        _weaponSlots.Add(newSlotIndex, newPosition);

        if (newPosition.childCount > 0)
        {
            Transform childTransform = newPosition.GetChild(0);
            AbstractShipWeapon weaponReference = childTransform.GetComponent<AbstractShipWeapon>();

            if (weaponReference != null)
            {
                _equiptWeapons.Add(newSlotIndex, weaponReference);
                weaponReference.SetParentSubsystemAndInitialize(this, _parentShip);
            }
        }

        RebuildSlotVisualizer();
        OnSlotAdded?.Invoke();

    }

    public List<int> GetUnoccupiedSlots()
    {
        List<int> unoccupiedPositions = new List<int>();
        
        for (int i = 0; i < _weaponSlots.Count; i++)
        {
            if (!_equiptWeapons.ContainsKey(i))
                unoccupiedPositions.Add(i);
        }

        return unoccupiedPositions;
    }

    public List<int> GetOccupiedSlots()
    {
        List<int> OccupiedSlots = new List<int>();

        foreach (KeyValuePair<int, AbstractShipWeapon> weaponRecord in _equiptWeapons)
            OccupiedSlots.Add(weaponRecord.Key);

        return OccupiedSlots;
    }

    public void AddWeapon(string weaponName, int slot)
    {
        if (_factoryReference.DoesWeaponExist(weaponName) && _weaponSlots.ContainsKey(slot) && !_equiptWeapons.ContainsKey(slot))
        {
            GameObject newWeapon = _factoryReference.CreateWeaponObject(weaponName);
            newWeapon.transform.SetParent(_weaponSlots[slot],false);

            AbstractShipWeapon weaponReference = newWeapon.GetComponent<AbstractShipWeapon>();
            weaponReference.SetParentSubsystemAndInitialize(this, _parentShip);
            _equiptWeapons.Add(slot, weaponReference);

            RebuildSlotVisualizer();
            OnWeaponAdded?.Invoke(new WeaponDetails(weaponReference, slot, _parentShip));
        }
    }

    public void RemoveWeapon(int slot)
    {
        if (_weaponSlots.ContainsKey(slot) && _equiptWeapons.ContainsKey(slot))
        {
            AbstractShipWeapon weaponReference = _equiptWeapons[slot];
            GameObject weaponObject = weaponReference.gameObject;
            _equiptWeapons.Remove(slot);

            _factoryReference.DecomissionWeapon(weaponObject);

            RebuildSlotVisualizer();
            OnWeaponRemoved?.Invoke(new WeaponDetails(weaponReference, slot, _parentShip));
        }
    }

    public AbstractShipWeapon GetWeaponAtSlot(int slot)
    {
        if (_weaponSlots.ContainsKey(slot))
        {
            if (_weaponSlots[slot].childCount > 0)
                return _weaponSlots[slot].GetChild(0).GetComponent<AbstractShipWeapon>();

            else
            {
                STKDebugLogger.LogStatement(_showDebug, $"No weapon in slot {slot} on Ship {_parentShip.GetName()}");
                return null;
            }
        }

        else
        {
            STKDebugLogger.LogStatement(_showDebug, $"No slot {slot} exists on Ship {_parentShip.GetName()}");
            return null;
        }
    }

    public Dictionary<int,AbstractShipWeapon> GetAllWeapons()
    {

        Dictionary<int, AbstractShipWeapon> copyOfWeaponRecords = new Dictionary<int, AbstractShipWeapon>();

        foreach (KeyValuePair<int, AbstractShipWeapon> record in _equiptWeapons)
            copyOfWeaponRecords.Add(record.Key,record.Value);

        return copyOfWeaponRecords;
    }

    public Dictionary<int,Transform> GetAllSlots()
    {
        Dictionary<int, Transform> copyOfShipSlots = new Dictionary<int, Transform>();

        foreach (KeyValuePair<int, Transform> record in _weaponSlots)
            copyOfShipSlots.Add(record.Key, record.Value);

        return copyOfShipSlots;
    }

    public void PassShootCommandToWeapons()
    {
        foreach (KeyValuePair<int, AbstractShipWeapon> registeredWeapon in _equiptWeapons)
            registeredWeapon.Value.SetShootCommand(_shootInput);
    }

    public void DisableWeapons()
    {
        DisableSubsystem();
        OnWeaponsDisabled?.Invoke();
    }

    public void EnableWeapons()
    {
        EnableSubsystem();
        OnWeaponsEnabled?.Invoke();
    }

    public int GetWeaponCount()
    {
        return _equiptWeapons.Count;
    }

    public void InitializeGameManagerDependentReferences()
    {
        _factoryReference = GameManager.Instance.GetWeaponsFactory();
    }

    public bool IsWeaponsDisabled()
    {
        return _isDisabled;
    }

    public void SetParentShipAndInitializeAwakeReferences(AbstractShip parent)
    {
        _parentShip = parent;

        InitializeSlotVisualizer();
        InitializeSlotDictionary();
        InitializeEquiptWeaponsDictionary();

        //in case the weapon positions are populated thru the inspector before runtime
        ReinitializeWeapons();
    }

    public int GetSlotCount()
    {
        return _weaponSlots.Count;
    }

    public override void DisableSubsystem()
    {
        foreach (KeyValuePair<int,AbstractShipWeapon> registeredWeaponRecord in _equiptWeapons)
            registeredWeaponRecord.Value.SetSubsystemOnlineStatus(false);

        base.DisableSubsystem();
    }

    public override void EnableSubsystem()
    {
        foreach (KeyValuePair<int, AbstractShipWeapon> registeredWeaponRecord in _equiptWeapons)
            registeredWeaponRecord.Value.SetSubsystemOnlineStatus(true);

        base.EnableSubsystem();
    }


    public bool GetShootInput()
    {
        return _shootInput;
    }

    public void SetShootInput(bool value)
    {
        _shootInput = value;
    }



    //Debugging Debug.Log($"");
    private void ListenForDebugCommands()
    {
        if (_addWeaponCmd)
        {
            _addWeaponCmd = false;
            TestAddingWeaponToDebugSlot();
        }

        if (_removeWeaponCmd)
        {
            _removeWeaponCmd = false;
            TestRemovingWeaponFromDebugSlot();
        }

        if (_logWeaponAtSlotCmd)
        {
            _logWeaponAtSlotCmd = false;
            LogWeaponAtDebugSlot();
        }

        if (_disableWeaponsCmd)
        {
            _disableWeaponsCmd = false;
            DisableWeapons();
        }
            
        if (_enableWeaponsCmd)
        {
            _enableWeaponsCmd = false;
            EnableWeapons();
        }

        if (_addSlotCmd)
        {
            _addSlotCmd = false;
            TestAddingNewSlot();
        }

        if (_logSlotCountCmd)
        {
            _logSlotCountCmd = false;
            LogSlotCount();
        }

        if (_logWeaponCountCmd)
        {
            _logWeaponCountCmd = false;
            LogWeaponCount();
        }
        if (_addFullArsenalCmd)
        {
            _addFullArsenalCmd = false;
            AddFullArsenal();
        }

    }

    private void TestAddingWeaponToDebugSlot()
    {
        AddWeapon(_debugWeaponName, _debugSlotIndex);
    }

    private void TestRemovingWeaponFromDebugSlot()
    {
        RemoveWeapon(_debugSlotIndex);
    }

    private void LogWeaponAtDebugSlot()
    {
        string weaponName = "None";
        if (GetWeaponAtSlot(_debugSlotIndex) != null)
            weaponName = GetWeaponAtSlot(_debugSlotIndex).GetWeaponName();
        STKDebugLogger.LogStatement(_showDebug, $"Weapon at Slot {_debugSlotIndex}: {weaponName}");
    }

    private void TestAddingNewSlot()
    {
        AddWeaponSlot(_debugNewSlotPosition);
    }

    private void LogSlotCount()
    {
        STKDebugLogger.LogStatement(_showDebug,$"Current slot count: {GetSlotCount()}");
    }

    private void LogWeaponCount()
    {
        STKDebugLogger.LogStatement(_showDebug, $"Current weapon count: {GetWeaponCount()}");
    }

    private void AddFullArsenal()
    {
        AddWeapon("Generic Laser", 0);
        AddWeapon("Generic Laser", 1);
        AddWeapon("Generic Blaster", 2);
        AddWeapon("Generic Blaster", 3);
    }
}
