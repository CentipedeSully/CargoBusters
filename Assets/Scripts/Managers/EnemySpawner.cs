using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _prefabContainer;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _amountOfPrefabsToSpawnPerRequest = 3;
    [SerializeField] private float _timeBetweenSpawnRequests = 7;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private LayerMask _blockablesLayerMask;





    public void RequestSpawn()
    {

    }

    
}
