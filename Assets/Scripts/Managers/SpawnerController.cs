using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerController : MonoBehaviour
{

    [SerializeField] private int _maxEnemiesOnField = 3;
    public UnityEvent OnPlayerDeath;





    public void SetMaxEnemies(int value)
    {
        if (value > 0)
            _maxEnemiesOnField = value;
    }

    

}
