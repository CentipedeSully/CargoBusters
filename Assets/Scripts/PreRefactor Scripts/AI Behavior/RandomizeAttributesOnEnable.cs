using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAttributesOnEnable : MonoBehaviour
{
    //Declarations
    [Header("Weapon Systems")]
    [SerializeField] private int _laserDamageMin = 1;
    [SerializeField] private int _laserDamageMax = 1;
    [SerializeField] private int _laserUpgradeMax = 1;
    [SerializeField] private int _laserUpgradeModifierMin = 0;
    [SerializeField] private int _laserUpgradeModifierMax = 1;

    [SerializeField] private float _shotCooldownMin = .5f;
    [SerializeField] private float _shotCooldownMax = .75f;
    [SerializeField] private int _cooldownMaxUpgrades = 2;
    [SerializeField] private float _cooldownUpgradeModifierMin = .15f;
    [SerializeField] private float _cooldownUpgradeModifierMax = .15f;

    [SerializeField] private int _blasterCountMin = 1;
    [SerializeField] private int _blasterCountMax = 1;
    [SerializeField] private int _blasterUpgradesMax = 1;
    [SerializeField] private int _blasterUpgradeModifierMin = 0;
    [SerializeField] private int _blasterUpgradeModifierMax = 1;


    [Header("Engine Systems")]
    [SerializeField] private float _minMoveSpeed = 300;
    [SerializeField] private float _maxMoveSpeed = 600;
    [SerializeField] private int _moveSpeedUpgradesMax = 4;
    [SerializeField] private float _moveModifierMin = 300;
    [SerializeField] private float _moveModifierMax = 300;

    [SerializeField] private float _minRotationSpeed = 50;
    [SerializeField] private float _maxRotationSpeed = 100;
    [SerializeField] private int _rotationUpgradesMax = 2;
    [SerializeField] private float _rotationModifierMin = 20;
    [SerializeField] private float _rotationModifierMax = 20;

    [Header("Durability")]
    [SerializeField] private int _hullMin = 3;
    [SerializeField] private int _hullMax = 3;
    [SerializeField] private int _hullUpgradesMax = 2;
    [SerializeField] private int _hullModifierMin = 0;
    [SerializeField] private int _hullModifierMax = 1;

    [SerializeField] private int _shieldMin = 0;
    [SerializeField] private int _shieldMax = 2;
    [SerializeField] private int _shieldUpgradesMax = 3;
    [SerializeField] private int _shieldsModifierMin = 0;
    [SerializeField] private int _shieldsModifierMax = 1;

    //ref
    [SerializeField] private ShipSystemReferencer _systemReferencer;
    [SerializeField] private GameObject _weaponsSystem;
    [SerializeField] private GameObject _enginesSystem;
    [SerializeField] private GameObject _hullObject;
    [SerializeField] private GameObject _shieldsObject;


    //Monos
    private void Awake()
    {
        InitializeReferences();
    }


    //Utilites
    private void InitializeReferences()
    {
        _weaponsSystem = _systemReferencer.GetWeaponsObject();
        _enginesSystem = _systemReferencer.GetEnginesObject();
        _hullObject = _systemReferencer.GetHullObject();
        _shieldsObject = _systemReferencer.GetShieldsObject();
    }

    private void RandomizeWeaponsAttributes()
    {
        int randomBlasterCount = Random.Range(_blasterCountMin, _blasterCountMax);
        int currentCount = 0;
        SpawnLaserOnCommand[] laserSpawners = _weaponsSystem.GetComponentsInChildren<SpawnLaserOnCommand>();

        while (currentCount < randomBlasterCount)
        {
            if (randomBlasterCount < laserSpawners.Length)
            {
                laserSpawners[currentCount].EnableBlaster();
                laserSpawners[currentCount].SetCooldown(Random.Range(_shotCooldownMin, _shotCooldownMax));
                laserSpawners[currentCount].SetDamage(Random.Range(_laserDamageMin, _laserDamageMax + 1));
            }

            currentCount++;
        }
    }

    public void RandomizeAttributes()
    {
        if (_systemReferencer.gameObject.GetComponent<ShipInformation>().IsPlayer() == false)
        {
            RandomizeWeaponsAttributes();

            //Engines
            _enginesSystem.GetComponent<MoveObject>().SetSpeed(Random.Range(_minMoveSpeed, _maxMoveSpeed));
            _enginesSystem.GetComponent<AddRotationToObject>().SetRotationSpeed(Random.Range(_minRotationSpeed, _maxRotationSpeed));

            //Hull
            int randomHull = Random.Range(_hullMin, _hullMax + 1);
            _hullObject.GetComponent<IntegrityBehavior>().SetMaxIntegrity(randomHull);
            _hullObject.GetComponent<IntegrityBehavior>().FillIntegrity();

            float critThreshold = 0;
            critThreshold = (((float)randomHull - ((float)randomHull - 2)) / (float)randomHull) + .01f;
            Debug.Log(critThreshold);

            _hullObject.GetComponent<IntegrityThresholdEvaluator>().SetCriticalThreshold(critThreshold);

            //Shields
            _shieldsObject.GetComponent<IntegrityBehavior>().SetMaxIntegrity(Random.Range(_shieldMin, _shieldMax + 1));
        }
    }

    public void SetStrength(int strengthLvl = 0)
    {
        if (strengthLvl >= 0)
        {
            //Upgrade Laser min/max range by the strength modifier
            if (strengthLvl <= _laserUpgradeMax)
            {
                _laserDamageMin += _laserUpgradeModifierMin * strengthLvl;
                _laserDamageMax += _laserUpgradeModifierMax * strengthLvl;
            }
            else
            {
                _laserDamageMin += _laserUpgradeModifierMin * _laserUpgradeMax;
                _laserDamageMax += _laserUpgradeModifierMax * _laserUpgradeMax;
            }


            //Upgrade cooldown min/max range by the strength modifier
            if (strengthLvl <= _cooldownMaxUpgrades)
            {
                _shotCooldownMin -= _cooldownUpgradeModifierMin * strengthLvl;
                _shotCooldownMax -= _cooldownUpgradeModifierMax * strengthLvl;
            }
            else
            {
                _shotCooldownMin -= _cooldownUpgradeModifierMin * _cooldownMaxUpgrades;
                _shotCooldownMax -= _cooldownUpgradeModifierMax * _cooldownMaxUpgrades;
            }


            //Upgrade blaster min/max range by the strength modifier
            if (strengthLvl <= _blasterUpgradesMax)
            {
                _blasterCountMin += _blasterUpgradeModifierMin * strengthLvl;
                _blasterCountMax += _blasterUpgradeModifierMax * strengthLvl;
            }
            else
            {
                _blasterCountMin += _blasterUpgradeModifierMin * _blasterUpgradesMax;
                _blasterCountMax += _blasterUpgradeModifierMax * _blasterUpgradesMax;
            }


            //Upgrade speed min/max range by the strength modifier
            if (strengthLvl <= _moveSpeedUpgradesMax)
            {
                _minMoveSpeed += _moveModifierMin * strengthLvl;
                _maxMoveSpeed += _moveModifierMax * strengthLvl;
            }
            else
            {
                _minMoveSpeed += _moveModifierMin * _moveSpeedUpgradesMax;
                _maxMoveSpeed += _moveModifierMax * _moveSpeedUpgradesMax;
            }


            //Upgrade turn min/max range by the strength modifier
            if (strengthLvl <= _rotationUpgradesMax)
            {
                _minRotationSpeed += _rotationModifierMin * strengthLvl;
                _maxRotationSpeed += _rotationModifierMax * strengthLvl;
            }
            else
            {
                _minRotationSpeed += _rotationModifierMin * _rotationUpgradesMax;
                _maxRotationSpeed += _rotationModifierMax * _rotationUpgradesMax;
            }


            //Upgrade hull min/max range by the strength modifier
            if (strengthLvl <= _hullUpgradesMax)
            {
                _hullMin += _hullModifierMin * strengthLvl;
                _hullMax += _hullModifierMax * strengthLvl;
            }
            else
            {
                _hullMin += _hullModifierMin * _hullUpgradesMax;
                _hullMax += _hullModifierMax * _hullUpgradesMax;
            }


            //Upgrade shields min/max range by the strength modifier
            if (strengthLvl <= _shieldUpgradesMax)
            {
                _shieldMin += _shieldsModifierMin * strengthLvl;
                _shieldMax += _shieldsModifierMax * strengthLvl;
            }
            else
            {
                _shieldMin += _shieldsModifierMin * _shieldUpgradesMax;
                _shieldMax += _shieldsModifierMax * _shieldUpgradesMax;
            }
        }
    }


}
