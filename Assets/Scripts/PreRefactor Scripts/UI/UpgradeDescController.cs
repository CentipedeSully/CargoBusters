using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeDescController : MonoBehaviour
{
    //declarations
    [Header("Description Panel Objects")]
    [SerializeField] private GameObject _damagePanel;
    [SerializeField] private GameObject _blasterPanel;
    [SerializeField] private GameObject _cooldownPanel;
    [SerializeField] private GameObject _negateKillPanel;
    [SerializeField] private GameObject _engineSpeedPanel;
    [SerializeField] private GameObject _engineTurnPanel;
    [SerializeField] private GameObject _hullDurabilityPanel;
    [SerializeField] private GameObject _hullRegenPanel;
    [SerializeField] private GameObject _shieldCapacityPanel;
    [SerializeField] private GameObject _shieldRegenPanel;
    [SerializeField] private GameObject _fixWarpCorePanel;
    [SerializeField] private GameObject _harvesterPanel;
    [SerializeField] private GameObject _busterTimePanel;
    [SerializeField] private GameObject _busterDropChancePanel;


    [Header("Values")]
    [SerializeField] private TextMeshProUGUI _damageValue;
    [SerializeField] private TextMeshProUGUI _blasterCountValue;
    [SerializeField] private TextMeshProUGUI _cooldownValue;
    [SerializeField] private TextMeshProUGUI _negateKillValue;
    [SerializeField] private TextMeshProUGUI _engineSpeedValue;
    [SerializeField] private TextMeshProUGUI _engineTurnValue;
    [SerializeField] private TextMeshProUGUI _hullDurabilityValue;
    [SerializeField] private TextMeshProUGUI _hullRegenValue;
    [SerializeField] private TextMeshProUGUI _shieldCapacityValue;
    [SerializeField] private TextMeshProUGUI _shieldRegenValue;
    [SerializeField] private TextMeshProUGUI _fixWarpCoreValue;
    [SerializeField] private TextMeshProUGUI _harvesterValue;
    [SerializeField] private TextMeshProUGUI _busterTimeValue;
    [SerializeField] private TextMeshProUGUI _busterDropChanceValue;

    [Header("Damage Cost Values")]
    [SerializeField] private TextMeshProUGUI _damageScrapCost;
    [SerializeField] private TextMeshProUGUI _damageECellsCost;
    [SerializeField] private TextMeshProUGUI _damageWCoilsCost;
    [SerializeField] private TextMeshProUGUI _damagePAccelsCost;
    [SerializeField] private TextMeshProUGUI _damageCAlloysCost;

    [Header("Blaster Count Cost Values")]
    [SerializeField] private TextMeshProUGUI _blasterCountScrapCost;
    [SerializeField] private TextMeshProUGUI _blasterCountECellsCost;
    [SerializeField] private TextMeshProUGUI _blasterCountWCoilsCost;
    [SerializeField] private TextMeshProUGUI _blasterCountPAccelsCost;
    [SerializeField] private TextMeshProUGUI _blasterCountCAlloysCost;

    [Header("Cooldown Cost Values")]
    [SerializeField] private TextMeshProUGUI _cooldownScrapCost;
    [SerializeField] private TextMeshProUGUI _cooldownECellsCost;
    [SerializeField] private TextMeshProUGUI _cooldownWCoilsCost;
    [SerializeField] private TextMeshProUGUI _cooldownPAccelsCost;
    [SerializeField] private TextMeshProUGUI _cooldownCAlloysCost;

    [Header("negateKill Cost Values")]
    [SerializeField] private TextMeshProUGUI _negateKillScrapCost;
    [SerializeField] private TextMeshProUGUI _negateKillECellsCost;
    [SerializeField] private TextMeshProUGUI _negateKillWCoilsCost;
    [SerializeField] private TextMeshProUGUI _negateKillPAccelsCost;
    [SerializeField] private TextMeshProUGUI _negateKillCAlloysCost;

    [Header("Engine Speed Cost Values")]
    [SerializeField] private TextMeshProUGUI _engineSpeedScrapCost;
    [SerializeField] private TextMeshProUGUI _engineSpeedECellsCost;
    [SerializeField] private TextMeshProUGUI _engineSpeedWCoilsCost;
    [SerializeField] private TextMeshProUGUI _engineSpeedPAccelsCost;
    [SerializeField] private TextMeshProUGUI _engineSpeedCAlloysCost;

    [Header("engineTurn Cost Values")]
    [SerializeField] private TextMeshProUGUI _engineTurnScrapCost;
    [SerializeField] private TextMeshProUGUI _engineTurnECellsCost;
    [SerializeField] private TextMeshProUGUI _engineTurnWCoilsCost;
    [SerializeField] private TextMeshProUGUI _engineTurnPAccelsCost;
    [SerializeField] private TextMeshProUGUI _engineTurnCAlloysCost;

    [Header("hullDurability Cost Values")]
    [SerializeField] private TextMeshProUGUI _hullDurabilityScrapCost;
    [SerializeField] private TextMeshProUGUI _hullDurabilityECellsCost;
    [SerializeField] private TextMeshProUGUI _hullDurabilityWCoilsCost;
    [SerializeField] private TextMeshProUGUI _hullDurabilityPAccelsCost;
    [SerializeField] private TextMeshProUGUI _hullDurabilityCAlloysCost;

    [Header("hullRegen Cost Values")]
    [SerializeField] private TextMeshProUGUI _hullRegenScrapCost;
    [SerializeField] private TextMeshProUGUI _hullRegenECellsCost;
    [SerializeField] private TextMeshProUGUI _hullRegenWCoilsCost;
    [SerializeField] private TextMeshProUGUI _hullRegenPAccelsCost;
    [SerializeField] private TextMeshProUGUI _hullRegenCAlloysCost;

    [Header("shieldCapacity Cost Values")]
    [SerializeField] private TextMeshProUGUI _shieldCapacityScrapCost;
    [SerializeField] private TextMeshProUGUI _shieldCapacityECellsCost;
    [SerializeField] private TextMeshProUGUI _shieldCapacityWCoilsCost;
    [SerializeField] private TextMeshProUGUI _shieldCapacityPAccelsCost;
    [SerializeField] private TextMeshProUGUI _shieldCapacityCAlloysCost;

    [Header("shieldRegen Cost Values")]
    [SerializeField] private TextMeshProUGUI _shieldRegenScrapCost;
    [SerializeField] private TextMeshProUGUI _shieldRegenECellsCost;
    [SerializeField] private TextMeshProUGUI _shieldRegenWCoilsCost;
    [SerializeField] private TextMeshProUGUI _shieldRegenPAccelsCost;
    [SerializeField] private TextMeshProUGUI _shieldRegenCAlloysCost;

    [Header("WarpCore Cost Values")]
    [SerializeField] private TextMeshProUGUI _warpCoreScrapCost;
    [SerializeField] private TextMeshProUGUI _warpCoreECellsCost;
    [SerializeField] private TextMeshProUGUI _warpCoreWCoilsCost;
    [SerializeField] private TextMeshProUGUI _warpCorePAccelsCost;
    [SerializeField] private TextMeshProUGUI _warpCoreCAlloysCost;

    [Header("harvester Cost Values")]
    [SerializeField] private TextMeshProUGUI _harvesterScrapCost;
    [SerializeField] private TextMeshProUGUI _harvesterECellsCost;
    [SerializeField] private TextMeshProUGUI _harvesterWCoilsCost;
    [SerializeField] private TextMeshProUGUI _harvesterPAccelsCost;
    [SerializeField] private TextMeshProUGUI _harvesterCAlloysCost;

    [Header("busterTime Cost Values")]
    [SerializeField] private TextMeshProUGUI _busterTimeScrapCost;
    [SerializeField] private TextMeshProUGUI _busterTimeECellsCost;
    [SerializeField] private TextMeshProUGUI _busterTimeWCoilsCost;
    [SerializeField] private TextMeshProUGUI _busterTimePAccelsCost;
    [SerializeField] private TextMeshProUGUI _busterTimeCAlloysCost;

    [Header("DropChance Cost Values")]
    [SerializeField] private TextMeshProUGUI _dropChanceScrapCost;
    [SerializeField] private TextMeshProUGUI _dropChanceECellsCost;
    [SerializeField] private TextMeshProUGUI _dropChanceWCoilsCost;
    [SerializeField] private TextMeshProUGUI _dropChancePAccelsCost;
    [SerializeField] private TextMeshProUGUI _dropChanceCAlloysCost;


    //refs
    private IntegrityBehavior _hullIntergityRef;
    private IntegrityBehavior _shieldIntegrityRef;
    private WeaponsSystemController _weaponsControllerRef;
    private MoveObject _enginesMoveRef;
    private AddRotationToObject _enginesRotateRef;
    private WarpCoreBehavior _warpCoreRef;
    private CargoBusterBehavior _busterRef;


    //Monobehaviors


    //Utilites
    private void UpdatePrices()
    {
        if (PlayerObjectManager.Instance.GetPlayerObject() != null)
        { 
            //weapons
            _damageScrapCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[0].ToString();
            _damageECellsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[1].ToString();
            _damageWCoilsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[2].ToString();
            _damagePAccelsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[3].ToString();
            _damageCAlloysCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[4].ToString();

            _blasterCountScrapCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[0].ToString();
            _blasterCountECellsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[1].ToString();
            _blasterCountWCoilsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[2].ToString();
            _blasterCountPAccelsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[3].ToString();
            _blasterCountCAlloysCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[4].ToString();

            _cooldownScrapCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[0].ToString();
            _cooldownECellsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[1].ToString();
            _cooldownWCoilsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[2].ToString();
            _cooldownPAccelsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[3].ToString();
            _cooldownCAlloysCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[4].ToString();

            _negateKillScrapCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[0].ToString();
            _negateKillECellsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[1].ToString();
            _negateKillWCoilsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[2].ToString();
            _negateKillPAccelsCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[3].ToString();
            _negateKillCAlloysCost.text = UpgradeManager.Instance.GetWeaponsUpgradePrice()[4].ToString();

            //engines
            _engineSpeedScrapCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[0].ToString();
            _engineSpeedECellsCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[1].ToString();
            _engineSpeedWCoilsCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[2].ToString();
            _engineSpeedPAccelsCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[3].ToString();
            _engineSpeedCAlloysCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[4].ToString();

            _engineTurnScrapCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[0].ToString();
            _engineTurnECellsCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[1].ToString();
            _engineTurnWCoilsCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[2].ToString();
            _engineTurnPAccelsCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[3].ToString();
            _engineTurnCAlloysCost.text = UpgradeManager.Instance.GetEnginesUpgradePrice()[4].ToString();

            //hull
            _hullDurabilityScrapCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[0].ToString();
            _hullDurabilityECellsCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[1].ToString();
            _hullDurabilityWCoilsCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[2].ToString();
            _hullDurabilityPAccelsCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[3].ToString();
            _hullDurabilityCAlloysCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[4].ToString();

            _hullRegenScrapCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[0].ToString();
            _hullRegenECellsCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[1].ToString();
            _hullRegenWCoilsCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[2].ToString();
            _hullRegenPAccelsCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[3].ToString();
            _hullRegenCAlloysCost.text = UpgradeManager.Instance.GetHullUpgradePrice()[4].ToString();

            //shields
            _shieldCapacityScrapCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[0].ToString();
            _shieldCapacityECellsCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[1].ToString();
            _shieldCapacityWCoilsCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[2].ToString();
            _shieldCapacityPAccelsCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[3].ToString();
            _shieldCapacityCAlloysCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[4].ToString();

            _shieldRegenScrapCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[0].ToString();
            _shieldRegenECellsCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[1].ToString();
            _shieldRegenWCoilsCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[2].ToString();
            _shieldRegenPAccelsCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[3].ToString();
            _shieldRegenCAlloysCost.text = UpgradeManager.Instance.GetShieldsUpgradePrice()[4].ToString();

            //buster
            _busterTimeScrapCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[0].ToString();
            _busterTimeECellsCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[1].ToString();
            _busterTimeWCoilsCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[2].ToString();
            _busterTimePAccelsCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[3].ToString();
            _busterTimeCAlloysCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[4].ToString();

            _dropChanceScrapCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[0].ToString();
            _dropChanceECellsCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[1].ToString();
            _dropChanceWCoilsCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[2].ToString();
            _dropChancePAccelsCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[3].ToString();
            _dropChanceCAlloysCost.text = UpgradeManager.Instance.GetBusterUpgradePrice()[4].ToString();

            //harvester
            _harvesterScrapCost.text = UpgradeManager.Instance.GetScrapHarvesterUpgradePrice()[0].ToString();
            _harvesterECellsCost.text = UpgradeManager.Instance.GetScrapHarvesterUpgradePrice()[1].ToString();
            _harvesterWCoilsCost.text = UpgradeManager.Instance.GetScrapHarvesterUpgradePrice()[2].ToString();
            _harvesterPAccelsCost.text = UpgradeManager.Instance.GetScrapHarvesterUpgradePrice()[3].ToString();
            _harvesterCAlloysCost.text = UpgradeManager.Instance.GetScrapHarvesterUpgradePrice()[4].ToString();

            //warp core
            _warpCoreScrapCost.text = UpgradeManager.Instance.GetWarpCoreUpgradePrice()[0].ToString();
            _warpCoreECellsCost.text = UpgradeManager.Instance.GetWarpCoreUpgradePrice()[1].ToString();
            _warpCoreWCoilsCost.text = UpgradeManager.Instance.GetWarpCoreUpgradePrice()[2].ToString();
            _warpCorePAccelsCost.text = UpgradeManager.Instance.GetWarpCoreUpgradePrice()[3].ToString();
            _warpCoreCAlloysCost.text = UpgradeManager.Instance.GetWarpCoreUpgradePrice()[4].ToString();
        }
    }

    private int CountEnabledBlasters()
    {
        int enabledBlasters = 0;
        SpawnLaserOnCommand[] blastersCollection = _weaponsControllerRef.gameObject.GetComponentsInChildren<SpawnLaserOnCommand>();
        foreach (SpawnLaserOnCommand Blaster in blastersCollection)
        {
            if (Blaster.IsBlasterEnabled())
                enabledBlasters++;
        }

        return enabledBlasters;
    }

    //externals
    public void  InitializeReferences()
    {
        if (PlayerObjectManager.Instance.GetPlayerObject() != null)
        {
            _hullIntergityRef = PlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetHullObject().GetComponent<IntegrityBehavior>();
            _shieldIntegrityRef = PlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetShieldsObject().GetComponent<IntegrityBehavior>();
            _weaponsControllerRef = PlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetWeaponsObject().GetComponent<WeaponsSystemController>();
            _enginesMoveRef = PlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetEnginesObject().GetComponent<MoveObject>();
            _enginesRotateRef = PlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetEnginesObject().GetComponent<AddRotationToObject>();
            _warpCoreRef = PlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetWarpCoreObject().GetComponent<WarpCoreBehavior>();
            _busterRef = PlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetCargoBuster();

            UpdateDescriptionValuesAndCosts();
            
        }
    }

    public void UpdateDescriptionValuesAndCosts()
    {
        if (PlayerObjectManager.Instance.GetPlayerObject() != null)
        {
            _damageValue.text = _weaponsControllerRef.gameObject.GetComponentInChildren<SpawnLaserOnCommand>().GetDamage().ToString();
            _cooldownValue.text = _weaponsControllerRef.gameObject.GetComponentInChildren<SpawnLaserOnCommand>().GetCooldown().ToString();
            _blasterCountValue.text = CountEnabledBlasters().ToString();
            _negateKillValue.text = KillNegater.Instance.GetKilNegationChance().ToString();

            _engineSpeedValue.text = _enginesMoveRef.GetSpeed().ToString();
            _engineTurnValue.text = _enginesRotateRef.GetRotationSpeed().ToString();

            _hullDurabilityValue.text = _hullIntergityRef.GetMaxIntegrity().ToString();

            if (_hullIntergityRef.gameObject.GetComponent<HullSystemController>().IsRegenUnlocked())
                _hullRegenValue.text = _hullIntergityRef.GetComponent<Regenerator>().GetTickDuration().ToString();
            else _hullRegenValue.text = "Offline";

            _shieldCapacityValue.text = _shieldIntegrityRef.GetMaxIntegrity().ToString();
            _shieldRegenValue.text = _shieldIntegrityRef.GetComponent<Regenerator>().GetTickDuration().ToString();

            _busterTimeValue.text = _busterRef.GetBusterDuration().ToString();
            _busterDropChanceValue.text = CargoLootDropper.Instance.GetDropChance().ToString();

            if (_warpCoreRef.GetComponent<WarpCoreSystemController>().IsWarpCoreRepaired())
                _fixWarpCoreValue.text = "Online";
            else _fixWarpCoreValue.text = "Offline";

            if (ScrapHarvester.Instance.IsHarvesterEnabled())
                _harvesterValue.text = "Online";
            else _harvesterValue.text = "Offline";

            UpdatePrices();
        }
    }

    public void ShowDamagePanel()
    {
        _damagePanel.SetActive(true);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowBlasterCountPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(true);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowCooldownPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(true);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowNegateKillPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(true);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowEngineSpeedPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(true);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowEngineTurnPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(true);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowHullDurabilityPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(true);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowHullRegenPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(true);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowShieldCapacityPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(true);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowShieldRegenPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(true);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowBusterTimePanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(true);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowDropChancePanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(true);
    }

    public void ShowWarpCorePanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(true);
        _harvesterPanel.SetActive(false);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }

    public void ShowHarvesterPanel()
    {
        _damagePanel.SetActive(false);
        _blasterPanel.SetActive(false);
        _cooldownPanel.SetActive(false);
        _negateKillPanel.SetActive(false);
        _engineSpeedPanel.SetActive(false);
        _engineTurnPanel.SetActive(false);
        _hullDurabilityPanel.SetActive(false);
        _hullRegenPanel.SetActive(false);
        _shieldCapacityPanel.SetActive(false);
        _shieldRegenPanel.SetActive(false);
        _fixWarpCorePanel.SetActive(false);
        _harvesterPanel.SetActive(true);
        _busterTimePanel.SetActive(false);
        _busterDropChancePanel.SetActive(false);
    }
}
