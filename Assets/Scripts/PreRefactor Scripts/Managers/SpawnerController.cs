using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerController : MonoBehaviour
{
    //Declarations
    [SerializeField] private int _maxEnemiesOnField = 3;
    [SerializeField] private int _minEnemiesOnField = 1;
    [SerializeField] private SpawnManager _spawnManagerReference;
    [SerializeField] private float _timeBetweenWaves = 5;

    [SerializeField] private int _wavesCompleted = 0;
    [SerializeField] private int _goalWavesCompleted = 5;


    //Monbehaviors






    //Utilities
    public void SetMaxEnemies(int value)
    {
        if (value > 0)
            _maxEnemiesOnField = value;
    }

    public int GetMaxEnemies()
    {
        return _maxEnemiesOnField;
    }
    
    private IEnumerator DelaySpawningForTime()
    {
        yield return new WaitForSeconds(_timeBetweenWaves);
        _spawnManagerReference.StartSpawning();
    }

    public void CheckEnemyInstanceCount(int amountOfEnemies)
    {
        if (amountOfEnemies >= _maxEnemiesOnField)
        {
            _spawnManagerReference.StopSpawning();
        }
            
        else if (amountOfEnemies < _minEnemiesOnField)
        {
            _maxEnemiesOnField += 1;
            _wavesCompleted += 1;

            if (_wavesCompleted == _goalWavesCompleted)
            {
                Debug.Log("Game Won!");
            }
            else StartCoroutine(DelaySpawningForTime());
        }
            


    }

}
