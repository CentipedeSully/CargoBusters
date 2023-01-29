using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAttributesOnEnable : MonoBehaviour
{
    //Declarations
    [Header("Weapon Systems")]
    [SerializeField] private int _laserDamageMin = 1;
    [SerializeField] private int _laserDamageMax = 1;
    [SerializeField] private float _shotCooldownMin = .3f;
    [SerializeField] private float _shotCooldownMax = .75f;
    [SerializeField] private int _blasterCountMin = 1;
    [SerializeField] private int _blasterCountMax = 1;

    [Header("Engine Systems")]
    [SerializeField] private float _minMoveSpeed = 300;
    [SerializeField] private float _maxMoveSpeed = 600;
    [SerializeField] private float _minRotationSpeed = 50;
    [SerializeField] private float _maxRotationSpeed = 100;

    [Header("Durability")]
    [SerializeField] private int _hullMin = 3;
    [SerializeField] private int _hullMax = 3;
    [SerializeField] private int _shieldMin = 0;
    [SerializeField] private int _shieldMax = 2;

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




}
