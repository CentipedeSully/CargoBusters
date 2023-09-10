using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;
using UnityEngine.Events;
using Cinemachine;

public class PlayerObjectManager : MonoSingleton<PlayerObjectManager>
{
    //Declarations
    [SerializeField] private GameObject _playerShipPreference;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCameraObject;
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
    public void SpawnPlayer()
    {
        if (_isPlayerAlive == false)
        {
            _playerObjectRef = Instantiate(_playerShipPreference, Vector3.zero, Quaternion.identity, ContainersManager.Instance.GetShipsContainer().transform);
            _playerObjectRef.GetComponent<ShipInformation>().SetPlayerFlag(true);
            _playerObjectRef.GetComponent<ShipSystemReferencer>().GetShieldsObject().GetComponent<ShieldUiCommunicator>().SetupIsplayer();
            _playerObjectRef.GetComponent<ShipSystemReferencer>().GetHullObject().GetComponent<HealthUiCommunicator>().SetupIsplayer();
            _playerObjectRef.GetComponent<ShipSystemReferencer>().GetCargoBuster().GetComponent<CargoBusterUiController>().SetupIsplayer();
            _playerObjectRef.GetComponent<ShipSystemReferencer>().GetWarpCoreObject().GetComponent<WarpUiCommunicator>().SetupIsplayer();
            _isPlayerAlive = true;
            _cinemachineVirtualCameraObject.Follow = _playerObjectRef.transform;
            GetComponent<SimulateStarMovementFromPlayerMovement>().SetPlayerObject(_playerObjectRef);
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

    public bool IsPlayerAlive()
    {
        return _isPlayerAlive;
    }

}
