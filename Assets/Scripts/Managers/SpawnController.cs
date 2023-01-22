using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;
using UnityEngine.Events;


public class SpawnController : MonoSingleton<SpawnController>
{
    //Declaration
    [SerializeField] private bool _isDebugEnabled = true;
    [SerializeField] private List<GameObject> _enemyShipPrefabsList;
    [SerializeField] private List<Transform> _spawnPositionsList;
    [SerializeField] private int _enemiesSpawned = 0;
    [SerializeField] private int _totalEnemiesSpawned = 0;
    [SerializeField] private int _enemiesDefeated = 0;
    [SerializeField] private int _totalEnemiesDefeated = 0;

    [SerializeField] private int _maxEnemyCountThisWave = 1;
    [SerializeField] private bool _isSpawning = false;
    [SerializeField] private bool _isWaveInProgress = false;
    private bool _isGameActive = false;
    [SerializeField] private float _intermissionDuration = 60;

    [SerializeField] private int _extraEnemyModifier = 2;
    [SerializeField] private int _wavesCompletedCount = 0;
    [SerializeField] private float _spawnIntervalDelay = 1;
    private WaitForSeconds _cachedSpawnIntervalWaitForSeconds;
    private WaitForSeconds _cachedIntermissionWaitForSeconds;

    [Header("Events")]
    public UnityEvent OnWaveStarted;
    public UnityEvent OnWaveEnded;
    public UnityEvent OnIntermissionStarted;
    public UnityEvent OnIntermissionEnded;


    //Monos
    private void Awake()
    {
        _cachedSpawnIntervalWaitForSeconds = new WaitForSeconds(_spawnIntervalDelay);
        _cachedIntermissionWaitForSeconds = new WaitForSeconds(_intermissionDuration);
    }


    //Utils
    private IEnumerator SpawnEnemies()
    {
        while (_isGameActive)
        {
            //initialize wave and spawning states
            _isSpawning = true;
            _isWaveInProgress = true;
            OnWaveStarted?.Invoke();
            LogWaveStarted();

            //spawn enemies
            while (_enemiesSpawned < _maxEnemyCountThisWave)
            {
                GameObject randomEnemyPrefab = _enemyShipPrefabsList[Random.Range(0, _enemyShipPrefabsList.Count)];
                Transform randomSpawnPosition = _spawnPositionsList[Random.Range(0, _spawnPositionsList.Count)];

                Instantiate(randomEnemyPrefab, randomSpawnPosition.position, Quaternion.identity, ContainersManager.Instance.GetShipsContainer().transform);
                _enemiesSpawned++;
                _totalEnemiesSpawned++;

                yield return _cachedSpawnIntervalWaitForSeconds;
            }

            _isSpawning = false;

            //wait until the wave is over: when the player defeats all enemies
            while (_enemiesDefeated < _enemiesSpawned)
                yield return 0;

            //update wave utilities
            _isWaveInProgress = false;
            _wavesCompletedCount++;
            OnWaveEnded?.Invoke();
            LogWaveEnded();


            //Enter Intermission and Wait...
            OnIntermissionStarted?.Invoke();
            LogIntermissionEntered();
            yield return _cachedIntermissionWaitForSeconds;
            OnIntermissionEnded?.Invoke();
            LogIntermissionEnded();

            //Reset Utilities for next wave
            _enemiesSpawned = 0;
            _enemiesDefeated = 0;
            _maxEnemyCountThisWave += _extraEnemyModifier + _wavesCompletedCount;


        }
    }


    //External Control Utils
    public void ReportEnemyDeath()
    {
        _enemiesDefeated++;
        _totalEnemiesDefeated++;
        LogEnemyReportedDefeated();
    }

    public void InterruptGame()
    {
        StopAllCoroutines();
        _isSpawning = false;
        _isWaveInProgress = false;
        LogGameInterrupted();
    }

    //Getters and Setters
    public int GetTotalEnemiesDefeated()
    {
        return _totalEnemiesDefeated;
    }

    public int GetWavesCompleted()
    {
        return _wavesCompletedCount;
    }

    public bool IsSpawning()
    {
        return _isSpawning;
    }

    public bool IsWaveInProgress()
    {
        return _isWaveInProgress;
    }
       
    //Debugging
    public void LogGameInterrupted()
    {
        if (_isDebugEnabled)
            Debug.Log("Game Spawner Interrupted.");
    }

    public void LogEnemyReportedDefeated()
    {
        if (_isDebugEnabled)
            Debug.Log("Enemy Defeat Reported. Defeated Enemies this wave: " + _enemiesDefeated);
    }

    public void LogWaveStarted()
    {
        if (_isDebugEnabled)
            Debug.Log("Wave Started. Expected Enemy Population: " + _maxEnemyCountThisWave);
    }

    public void LogWaveEnded()
    {
        if (_isDebugEnabled)
            Debug.Log("Wave Ended. Waves Completed: " + _wavesCompletedCount);
    }

    public void LogIntermissionEntered()
    {
        if (_isDebugEnabled)
            Debug.Log("Entering Intermission");
    }

    public void LogIntermissionEnded()
    {
        if (_isDebugEnabled)
            Debug.Log("Intermission Ended");
    }
}
