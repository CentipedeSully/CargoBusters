using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnableObject : SpawnableObject
{
    [SerializeField] private Transform prefabContainer;

    private void Awake()
    {
        prefabContainer = GameObject.Find("Enemies Container").transform;
    }

    public override GameObject Spawn(SpawnPosition spawnPosition)
    {

        if (spawnPosition != null)
        {
             GameObject newGameObject = Instantiate(gameObject, spawnPosition.transform.position, Quaternion.identity);

            return newGameObject;
        }
            
        else
        {
            if (_showDebug)
                Debug.Log("No Position is available. Spawn Request Ignored");
            return null;
        }
    }

    public Transform GetPrefabContainer()
    {
        return prefabContainer;
    }
}
