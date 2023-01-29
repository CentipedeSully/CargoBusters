using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //Declarations
    private enum Materials
    {
        Scrap,
        EnergyCells,
        WarpCoils,
        PlasmaAccelerators,
        CannonAlloys
    }

    [Header("Weapons")]
    [SerializeField] private int _weaponsUpgradesMax = 4;
    [SerializeField] private int _weaponsUpgradesCurrent = 0;
    [SerializeField] private List<Materials> _weaponsUpgradeMaterialsList;
    [SerializeField] private List<int> _initialWeaponsMaterialCounts;
    [SerializeField] private List<int> _incrementalWeaponsMaterialCounts;
    [SerializeField] private List<int> _currentWeaponsUpgradePrice;


    [Space(20)]
    [SerializeField] private bool _isBlasterCountUpgradeAvailable = false;
    [SerializeField] private int _blasterCountMax = 2;
    [SerializeField] private int _blasterCountUpgradeCurrent = 0;

    [Space(20)]
    [SerializeField] private bool _isBlasterCooldownUpgradeAvailable = false;
    [SerializeField] private int _cooldownUpgradeMax = 2;
    [SerializeField] private int _cooldownUpgradeCurrent = 0;
    [SerializeField] private float _cooldownUpgradeModifier = .15f;

    [Space(20)]
    [SerializeField] private bool _isBlasterDamageUpgradeAvailable = false;
    [SerializeField] private int _damageUpgradeMax = 1;
    [SerializeField] private int _damageUpgradeCurrent = 0;

    [Space(20)]
    [SerializeField] private bool _isBlasterKillNegationChanceUpgradeAvailable = false;
    [SerializeField] private int _killNegationChanceUpgradeMax = 2;
    [SerializeField] private int _killNegationChanceUpgradeCurrent = 0;
    [SerializeField] private float _killNegationChanceModifier = .44f;


    [Header("Engines")]
    [SerializeField] private int _engineUpgradesMax = 4;
    [SerializeField] private int _engineUpgradesCurrent = 0;
    [SerializeField] private List<Materials> _enginesUpgradeMaterialsList;
    [SerializeField] private List<int> _initialEnginesMaterialCounts;
    [SerializeField] private List<int> _incrementalEnginesMaterialCounts;
    [SerializeField] private List<int> _currentEnginesUpgradePrice;

    [Space(20)]
    [SerializeField] private bool _isEngineForwardsSpeedUpgradeAvailable = false;
    [SerializeField] private int _forwardSpeedUpgradeMax = 3;
    [SerializeField] private int _forwardSpeedUpgradeCurrent = 0;
    [SerializeField] private float _forwardSpeedModifier = 250;

    [Space(20)]
    [SerializeField] private bool _isEngineTurnSpeedUpgradeAvailable = false;
    [SerializeField] private int _turnSpeedUpgradeMax = 3;
    [SerializeField] private int _turnSpeedUpgradeCurrent = 0;
    [SerializeField] private float _turnSpeedModifier = 60;


    [Header("Hull")]
    [SerializeField] private int _hullUpgradesMax = 5;
    [SerializeField] private int _hullUpgradesCurrent = 0;
    [SerializeField] private List<Materials> _hullUpgradeMaterialsList;
    [SerializeField] private List<int> _initialHullMaterialCounts;
    [SerializeField] private List<int> _incrementalHullMaterialCounts;
    [SerializeField] private List<int> _currentHullUpgradePrice;

    [Space(20)]
    [SerializeField] private bool _isHullDurabilityUpgradeAvailable = false;
    [SerializeField] private int _hullDurabilityUpgradeCurrent = 0;
    [SerializeField] private int _hullDurabilityUpgradeMax = 2;

    [Space(20)]
    [SerializeField] private bool _isHullRegenUpgradeAvailable = false;
    [SerializeField] private int _hullRegenUpgradeCurrent = 0;
    [SerializeField] private float _hullRegenModifier = .1f;

    [Header("Shields")]
    [SerializeField] private int _shieldsUpgradesMax = 5;
    [SerializeField] private int _shieldsUpgradesCurrent = 0;
    [SerializeField] private List<Materials> _shieldsUpgradeMaterialsList;
    [SerializeField] private List<int> _initialShieldsMaterialCounts;
    [SerializeField] private List<int> _incrementalShieldsMaterialCounts;
    [SerializeField] private List<int> _currentShieldsUpgradePrice;

    [Space(20)]
    [SerializeField] private bool _isShieldCapacityUpgradeAvailable = false;
    [SerializeField] private int _shieldCapacityUpgradeCurrent = 0;
    [SerializeField] private int _shieldCapacityUpgradeMax = 5;

    [Space(20)]
    [SerializeField] private bool _isShieldRegenUpgradeAvailable = false;
    [SerializeField] private int _shieldRegenUpgradeCurrent = 0;
    [SerializeField] private float _shieldRegenModifier = .1f;

    [Header("CargoBuster")]
    [SerializeField] private int _busterUpgradesMax = 2;
    [SerializeField] private int _busterUpgradesCurrent = 0;
    [SerializeField] private List<Materials> _busterUpgradeMaterialsList;
    [SerializeField] private List<int> _initialBusterMaterialCounts;
    [SerializeField] private List<int> _incrementalBusterMaterialCounts;
    [SerializeField] private List<int> _currentBusterUpgradePrice;

    [Space(20)]
    [SerializeField] private bool _isBusterTimeUpgradeAvailable = false;
    [SerializeField] private float _busterTimeModifier = 1.5f;

    [Space(20)]
    [SerializeField] private bool _isBusterDropChanceUpgradeAvailable = false;
    [SerializeField] private float _busterDropChanceModifier = .15f;

    [Header("ScrapHarvester")]
    [SerializeField] private bool _isScrapHarvesterUpgradeAvailable = false;
    [SerializeField] private List<Materials> _ScrapHarvesterUpgradeMaterialsList;
    [SerializeField] private List<int> _currentScrapHarvesterUpgradePrice;
    [SerializeField] private bool _isScrapHarvesterPurchased = false;

    [Header("WarpCore")]
    [SerializeField] private bool _isWarpCoreUpgradeAvailable = false;
    [SerializeField] private bool _isWarpCoreRepaired = false;
    [SerializeField] private List<Materials> _warpCoreUpgradeMaterialsList;
    [SerializeField] private List<int> _warpCoreUpgradePrice;

    //references
    private PlayerInventoryManager _inventoryRef;
    private GameObject _playerWeaponsObject;
    private GameObject _playerEnginesObject;
    private GameObject _playerHullObject;
    private GameObject _playerShieldsObject;
    private CargoBusterBehavior _playerBusterObject;
    private GameObject _playerWarpCoreObject;
    private SpawnLaserOnCommand[] _playerBlastersArray;

    //Monobehaviors
    private void Awake()
    {
        _inventoryRef = PlayerInventoryManager.Instance;
        _playerWeaponsObject = PlayerObjectManager.Instance.GetComponent<ShipSystemReferencer>().GetWeaponsObject();
        _playerEnginesObject = PlayerObjectManager.Instance.GetComponent<ShipSystemReferencer>().GetEnginesObject();
        _playerHullObject = PlayerObjectManager.Instance.GetComponent<ShipSystemReferencer>().GetHullObject();
        _playerShieldsObject = PlayerObjectManager.Instance.GetComponent<ShipSystemReferencer>().GetShieldsObject();
        _playerWarpCoreObject = PlayerObjectManager.Instance.GetComponent<ShipSystemReferencer>().GetWarpCoreObject();
        _playerBusterObject = PlayerObjectManager.Instance.GetComponent<ShipSystemReferencer>().GetCargoBuster();

        _playerBlastersArray = _playerWeaponsObject.GetComponentsInChildren<SpawnLaserOnCommand>();
        //Set Current Upgrade Prices to the Initial Prices
        //...
    }



    //Utilites
    private void UpgradeWeaponsCooldown()
    {
        foreach (SpawnLaserOnCommand blaster in _playerBlastersArray)
            blaster.SetCooldown(blaster.GetCooldown() - _cooldownUpgradeModifier);

        _cooldownUpgradeCurrent++;
        _weaponsUpgradesCurrent++;
    }

    private void UpgradeWeaponsDamage()
    {
        foreach (SpawnLaserOnCommand blaster in _playerBlastersArray)
            blaster.SetDamage(blaster.GetDamage() + 1);

        _damageUpgradeCurrent++;
        _weaponsUpgradesCurrent++;
    }

    private void UpgradeWeaponsBlasterCount()
    {
        foreach (SpawnLaserOnCommand blaster in _playerBlastersArray)
        {
            if (blaster.IsBlasterEnabled() == false)
            {
                blaster.EnableBlaster();
                break;
            }
        }

        _blasterCountUpgradeCurrent++;
        _weaponsUpgradesMax++;
    }

    private void UpgradeWeaponsKillNegationChance() //!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        //Build Damage Negation Mechanic
        //Upgrade the Negation Chance

        _killNegationChanceUpgradeCurrent++;
        _weaponsUpgradesCurrent++;
    }

    private void UpgradeEngineSpeed()
    {
        _playerEnginesObject.GetComponent<MoveObject>().SetSpeed(_playerEnginesObject.GetComponent<MoveObject>().GetSpeed() + _forwardSpeedModifier);
        
        _forwardSpeedUpgradeCurrent++;
        _engineUpgradesCurrent++;
    }

    private void UpgradeEngineTurn()
    {
        _playerEnginesObject.GetComponent<AddRotationToObject>().SetRotationSpeed(_playerEnginesObject.GetComponent<AddRotationToObject>().GetRotationSpeed() + _turnSpeedModifier);

        _turnSpeedUpgradeCurrent++;
        _engineUpgradesCurrent++;
    }

    private void UpgradeHullDurability()
    {
        _playerHullObject.GetComponent<IntegrityBehavior>().SetMaxIntegrity((int)_playerHullObject.GetComponent<IntegrityBehavior>().GetMaxIntegrity() + 1);

        _hullDurabilityUpgradeCurrent++;
        _hullUpgradesCurrent++;
    }

    private void UpgradeHullRegenRate() //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        //Implement Hull Regenerator!

        _hullRegenUpgradeCurrent++;
        _hullUpgradesCurrent++;
    }

    private void UpgradeShieldsCapacity()
    {
        _playerShieldsObject.GetComponent<IntegrityBehavior>().SetMaxIntegrity((int)_playerShieldsObject.GetComponent<IntegrityBehavior>().GetMaxIntegrity() + 1);

        _shieldCapacityUpgradeCurrent++;
        _shieldsUpgradesCurrent++;
    }

    private void UpgradeShieldRegenRate()
    {
        _playerShieldsObject.GetComponent<Regenerator>().SetTickDuration(_playerShieldsObject.GetComponent<Regenerator>().GetTickDuration() - _shieldRegenModifier);

        _shieldRegenUpgradeCurrent++;
        _shieldsUpgradesCurrent++;
    }

    private void FixWarpCore()
    {
        _playerWarpCoreObject.GetComponent<WarpCoreSystemController>().EnableSystem();
        _isWarpCoreRepaired = true;
    }

    private void UpgradeBusterDuration()
    {
        _playerBusterObject.GetComponent<CargoBusterBehavior>().SetBusterDuration(_playerBusterObject.GetComponent<CargoBusterBehavior>().GetBusterDuration() - _busterTimeModifier);

        _busterUpgradesCurrent++;
    }

    private void UpgradeBusterDropChance()
    {
        CargoLootDropper.Instance.IncreaseDropRate((int)(_busterDropChanceModifier * 100));
        _busterUpgradesCurrent++;
    }

    private void EnableScrapHarvester()
    {
        ScrapHarvester.Instance.EnableHarvesting();
        _isScrapHarvesterPurchased = true;
    }

    private void RemoveMaterialsFromInventory(List<Materials> materialsList, List<int> priceList)
    {
        for (int i = 0; i < materialsList.Count; i++)
            _inventoryRef.IncrementItemCount((int)materialsList[i], priceList[i]);
    }

    private void IncreaseUpgradePrice(ref List<int> currentPriceList, List<int> incrementalPriceList)
    {
        for (int i = 0; i < currentPriceList.Count; i++)
            currentPriceList[i] += incrementalPriceList[i];
    }


    //External Control Utils


    //Getters and Setters





}
