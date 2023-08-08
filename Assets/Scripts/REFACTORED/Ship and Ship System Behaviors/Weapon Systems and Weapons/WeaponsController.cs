using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponsController : ShipSubsystem, IWeaponsSubsystemBehavior
{
    //Declarations
    [Header("Weapons")]
    [SerializeField] private int _weaponCount;
    [SerializeField] private List<Transform> _weaponPositions;
    [SerializeField] private AbstractShipWeapon[] _weaponsReferenceList;
    [SerializeField] private WeaponFactory _factoryReference;

    [Header("Debugging")]
    [SerializeField] private bool _testAddWeapon = false;
    [SerializeField] private bool _testRemoveWeapon = false;
    [SerializeField] private bool _testGetWeaponAtPosition = false;
    [SerializeField] private bool _testEnableDisableWeapons = false;


    //events
    public delegate void WeaponryUpdateEvent(Transform weaponLocation, int referencePosition);
    public event WeaponryUpdateEvent OnWeaponAdded;
    public event WeaponryUpdateEvent OnWeaponRemoved;

    public delegate void WeaponsControllerEvent();
    public event WeaponsControllerEvent OnWeaponsDisabled;
    public event WeaponsControllerEvent OnWeaponsEnabled;



    //Monobehaviours
    private void Update()
    {
        if (_showDebug)
            ListenForDebugCommands();
    }




    //Interface Utils
    public void AddWeapon(string weaponName, int position)
    {
        if (position >= 0 && position < _weaponPositions.Count)
        {
            if (GetWeaponFromPosition(position) == null)
            {
                //Ask the factory to create a new weapon
                GameObject newWeaponObject = _factoryReference.CreateWeaponObject(weaponName);

                //bind the new weapon to the desired position.
                newWeaponObject.transform.SetParent(_weaponPositions[position]);
                newWeaponObject.transform.position = _weaponPositions[position].position;

                //Initialize the weapon
                newWeaponObject.GetComponent<IShipWeaponry>().SetParentSubsystemAndInitialize(this);

                //Update the weapon count and weapon references
                RebuildWeaponReferences();
                UpdateWeaponCountUsingWeaponReferences();

                OnWeaponAdded?.Invoke(_weaponPositions[position], position);
            }
            else
                Debug.LogWarning($"Caution: position {position} already holds a weapon on ship {_parentShip.GetName()}. " +
                    $"Ignoring request to add weapon {weaponName} to position {position}");
        }
        else
            Debug.LogWarning($"Caution: position {position} doesn't exist on ship {_parentShip.GetName()}" +
                $"Ignoring request to add weapon {weaponName} to position {position}");
        
    }

    public GameObject RemoveWeapon(int position)
    {
        GameObject removedWeapon = null;
        if (position >= 0 && position < _weaponsReferenceList.Length)
        {
            
            if (GetWeaponFromPosition(position) == null)
                Debug.LogWarning($"Caution: no weapon exists at position {position} on ship {_parentShip.GetName()}." +
                    $"Ignoring request to remove weapon from position {position}");
            else
            {
                removedWeapon = _weaponsReferenceList[position].gameObject;
                removedWeapon.SetActive(false);

                //Move the weapon from its position in the weapon system
                removedWeapon.transform.SetParent(GameManager.Instance.GetInstanceTracker().GetWeaponContainer()); ;


                RebuildWeaponReferences();
                UpdateWeaponCountUsingWeaponReferences();
            }
        }
        else
            Debug.LogWarning($"Caution: Attempted OutOfBounds removal of weapon at position {position} on {_parentShip.GetName()}." +
                $"Ignoring request to remove weapon from position {position}");

        return removedWeapon;
        
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

    public void FireWeapons()
    {
        foreach (IShipWeaponry weapon in _weaponsReferenceList)
        {
            if (weapon != null)
                weapon.FireWeapon();
        }
    }

    public int GetWeaponCount()
    {
        return _weaponCount;
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
        RebuildWeaponReferences();
        UpdateWeaponCountUsingWeaponReferences();

        //in case the weapon positions are populated thru the inspector before runtime
        ReinitializeWeapons();
    }

    public IShipWeaponry GetWeaponFromPosition(int position)
    {
        if (position >= 0 && position < _weaponsReferenceList.Length)
            return _weaponsReferenceList[position];
        else
        {
            Debug.LogWarning($"Weapon position {position} is out of bounds on ship {_parentShip.GetName()}" +
                $"Ignoring weapon-request on position {position}");
            return null;
        }
            
    }

    public int GetPositionCount()
    {
        return _weaponPositions.Count;
    }

    public int GetAvailablePosition()
    {
        for (int i = 0; i < _weaponsReferenceList.Length; i++)
        {
            if (_weaponsReferenceList[i] == null)
                return i;
        }

        return -1;
    }




    //Utils
    public override void DisableSubsystem()
    {
        foreach (IShipWeaponry weapon in _weaponsReferenceList)
        {
            if (weapon != null)
                weapon.SetSubsystemOnlineStatus(false);
        }

        base.DisableSubsystem();
    }

    public override void EnableSubsystem()
    {
        foreach (IShipWeaponry weapon in _weaponsReferenceList)
        {
            if (weapon != null)
                weapon.SetSubsystemOnlineStatus(true);
        }

        base.EnableSubsystem();
    }

    public void RebuildWeaponReferences()
    {
        _weaponsReferenceList = new AbstractShipWeapon[_weaponPositions.Count];

        for (int i = 0; i < _weaponPositions.Count; i++)
        {
            if (_weaponPositions[i].childCount > 0)
                _weaponsReferenceList[i] = _weaponPositions[i].GetChild(0).gameObject.GetComponent<AbstractShipWeapon>();
        }            
    }

    public void UpdateWeaponCountUsingWeaponReferences()
    {
        int newWeaponCount = 0;
        foreach (IShipWeaponry weapon in _weaponsReferenceList)
        {
            if (weapon != null)
                newWeaponCount++;
        }

        _weaponCount = newWeaponCount;
    }

    public void ReinitializeWeapons()
    {
        foreach (IShipWeaponry weapon in _weaponsReferenceList)
        {
            if (weapon != null)
                weapon.SetParentSubsystemAndInitialize(this);
        }
    }




    //Debugging Debug.Log($"");
    private void ListenForDebugCommands()
    {
        if (_testAddWeapon)
        {
            _testAddWeapon = false;
            AddWeaponTest();
        }
        if (_testRemoveWeapon)
        {
            _testRemoveWeapon = false;
            RemoveWeaponTest();
        }
        if (_testGetWeaponAtPosition)
        {
            _testGetWeaponAtPosition = false;
            GetWeaponFromPositionTest();
        }
        if (_testEnableDisableWeapons)
        {
            _testEnableDisableWeapons = false;
            EnableAndDisableWeaponsTest();
        }
    }

    private void AddWeaponTest()
    {
        //Testing AddWeapon.
        Debug.Log($"Testing Adding weapons...");

        Debug.Log($"Adding 'Test Weapon' to all positions...");
        for (int i = 0; i < _weaponsReferenceList.Length; i++)
            AddWeapon("Test Weapon", i);

        //Add weapon to nonexistent position
        Debug.Log($"Adding 'Test Weapon' to position that doesn't exist. Expect a warning...");
        AddWeapon("Test Weapon", _weaponsReferenceList.Length);

        //Add weapon to a position with a preexisting weapon
        Debug.Log($"Adding 'Test Weapon' to position that already holds a weapon. Expect a warning...");
        AddWeapon("Test Weapon", 0);

        //Add undefined weapon
        Debug.Log($"Adding undefined weapon to posiion 0. Expect an Error 'Weapon doesnt exist'...");
        AddWeapon("Nonexistent Weapon", 0);

        Debug.Log($"Add Weapon Testing Completed");

    }

    private void RemoveWeaponTest()
    {
        //Testing RemoveWeapon
        Debug.Log($"Testing Removing Weapons...");

        //Remove valid weapon
        Debug.Log($"Removing Weapon from an occupied position...");
        int position = 0;
        if (_weaponCount > 0)
        {
            //find an occupied position
           for (int i =0; i < _weaponsReferenceList.Length; i++)
            {
                if (_weaponsReferenceList[i] != null)
                    position = i;
            }

            Debug.Log($"Attempting to remove weapon from position {position}..."); 
            GameObject removedWeapon = RemoveWeapon(position);
            Debug.Log($"Removed {removedWeapon.GetComponent<IShipWeaponry>().GetWeaponName()} from position {position}");
        }
        else Debug.Log($"No weapons exist on ship {_parentShip.GetName()} to remove!");

        //remove nonexistent weapon
        Debug.Log($"Attempting to remove weapon from empty position {position}. Expect a warning...");
        RemoveWeapon(position);

        Debug.Log($"REmove Weapon Testing Completed");



    }

    private void EnableAndDisableWeaponsTest()
    {
        //Test Enabling/disabling weapons
        Debug.Log($"Testing Weapon Enabling and Disabling...");
        Debug.Log($"Disabling Weapons and Firing. Expect no weapons fire...");
        DisableWeapons();
        FireWeapons();
        
        Debug.Log($"Enabling Weapons and Firing. Expect weapons fire...");
        EnableWeapons();
        FireWeapons();

        Debug.Log($"Weapon Enabling and Disabling Completed");
    }

    private void GetWeaponFromPositionTest()
    {
        //Test Getting weapons from slots
        Debug.Log($"Testing Getting Weapon from all positions and one Out of bounds position...");
        Debug.Log($"Testing InBounds positions...");
        for (int i = 0; i < _weaponsReferenceList.Length; i++)
            Debug.Log($"Weapon at position {i}: {GetWeaponFromPosition(i)}");

        Debug.Log($"Testing OutofBounds position...");
        Debug.Log($"Weapon at position {_weaponsReferenceList.Length}: {GetWeaponFromPosition(_weaponsReferenceList.Length)}");

        Debug.Log($"Getting Weapon from position test Completed");
    }





}
