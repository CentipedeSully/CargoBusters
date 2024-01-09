using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class ShipFactory : MonoBehaviour
{
    //Declarations
    [Header("Settings")]
    [SerializeField] private List<GameObject> _shipPrefabs;
    private FactionRelationshipManager _factionManagerRef;

    [Header("========== Debug Utilities ==========")]
    [SerializeField] private bool _isDebugActive = false;

    [Header ("Testing Values")]
    [SerializeField] private string _prefabNameDebug;
    [SerializeField] private Vector3 _prefabSpawnPositionDebug;
    [SerializeField] private float _prefabRotationDebug;
    [SerializeField] private bool _spawnPrefabAsPlayerDebug;
    [SerializeField] private string _shipNameDebug;
    [SerializeField] private string _factionNameDebug;

    [Header("Commands")]
    [SerializeField] private bool _spawnShipCmd = false;
    [SerializeField] private bool _checkPrefabExistenceCmd = false;



    //Monobehaviours
    private void Update()
    {
        if (_isDebugActive)
            ListenForDebugCommands();
    }



    //Internal Utils
    private GameObject GetPrefab(string prefabName)
    {
        if (_shipPrefabs == null)
            return null;

        foreach (GameObject prefab in _shipPrefabs)
        {
            if (prefab.name == prefabName)
                return prefab;
        }

        return null;
    }




    //Getters, Setters, & Commands
    public AbstractShip SpawnShip(string prefabName, Vector3 position, float rotation, string shipName, string faction, bool isPlayer)
    {
        GameObject shipPrefab = GetPrefab(prefabName);
        if (shipPrefab != null)
        {
            Vector3 zRotation = new Vector3(0, 0, rotation);
            Transform containerTransform = GameManager.Instance.GetShipContainer();

            //Spawn new ship within the ship container
            AbstractShip newShip = Instantiate(shipPrefab, position, Quaternion.Euler(zRotation), containerTransform).GetComponent<AbstractShip>();

            //Setup Ship Info
            newShip.SetName(shipName);

            if (_factionManagerRef == null)
                _factionManagerRef = GameManager.Instance.GetFactionRelationshipManager();

            if (_factionManagerRef.DoesFactionExist(faction) == false)
                _factionManagerRef.AddFaction(faction);
            
            newShip.SetFaction(faction);

            if (isPlayer && GameManager.Instance.GetPlayerManager().DoesPlayerShipExist() == false)
            {
                newShip.MakeShipPlayerControlled();
                GameManager.Instance.GetCameraController().SetCameraFocusToNewFollowObject(newShip.gameObject);
            }
                
            else
                newShip.MakeShipAiControlled();

            return newShip;
        }

        LogWarning($"Prefab '{prefabName}' doesnt exist in the Ship Factory. returning null");
        return null;

    }





    //Debugging Utils
    private void ListenForDebugCommands()
    {
        if (_spawnShipCmd == true)
        {
            _spawnShipCmd = false;
            SpawnShip(_prefabNameDebug, _prefabSpawnPositionDebug, _prefabRotationDebug, _shipNameDebug, _factionNameDebug, _spawnPrefabAsPlayerDebug);
        }

        if (_checkPrefabExistenceCmd == true)
        {
            _checkPrefabExistenceCmd = false;
            LogStatement($"Does prefab '{_prefabNameDebug}' exist: {GetPrefab(_prefabNameDebug) != null}");
        }
    }

    private void LogStatement(string statement)
    {
        STKDebugLogger.LogStatement(_isDebugActive,statement);
    }

    private void LogWarning(string warning)
    {
        STKDebugLogger.LogWarning(warning);
    }

    private void LogError(string error)
    {
        STKDebugLogger.LogError(error);
    }



}
