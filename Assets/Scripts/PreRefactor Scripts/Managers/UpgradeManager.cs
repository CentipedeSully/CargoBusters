using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;
using TMPro;
using UnityEngine.Events;

public class UpgradeManager : MonoSingleton<UpgradeManager>
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
    [SerializeField] private BubbleCollectionController _weaponsBubbleController;


    [Space(20)]
    [SerializeField] private bool _isBlasterCountUpgradeAvailable = false;
    [SerializeField] private int _blasterCountUpgradeMax = 2;
    [SerializeField] private int _blasterCountUpgradeCurrent = 0;
    [SerializeField] private GameObject _blasterCountUpgradeButton;

    [Space(20)]
    [SerializeField] private bool _isBlasterCooldownUpgradeAvailable = false;
    [SerializeField] private int _cooldownUpgradeMax = 2;
    [SerializeField] private int _cooldownUpgradeCurrent = 0;
    [SerializeField] private float _cooldownUpgradeModifier = .15f;
    [SerializeField] private GameObject _cooldownUpgradeButton;

    [Space(20)]
    [SerializeField] private bool _isBlasterDamageUpgradeAvailable = false;
    [SerializeField] private int _damageUpgradeMax = 1;
    [SerializeField] private int _damageUpgradeCurrent = 0;
    [SerializeField] private GameObject _damageUpgradeButton;

    [Space(20)]
    [SerializeField] private bool _isBlasterKillNegationChanceUpgradeAvailable = false;
    [SerializeField] private int _killNegationChanceUpgradeMax = 2;
    [SerializeField] private int _killNegationChanceUpgradeCurrent = 0;
    [SerializeField] private float _killNegationChanceModifier = .44f;
    [SerializeField] private GameObject _killNegationUpgradeButton;


    [Header("Engines")]
    [SerializeField] private int _engineUpgradesMax = 4;
    [SerializeField] private int _engineUpgradesCurrent = 0;
    [SerializeField] private List<Materials> _enginesUpgradeMaterialsList;
    [SerializeField] private List<int> _initialEnginesMaterialCounts;
    [SerializeField] private List<int> _incrementalEnginesMaterialCounts;
    [SerializeField] private List<int> _currentEnginesUpgradePrice;
    [SerializeField] private BubbleCollectionController _enginesBubbleController;

    [Space(20)]
    [SerializeField] private bool _isEngineForwardsSpeedUpgradeAvailable = false;
    [SerializeField] private int _forwardSpeedUpgradeMax = 3;
    [SerializeField] private int _forwardSpeedUpgradeCurrent = 0;
    [SerializeField] private float _forwardSpeedModifier = 250;
    [SerializeField] private GameObject _engineSpeedUpgradeButton;

    [Space(20)]
    [SerializeField] private bool _isEngineTurnSpeedUpgradeAvailable = false;
    [SerializeField] private int _turnSpeedUpgradeMax = 3;
    [SerializeField] private int _turnSpeedUpgradeCurrent = 0;
    [SerializeField] private float _turnSpeedModifier = 60;
    [SerializeField] private GameObject _engineTurnUpgradeButton;


    [Header("Hull")]
    [SerializeField] private int _hullUpgradesMax = 5;
    [SerializeField] private int _hullUpgradesCurrent = 0;
    [SerializeField] private List<Materials> _hullUpgradeMaterialsList;
    [SerializeField] private List<int> _initialHullMaterialCounts;
    [SerializeField] private List<int> _incrementalHullMaterialCounts;
    [SerializeField] private List<int> _currentHullUpgradePrice;
    [SerializeField] private BubbleCollectionController _hullBubbleController;
    [Space(20)]
    public UnityEvent OnHullDurabilityIncreased;

    [Space(20)]
    [SerializeField] private bool _isHullDurabilityUpgradeAvailable = false;
    [SerializeField] private int _hullDurabilityUpgradeCurrent = 0;
    [SerializeField] private int _hullDurabilityUpgradeMax = 2;
    [SerializeField] private GameObject _hullDurabilityUpgradeButton;

    [Space(20)]
    [SerializeField] private bool _isHullRegenUpgradeAvailable = false;
    [SerializeField] private int _hullRegenUpgradeCurrent = 0;
    [SerializeField] private float _hullRegenModifier = 1;
    [SerializeField] private GameObject _hullRegenUpgradeButton;

    [Header("Shields")]
    [SerializeField] private int _shieldsUpgradesMax = 5;
    [SerializeField] private int _shieldsUpgradesCurrent = 0;
    [SerializeField] private List<Materials> _shieldsUpgradeMaterialsList;
    [SerializeField] private List<int> _initialShieldsMaterialCounts;
    [SerializeField] private List<int> _incrementalShieldsMaterialCounts;
    [SerializeField] private List<int> _currentShieldsUpgradePrice;
    [SerializeField] private BubbleCollectionController _shieldsBubbleController;
    [Space(20)]
    public UnityEvent OnShieldsCapacityIncreased;

    [Space(20)]
    [SerializeField] private bool _isShieldCapacityUpgradeAvailable = false;
    [SerializeField] private int _shieldCapacityUpgradeCurrent = 0;
    [SerializeField] private int _shieldCapacityUpgradeMax = 5;
    [SerializeField] private GameObject _shieldCapacityUpgradeButton;

    [Space(20)]
    [SerializeField] private bool _isShieldRegenUpgradeAvailable = false;
    [SerializeField] private int _shieldRegenUpgradeCurrent = 0;
    [SerializeField] private float _shieldRegenModifier = .1f;
    [SerializeField] private GameObject _shieldRegenUpgradeButton;

    [Header("CargoBuster")]
    [SerializeField] private int _busterUpgradesMax = 2;
    [SerializeField] private int _busterUpgradesCurrent = 0;
    [SerializeField] private List<Materials> _busterUpgradeMaterialsList;
    [SerializeField] private List<int> _initialBusterMaterialCounts;
    [SerializeField] private List<int> _incrementalBusterMaterialCounts;
    [SerializeField] private List<int> _currentBusterUpgradePrice;
    [SerializeField] private BubbleCollectionController _AuxBubbleController;

    [Space(20)]
    [SerializeField] private bool _isBusterTimeUpgradeAvailable = false;
    [SerializeField] private float _busterTimeModifier = 1.5f;
    [SerializeField] private GameObject _busterTimeUpgradeButton;

    [Space(20)]
    [SerializeField] private bool _isBusterDropChanceUpgradeAvailable = false;
    [SerializeField] private float _busterDropChanceModifier = .15f;
    [SerializeField] private GameObject _dropChanceUpgradeButton;

    [Header("ScrapHarvester")]
    [SerializeField] private bool _isScrapHarvesterUpgradeAvailable = false;
    [SerializeField] private List<Materials> _scrapHarvesterUpgradeMaterialsList;
    [SerializeField] private List<int> _currentScrapHarvesterUpgradePrice;
    [SerializeField] private bool _isScrapHarvesterPurchased = false;
    [SerializeField] private GameObject _HarvesterUpgradeButton;

    [Header("WarpCore")]
    [SerializeField] private bool _isWarpCoreUpgradeAvailable = false;
    [SerializeField] private bool _isWarpCoreRepaired = false;
    [SerializeField] private List<Materials> _warpCoreUpgradeMaterialsList;
    [SerializeField] private List<int> _warpCoreUpgradePrice;
    [SerializeField] private GameObject _fixCoreUpgradeButton;

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
    //...



    //Utilites
    public void UpdateUpgradeAvailability()
    {
        UpdateWeaponUpgradeAvailability();

        UpdateEnginesUpgradeAvailability();

        UpdateHullUpgradeAvailability();

        UpdateShieldsUpgradeAvailability();

        UpdateBusterUpgradeAvailability();

        UpdateAuxillaryUpgradesAvailability();
    }

    private void UpdateWeaponUpgradeAvailability()
    {
        if (_weaponsUpgradesCurrent >= _weaponsUpgradesMax)
        {
            _isBlasterCooldownUpgradeAvailable = false;
            _isBlasterCountUpgradeAvailable = false;
            _isBlasterDamageUpgradeAvailable = false;
            _isBlasterKillNegationChanceUpgradeAvailable = false;
        }
        else
        {
            //cooldown
            if (_cooldownUpgradeCurrent >= _cooldownUpgradeMax || !IsUpgradePriceAffordable(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice))
                _isBlasterCooldownUpgradeAvailable = false;
            else _isBlasterCooldownUpgradeAvailable = true;

            //Blaster count
            if (_blasterCountUpgradeCurrent >= _blasterCountUpgradeMax || !IsUpgradePriceAffordable(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice))
                _isBlasterCountUpgradeAvailable = false;
            else _isBlasterCountUpgradeAvailable = true;

            //Damage
            if (_damageUpgradeCurrent >= _damageUpgradeMax || !IsUpgradePriceAffordable(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice))
                _isBlasterDamageUpgradeAvailable = false;
            else _isBlasterDamageUpgradeAvailable = true;

            //KillNegation
            if (_killNegationChanceUpgradeCurrent >= _killNegationChanceUpgradeMax || !IsUpgradePriceAffordable(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice))
                _isBlasterKillNegationChanceUpgradeAvailable = false;
            else _isBlasterKillNegationChanceUpgradeAvailable = true;
        }

        if (_isBlasterCooldownUpgradeAvailable)
            _cooldownUpgradeButton.gameObject.SetActive(true);
        else _cooldownUpgradeButton.gameObject.SetActive(false);

        if (_isBlasterDamageUpgradeAvailable)
            _damageUpgradeButton.gameObject.SetActive(true);
        else _damageUpgradeButton.gameObject.SetActive(false);

        if (_isBlasterKillNegationChanceUpgradeAvailable)
            _killNegationUpgradeButton.gameObject.SetActive(true);
        else _killNegationUpgradeButton.gameObject.SetActive(false);

        if (_isBlasterCountUpgradeAvailable)
            _blasterCountUpgradeButton.gameObject.SetActive(true);
        else _blasterCountUpgradeButton.gameObject.SetActive(false);
    }

    private void UpdateEnginesUpgradeAvailability()
    {
        if (_engineUpgradesCurrent >= _engineUpgradesMax)
        {
            _isEngineForwardsSpeedUpgradeAvailable = false;
            _isEngineTurnSpeedUpgradeAvailable = false;
        }
        else
        {
            if (_forwardSpeedUpgradeCurrent >= _forwardSpeedUpgradeMax || !IsUpgradePriceAffordable(_enginesUpgradeMaterialsList, _currentEnginesUpgradePrice))
                _isEngineForwardsSpeedUpgradeAvailable = false;
            else _isEngineForwardsSpeedUpgradeAvailable = true;

            if (_turnSpeedUpgradeCurrent >= _turnSpeedUpgradeMax || !IsUpgradePriceAffordable(_enginesUpgradeMaterialsList, _currentEnginesUpgradePrice))
                _isEngineTurnSpeedUpgradeAvailable = false;
            else _isEngineTurnSpeedUpgradeAvailable = true;
        }

        if (_isEngineForwardsSpeedUpgradeAvailable)
            _engineSpeedUpgradeButton.gameObject.SetActive(true);
        else _engineSpeedUpgradeButton.gameObject.SetActive(false);

        if (_isEngineTurnSpeedUpgradeAvailable)
            _engineTurnUpgradeButton.gameObject.SetActive(true);
        else _engineTurnUpgradeButton.gameObject.SetActive(false);
    }

    private void UpdateHullUpgradeAvailability()
    {
        if (_hullUpgradesCurrent >= _hullUpgradesMax)
        {
            _isHullDurabilityUpgradeAvailable = false;
            _isHullRegenUpgradeAvailable = false;
        }
        else
        {
            if (_hullDurabilityUpgradeCurrent >= _hullDurabilityUpgradeMax || !IsUpgradePriceAffordable(_hullUpgradeMaterialsList, _currentHullUpgradePrice))
                _isHullDurabilityUpgradeAvailable = false;
            else _isHullDurabilityUpgradeAvailable = true;

            if (!IsUpgradePriceAffordable(_hullUpgradeMaterialsList, _currentHullUpgradePrice))
                _isHullRegenUpgradeAvailable = false;
            else _isHullRegenUpgradeAvailable = true;
        }

        if (_isHullDurabilityUpgradeAvailable)
            _hullDurabilityUpgradeButton.gameObject.SetActive(true);
        else _hullDurabilityUpgradeButton.gameObject.SetActive(false);

        if (_isHullRegenUpgradeAvailable)
            _hullRegenUpgradeButton.gameObject.SetActive(true);
        else _hullRegenUpgradeButton.gameObject.SetActive(false);
    }

    private void UpdateShieldsUpgradeAvailability()
    {
        if (_shieldsUpgradesCurrent >= _shieldsUpgradesMax)
        {
            _isShieldCapacityUpgradeAvailable = false;
            _isShieldRegenUpgradeAvailable = false;
        }
        else
        {
            if (_shieldCapacityUpgradeCurrent >= _shieldCapacityUpgradeMax || !IsUpgradePriceAffordable(_shieldsUpgradeMaterialsList, _currentShieldsUpgradePrice))
                _isShieldCapacityUpgradeAvailable = false;
            else _isShieldCapacityUpgradeAvailable = true;

            if (!IsUpgradePriceAffordable(_shieldsUpgradeMaterialsList, _currentShieldsUpgradePrice))
                _isShieldRegenUpgradeAvailable = false;
            else _isShieldRegenUpgradeAvailable = true;
        }

        if (_isShieldCapacityUpgradeAvailable)
            _shieldCapacityUpgradeButton.gameObject.SetActive(true);
        else _shieldCapacityUpgradeButton.gameObject.SetActive(false);

        if (_isShieldRegenUpgradeAvailable)
            _shieldRegenUpgradeButton.gameObject.SetActive(true);
        else _shieldRegenUpgradeButton.gameObject.SetActive(false);

    }

    private void UpdateBusterUpgradeAvailability()
    {
        if (_busterUpgradesCurrent >= _busterUpgradesMax)
        {
            _isBusterDropChanceUpgradeAvailable = false;
            _isBusterTimeUpgradeAvailable = false;
        }
        else
        {
            if (!IsUpgradePriceAffordable(_busterUpgradeMaterialsList, _currentBusterUpgradePrice))
            {
                _isBusterDropChanceUpgradeAvailable = false;
                _isBusterTimeUpgradeAvailable = false;
            }
            else
            {
                _isBusterTimeUpgradeAvailable = true;
                _isBusterDropChanceUpgradeAvailable = true;
            }
                
        }

        if (_isBusterTimeUpgradeAvailable)
            _busterTimeUpgradeButton.gameObject.SetActive(true);
        else _busterTimeUpgradeButton.gameObject.SetActive(false);

        if (_isBusterDropChanceUpgradeAvailable)
            _dropChanceUpgradeButton.gameObject.SetActive(true);
        else _dropChanceUpgradeButton.gameObject.SetActive(false);

    }

    private void UpdateAuxillaryUpgradesAvailability()
    {
        if (_isWarpCoreRepaired || !IsUpgradePriceAffordable(_warpCoreUpgradeMaterialsList, _warpCoreUpgradePrice))
            _isWarpCoreUpgradeAvailable = false;
        else _isWarpCoreUpgradeAvailable = true;

        if (_isScrapHarvesterPurchased || !IsUpgradePriceAffordable(_scrapHarvesterUpgradeMaterialsList, _currentScrapHarvesterUpgradePrice))
            _isScrapHarvesterUpgradeAvailable = false;
        else _isScrapHarvesterUpgradeAvailable = true;

        if (_isWarpCoreUpgradeAvailable)
            _fixCoreUpgradeButton.gameObject.SetActive(true);
        else _fixCoreUpgradeButton.gameObject.SetActive(false);

        if (_isScrapHarvesterUpgradeAvailable)
            _HarvesterUpgradeButton.gameObject.SetActive(true);
        else _HarvesterUpgradeButton.gameObject.SetActive(false);
    }

    private void RemoveMaterialsFromInventory(List<Materials> materialsList, List<int> priceList)
    {
        for (int i = 0; i < materialsList.Count; i++)
            _inventoryRef.DecrementItemCount((int)materialsList[i], priceList[i]);
    }

    private void IncreaseUpgradePrice(ref List<int> currentPriceList, List<int> incrementalPriceList)
    {
        for (int i = 0; i < currentPriceList.Count; i++)
            currentPriceList[i] += incrementalPriceList[i];
    }

    private bool IsUpgradePriceAffordable(List<Materials> materialsList, List<int> priceList)
    {
        for (int i = 0; i < materialsList.Count; i++)
        {
            if (PlayerInventoryManager.Instance.GetItemCount((int)materialsList[i]) < priceList[i])
                return false;
        }

        return true;
    }

    private string BuildPriceString(List<Materials> materialsList, List<int> priceList)
    {
        string priceString = "";
        for (int i = 0; i < materialsList.Count; i++)
            priceString += materialsList[i].ToString() + " : " + priceList[i] + "\n";

        return priceString;
    }


    //External Control Utils
    public void InitializeUpgrader()
    {
        _inventoryRef = PlayerInventoryManager.Instance;

        _playerWeaponsObject = OldPlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetWeaponsObject();
        _playerEnginesObject = OldPlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetEnginesObject();
        _playerHullObject = OldPlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetHullObject();
        _playerShieldsObject = OldPlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetShieldsObject();
        _playerWarpCoreObject = OldPlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetWarpCoreObject();
        _playerBusterObject = OldPlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetCargoBuster();

        _playerBlastersArray = _playerWeaponsObject.GetComponentsInChildren<SpawnLaserOnCommand>();



        //Set Current Upgrade Prices to the Initial Prices
        _currentWeaponsUpgradePrice = new List<int>(_initialWeaponsMaterialCounts);
        _currentEnginesUpgradePrice = new List<int>(_initialEnginesMaterialCounts);
        _currentHullUpgradePrice = new List<int>(_initialHullMaterialCounts);
        _currentShieldsUpgradePrice = new List<int>(_initialShieldsMaterialCounts);
        _currentBusterUpgradePrice = new List<int>(_initialBusterMaterialCounts);

        UpdateUpgradeAvailability();
        OldUiManager.Instance.GetUpgradeDescController().InitializeReferences();
    }

    public void UpgradeWeaponsCooldown()
    {
        if (_isBlasterCooldownUpgradeAvailable && IsUpgradePriceAffordable(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice))
        {
            //Take Materials from Inventory
            RemoveMaterialsFromInventory(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice);
            IncreaseUpgradePrice(ref _currentWeaponsUpgradePrice, _incrementalWeaponsMaterialCounts);

            //Upgrade System
            foreach (SpawnLaserOnCommand blaster in _playerBlastersArray)
                blaster.SetCooldown(blaster.GetCooldown() - _cooldownUpgradeModifier);

            //Track Upgrade Progress
            _cooldownUpgradeCurrent++;
            _weaponsUpgradesCurrent++;

            _weaponsBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeWeaponsDamage()
    {
        if (_isBlasterDamageUpgradeAvailable && IsUpgradePriceAffordable(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice);
            IncreaseUpgradePrice(ref _currentWeaponsUpgradePrice, _incrementalWeaponsMaterialCounts);

            //Upgrade System
            foreach (SpawnLaserOnCommand blaster in _playerBlastersArray)
                blaster.SetDamage(blaster.GetDamage() + 1);

            //Track Upgrade Progress
            _damageUpgradeCurrent++;
            _weaponsUpgradesCurrent++;

            _weaponsBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeWeaponsBlasterCount()
    {
        if (_isBlasterCountUpgradeAvailable && IsUpgradePriceAffordable(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice);
            IncreaseUpgradePrice(ref _currentWeaponsUpgradePrice, _incrementalWeaponsMaterialCounts);

            //Upgrade System
            foreach (SpawnLaserOnCommand blaster in _playerBlastersArray)
            {
                if (blaster.IsBlasterEnabled() == false)
                {
                    blaster.EnableBlaster();
                    break;
                }
            }

            //Track Upgrade Progress
            _blasterCountUpgradeCurrent++;
            _weaponsUpgradesMax++;

            _weaponsBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeWeaponsKillNegationChance()
    {
        if (_isBlasterKillNegationChanceUpgradeAvailable && IsUpgradePriceAffordable(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_weaponsUpgradeMaterialsList, _currentWeaponsUpgradePrice);
            IncreaseUpgradePrice(ref _currentWeaponsUpgradePrice, _incrementalWeaponsMaterialCounts);

            //Upgrade System
            KillNegater.Instance.IncreaseKillNegationChance((int)(_killNegationChanceModifier * 100));

            //Track Upgrade Progress
            _killNegationChanceUpgradeCurrent++;
            _weaponsUpgradesCurrent++;

            _weaponsBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeEngineSpeed()
    {
        if (_isEngineForwardsSpeedUpgradeAvailable && IsUpgradePriceAffordable(_enginesUpgradeMaterialsList, _currentEnginesUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_enginesUpgradeMaterialsList, _currentEnginesUpgradePrice);
            IncreaseUpgradePrice(ref _currentEnginesUpgradePrice, _incrementalEnginesMaterialCounts);

            //Upgrade system
            _playerEnginesObject.GetComponent<MoveObject>().SetSpeed(_playerEnginesObject.GetComponent<MoveObject>().GetSpeed() + _forwardSpeedModifier);

            //track progress
            _forwardSpeedUpgradeCurrent++;
            _engineUpgradesCurrent++;

            _enginesBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeEngineTurn()
    {
        if (_isEngineTurnSpeedUpgradeAvailable && IsUpgradePriceAffordable(_enginesUpgradeMaterialsList, _currentEnginesUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_enginesUpgradeMaterialsList, _currentEnginesUpgradePrice);
            IncreaseUpgradePrice(ref _currentEnginesUpgradePrice, _incrementalEnginesMaterialCounts);

            //upgrade system
            _playerEnginesObject.GetComponent<AddRotationToObject>().SetRotationSpeed(_playerEnginesObject.GetComponent<AddRotationToObject>().GetRotationSpeed() + _turnSpeedModifier);

            //track progress
            _turnSpeedUpgradeCurrent++;
            _engineUpgradesCurrent++;

            _enginesBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeHullDurability()
    {
        if (_isHullDurabilityUpgradeAvailable && IsUpgradePriceAffordable(_hullUpgradeMaterialsList, _currentHullUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_hullUpgradeMaterialsList, _currentHullUpgradePrice);
            IncreaseUpgradePrice(ref _currentHullUpgradePrice, _incrementalHullMaterialCounts);

            //Upgrade systems
            _playerHullObject.GetComponent<IntegrityBehavior>().SetMaxIntegrity((int)_playerHullObject.GetComponent<IntegrityBehavior>().GetMaxIntegrity() + 1);
            OnHullDurabilityIncreased?.Invoke();

            //track progress
            _hullDurabilityUpgradeCurrent++;
            _hullUpgradesCurrent++;

            _hullBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeHullRegenRate()
    {
        if (_isHullRegenUpgradeAvailable && IsUpgradePriceAffordable(_hullUpgradeMaterialsList, _currentHullUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_hullUpgradeMaterialsList, _currentHullUpgradePrice);
            IncreaseUpgradePrice(ref _currentHullUpgradePrice, _incrementalHullMaterialCounts);

            //upgrade Sysytems
            if (_playerHullObject.GetComponent<HullSystemController>().IsRegenUnlocked() == false)
                _playerHullObject.GetComponent<HullSystemController>().UnlockHullRegeneration();
            else 
                _playerHullObject.GetComponent<Regenerator>().SetTickDuration(_playerHullObject.GetComponent<Regenerator>().GetTickDuration() - _hullRegenModifier);

            //track progress
            _hullRegenUpgradeCurrent++;
            _hullUpgradesCurrent++;

            _hullBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeShieldsCapacity()
    {
        if (_isShieldCapacityUpgradeAvailable && IsUpgradePriceAffordable(_shieldsUpgradeMaterialsList, _currentShieldsUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_shieldsUpgradeMaterialsList, _currentShieldsUpgradePrice);
            IncreaseUpgradePrice(ref _currentShieldsUpgradePrice, _incrementalShieldsMaterialCounts);

            //upgrade system
            _playerShieldsObject.GetComponent<IntegrityBehavior>().SetMaxIntegrity((int)_playerShieldsObject.GetComponent<IntegrityBehavior>().GetMaxIntegrity() + 1);
            _playerShieldsObject.GetComponent<Regenerator>().StartRegen();
            OnShieldsCapacityIncreased?.Invoke();


            //track progress
            _shieldCapacityUpgradeCurrent++;
            _shieldsUpgradesCurrent++;

            _shieldsBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeShieldRegenRate()
    {
        if (_isShieldRegenUpgradeAvailable && IsUpgradePriceAffordable(_shieldsUpgradeMaterialsList, _currentShieldsUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_shieldsUpgradeMaterialsList, _currentShieldsUpgradePrice);
            IncreaseUpgradePrice(ref _currentShieldsUpgradePrice, _incrementalShieldsMaterialCounts);

            //upgrade systems
            _playerShieldsObject.GetComponent<Regenerator>().SetTickDuration(_playerShieldsObject.GetComponent<Regenerator>().GetTickDuration() - _shieldRegenModifier);

            //track progress
            _shieldRegenUpgradeCurrent++;
            _shieldsUpgradesCurrent++;

            _shieldsBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void FixWarpCore()
    {
        if (_isWarpCoreUpgradeAvailable && IsUpgradePriceAffordable(_warpCoreUpgradeMaterialsList, _warpCoreUpgradePrice))
        {
            //remove materials from inventory
            RemoveMaterialsFromInventory(_warpCoreUpgradeMaterialsList, _warpCoreUpgradePrice);

            //fix system
            _playerWarpCoreObject.GetComponent<WarpCoreSystemController>().RepairWarpCore();

            //track progress
            _isWarpCoreRepaired = true;

            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeBusterDuration()
    {
        if (_isBusterTimeUpgradeAvailable && IsUpgradePriceAffordable(_busterUpgradeMaterialsList, _currentBusterUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_busterUpgradeMaterialsList, _currentBusterUpgradePrice);
            IncreaseUpgradePrice(ref _currentBusterUpgradePrice, _incrementalBusterMaterialCounts);

            //upgrade system
            _playerBusterObject.GetComponent<CargoBusterBehavior>().SetBusterDuration(_playerBusterObject.GetComponent<CargoBusterBehavior>().GetBusterDuration() - _busterTimeModifier);

            //track progress
            _busterUpgradesCurrent++;

            _AuxBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void UpgradeBusterDropChance()
    {
        if (_isBusterDropChanceUpgradeAvailable && IsUpgradePriceAffordable(_busterUpgradeMaterialsList, _currentBusterUpgradePrice))
        {
            //remove Materials fom inventory
            RemoveMaterialsFromInventory(_busterUpgradeMaterialsList, _currentBusterUpgradePrice);
            IncreaseUpgradePrice(ref _currentBusterUpgradePrice, _incrementalBusterMaterialCounts);

            //upgrade system
            CargoLootDropper.Instance.IncreaseDropRate((int)(_busterDropChanceModifier * 100));

            //track progress
            _busterUpgradesCurrent++;

            _AuxBubbleController.FillSingle();
            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }

    public void EnableScrapHarvester()
    {
        if (_isScrapHarvesterUpgradeAvailable && IsUpgradePriceAffordable(_scrapHarvesterUpgradeMaterialsList, _currentScrapHarvesterUpgradePrice))
        {
            //remove materials from inventory
            RemoveMaterialsFromInventory(_scrapHarvesterUpgradeMaterialsList, _currentScrapHarvesterUpgradePrice);

            //upgrade system
            ScrapHarvester.Instance.EnableHarvesting();

            //track progress
            _isScrapHarvesterPurchased = true;

            UpdateUpgradeAvailability();
            OldUiManager.Instance.GetUpgradeDescController().UpdateDescriptionValuesAndCosts();
        }
    }


    //Getters and Setters
    public List<int> GetWeaponsUpgradePrice()
    {
        return _currentWeaponsUpgradePrice;
    }

    public List<int> GetEnginesUpgradePrice()
    {
        return _currentEnginesUpgradePrice;
    }
    
    public List<int> GetHullUpgradePrice()
    {
        return _currentHullUpgradePrice;
    }

    public List<int> GetShieldsUpgradePrice()
    {
        return _currentShieldsUpgradePrice;
    }

    public List<int> GetBusterUpgradePrice()
    {
        return _currentBusterUpgradePrice;
    }

    public List<int> GetWarpCoreUpgradePrice()
    {
        return _warpCoreUpgradePrice;
    }

    public List<int> GetScrapHarvesterUpgradePrice()
    {
        return _currentScrapHarvesterUpgradePrice;
    }
}
