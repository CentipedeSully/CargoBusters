using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstanceCounter : MonoBehaviour
{
    //Declarations
    [SerializeField] private int _activeinstanceCount = 0;
    [SerializeField] private GameObject _prefabBeingCounted;
    [SerializeField] private SpawnerController _spawnControllerRef;

    //Monos






    //Utils
    public void IncrementCount()
    {
        _activeinstanceCount += 1;
        ReportNewCount();
    }

    public void DecrementCount()
    {
        _activeinstanceCount -= 1;
        ReportNewCount();
    }

    public int GetInstanceCount()
    {
        return _activeinstanceCount;
    }

    private void ReportNewCount()
    {
        _spawnControllerRef.CheckEnemyInstanceCount(_activeinstanceCount);
    }

}
