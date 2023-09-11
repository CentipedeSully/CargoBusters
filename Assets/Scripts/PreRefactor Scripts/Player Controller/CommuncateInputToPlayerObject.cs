using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class CommuncateInputToPlayerObject : MonoSingleton<CommuncateInputToPlayerObject>
{
    //Declarations
    private GameObject _playerShipObject;
    private bool _isPlayerReferencesInitialized = false;
    private Vector2 _moveDirection = Vector2.zero;
    private float _turnInput = 0;
    private bool _shootInput = false;
    private bool _boostInput = false;
    private bool _warpInput = false;
    private bool _bustCargoInput = false;
    private bool _spawnPlayerInput = false;
    private bool _repairPlayerInput = false;

    //References
    private HullSystemController _playerHullSystemControllerRef;
    private EnginesSystemController _playerEnginesControllerRef;
    private WeaponsSystemController _playerShotCommanderScriptRef;
    private WarpCoreSystemController _playerWarpCoreControllerRef;
    private CargoBusterBehavior _playerCargoBusterRef;


    //monos
    private void Update()
    {
        GetInputsFromInputDetector();
        SpawnPlayerOnInput();
        if (_isPlayerReferencesInitialized)
        {
            ShareMoveInputWithEnginesControllerScript();
            ShareShootInputWithWeaponsControllerScript();
            ShareWarpInputWithWarpControllerScript();
            ShareBusterInputWithBusterBehavior();
            ShareRepairInputWithHullControllerScript();
        }
        
    }


    //Utils
    private void GetInputsFromInputDetector()
    {
        _moveDirection = InputDetector.Instance.GetMoveInput();

        _shootInput = InputDetector.Instance.GetShootInput();

        _boostInput = InputDetector.Instance.GetBoostInput();

        _turnInput = InputDetector.Instance.GetTurnInput();

        _warpInput = InputDetector.Instance.GetWarpInput();

        _bustCargoInput = InputDetector.Instance.GetBustCargoInput();

        _spawnPlayerInput = InputDetector.Instance.GetPlayerSpawnInput();

        _repairPlayerInput = InputDetector.Instance.GetPlayerRepairInput();
    }

    private void SpawnPlayerOnInput()
    {
        if (OldPlayerObjectManager.Instance.IsPlayerAlive() == false && _spawnPlayerInput)
            OldPlayerObjectManager.Instance.SpawnPlayer();
    }

    private void ShareMoveInputWithEnginesControllerScript()
    {
        _playerEnginesControllerRef.SetMoveInput(_moveDirection);
        _playerEnginesControllerRef.SetTurnInput(_turnInput);
    }

    private void ShareShootInputWithWeaponsControllerScript()
    {
        _playerShotCommanderScriptRef.SetShotCommand(_shootInput);
    }

    private void ShareWarpInputWithWarpControllerScript()
    {
        _playerWarpCoreControllerRef.SetWarpCommand(_warpInput);
    }

    private void ShareBusterInputWithBusterBehavior()
    {
        _playerCargoBusterRef.SetBustCommand(_bustCargoInput);
    }

    private void ShareRepairInputWithHullControllerScript()
    {
        _playerHullSystemControllerRef.SetRegenCommand(_repairPlayerInput);
    }

    //External Control Utils
    public void InitializePlayerObjectReferences()
    {
        //Establish Refererences to the player
        _playerShipObject = OldPlayerObjectManager.Instance.GetPlayerObject();
        if (_playerShipObject != null)
            _isPlayerReferencesInitialized = true;

        if (_isPlayerReferencesInitialized)
        {
            _playerEnginesControllerRef = _playerShipObject.GetComponent<ShipSystemReferencer>().GetEnginesObject().GetComponent<EnginesSystemController>();
            _playerShotCommanderScriptRef = _playerShipObject.GetComponent<ShipSystemReferencer>().GetWeaponsObject().GetComponent<WeaponsSystemController>();
            _playerWarpCoreControllerRef = _playerShipObject.GetComponent<ShipSystemReferencer>().GetWarpCoreObject().GetComponent<WarpCoreSystemController>();
            _playerCargoBusterRef = _playerShipObject.GetComponent<ShipSystemReferencer>().GetCargoBuster();
            _playerHullSystemControllerRef = _playerShipObject.GetComponent<ShipSystemReferencer>().GetHullObject().GetComponent<HullSystemController>();
        }
    }

    public void DereferencePlayer()
    {
        _isPlayerReferencesInitialized = false;
    }

}
