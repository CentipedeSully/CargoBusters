using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;
using UnityEngine.Events;


public class SpawnController : MonoSingleton<SpawnController>
{
    //Declaration
    [SerializeField] private GameObject _enemyDotSensorPrefab;
    [SerializeField] private GameObject _statusVisualizerPrefab;
    [SerializeField] private Transform _visualizerContainer;
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
    private bool _skipIntermission = false;

    [SerializeField] private int _extraEnemyModifier = 2;
    [SerializeField] private int _totalWavesCompleted = 0;
    [SerializeField] private int _wavesCompletedThisRound = 0;
    [SerializeField] private int _roundsCompleted = 0;
    [SerializeField] private int _wavesPerRound = 5;
    [SerializeField] private float _spawnIntervalDelay = 1;
    private WaitForSeconds _cachedSpawnIntervalWaitForSeconds;
    private WaitForSeconds _cachedIntermissionWaitForSeconds;
    private WaitForSeconds _cachedShipSpawnAnimDelayWait;

    [Header("Events")]
    public UnityEvent OnWaveStarted;
    public UnityEvent OnWaveEnded;
    public UnityEvent OnRoundCompleted;
    public UnityEvent OnIntermissionStarted;
    public UnityEvent OnIntermissionEnded;


    //Monos
    private void Start()
    {
        _cachedSpawnIntervalWaitForSeconds = new WaitForSeconds(_spawnIntervalDelay);
        _cachedIntermissionWaitForSeconds = new WaitForSeconds(1);
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
                

                //Spawn enemy at random position and dot sensor on player, also set up enemy strength based on rounds completed
                GameObject enemyShip = Instantiate(randomEnemyPrefab, randomSpawnPosition.position, Quaternion.identity, ContainersManager.Instance.GetShipsContainer().transform);
                enemyShip.GetComponent<ShipSystemReferencer>().GetAiBehaviorObject().GetComponent<RandomizeAttributesOnEnable>().SetStrength(_roundsCompleted);
                enemyShip.GetComponent<ShipSystemReferencer>().GetAiBehaviorObject().GetComponent<RandomizeAttributesOnEnable>().RandomizeAttributes();
                SetupPointerToEnemy(enemyShip);
                SetupEnemyTargetingAI(enemyShip);
                SetupStatusVisualizerToEnemy(enemyShip);


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
            _totalWavesCompleted++;
            _wavesCompletedThisRound++;

            //if round completed, reset current wave count and wave intensity, and then increase round count
            if (_wavesCompletedThisRound == _wavesPerRound)
            {
                _roundsCompleted++;
                _wavesCompletedThisRound = 0;
                _maxEnemyCountThisWave = 1;
                //Increase Enemy Value
                OnRoundCompleted?.Invoke();
            }
            else _maxEnemyCountThisWave += _extraEnemyModifier + _totalWavesCompleted;

            //Reset Utilities for next wave
            _enemiesSpawned = 0;
            _enemiesDefeated = 0;

            OldUiManager.Instance.GetWavesCompletedText().text = _totalWavesCompleted.ToString();
            OnWaveEnded?.Invoke();
            LogWaveEnded();


            //Enter Intermission and Wait...
            OnIntermissionStarted?.Invoke();
            OldUiManager.Instance.GetIntermissionTimerDisplay().ShowDisplay();
            int timePassed = 0;
            while(timePassed <= _intermissionDuration && _skipIntermission == false) 
            {
                OldUiManager.Instance.GetIntermissionTimerText().text = ((int)_intermissionDuration - timePassed).ToString();
                yield return _cachedIntermissionWaitForSeconds;
                timePassed++;
            }
            OnIntermissionEnded?.Invoke();
            OldUiManager.Instance.GetIntermissionTimerDisplay().HideDisplay();
            OldUiManager.Instance.GetIntermissionTimerText().text = "00:00";

            _skipIntermission = false;



        }
    }

    private void SetupStatusVisualizerToEnemy(GameObject enemyShip)
    {
        GameObject statusVisualizer = Instantiate(_statusVisualizerPrefab, enemyShip.transform.position, Quaternion.identity, ContainersManager.Instance.GetVisualizerContainer().transform);
        statusVisualizer.GetComponent<StatusVisualController>().SetTarget(enemyShip);
        statusVisualizer.GetComponent<StatusVisualController>().SetPlayerReference();
    }

    private void SetupPointerToEnemy(GameObject enemy)
    {
        GameObject enemyDotObj = Instantiate(_enemyDotSensorPrefab, OldPlayerObjectManager.Instance.GetPlayerObject().GetComponent<ShipSystemReferencer>().GetSensorObject().transform);
        enemyDotObj.GetComponent<PointToEnemy>().SetEnemyTarget(enemy);
        enemyDotObj.GetComponent<PointToEnemy>().EnablePointer();
    }

    private void SetupEnemyTargetingAI(GameObject enemy)
    {
        AiController enemyAiController = enemy.GetComponent<ShipSystemReferencer>().GetAiBehaviorObject().GetComponent<AiController>();
        enemyAiController.EnterPursuit(OldPlayerObjectManager.Instance.GetPlayerObject());
    }

    //External Control Utils
    public void StartGameSpawning()
    {
        _isGameActive = true;
        StartCoroutine(SpawnEnemies());
    }

    public void ReportEnemyDeath()
    {
        _enemiesDefeated++;
        _totalEnemiesDefeated++;
        OldUiManager.Instance.GetShipsDestroyedText().text = _totalEnemiesDefeated.ToString();
        LogEnemyReportedDefeated();
    }

    public void InterruptGame()
    {
        StopAllCoroutines();
        _isGameActive = false;
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
        return _totalWavesCompleted;
    }

    public bool IsSpawning()
    {
        return _isSpawning;
    }

    public void SkipIntermission()
    {
        _skipIntermission = true;
    }

    public bool IsWaveInProgress()
    {
        return _isWaveInProgress;
    }

    public int GetRoundCount()
    {
        return _roundsCompleted;
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
            Debug.Log("Wave Ended. Waves Completed: " + _totalWavesCompleted);
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
