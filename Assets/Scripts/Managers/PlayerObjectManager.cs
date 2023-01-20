using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;
using UnityEngine.Events;

public class PlayerObjectManager : MonoSingleton<PlayerObjectManager>
{
    //Declarations
    [SerializeField] private GameObject _playerShipPreference;
    private GameObject _playerObjectRef;
    [SerializeField] private bool _isPlayerAlive = false;

    [Header("Events")]
    public UnityEvent OnPlayerSpawned;
    public UnityEvent OnPlayerDeath;


    //Monos
    //...


    //MonoSingletons
    //...


    //Utilities
    public void SpawnPlayer(Transform position)
    {
        if (_isPlayerAlive == false)
        {
            _playerObjectRef = Instantiate(_playerShipPreference, position.position, Quaternion.identity, ContainersManager.Instance.GetShipsContainer().transform);
            _playerObjectRef.GetComponent<ShipInformation>().SetPlayerFlag(true);
            _isPlayerAlive = true;
            OnPlayerSpawned?.Invoke();
        }
            
    }

    public void ReportPlayerDeath()
    {
        _isPlayerAlive = false;
        OnPlayerDeath?.Invoke();
    }

    public void SetPlayerObject(GameObject newPlayerObject)
    {
        _playerObjectRef = newPlayerObject;
    }

    public GameObject GetPlayerObject()
    {
        return _playerObjectRef;
    }

}
