using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommuncateInputToPlayerObject : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject _playerShipObject;
    private Vector2 _moveDirection = Vector2.zero;
    private float _turnInput = 0;
    private bool _shootInput = false;
    private bool _boostInput = false;

    //References
    private EnginesSystemController _playerEnginesControllerRef;
    private WeaponsSystemController _playerShotCommanderScriptRef;


    //monos
    private void Start()
    {
        InitializeReferences();
    }


    private void Update()
    {
        GetInputsFromInputDetector();
        ShareMoveInputWithEnginesControllerScript();
        ShareShootInputWithWeaponsControllerScript();
    }


    //Utils
    private void InitializeReferences()
    {
        //Establish Refererences
        _playerEnginesControllerRef = _playerShipObject.GetComponent<ShipSystemReferencer>().GetEnginesObject().GetComponent<EnginesSystemController>();
        _playerShotCommanderScriptRef = _playerShipObject.GetComponent<ShipSystemReferencer>().GetWeaponsObject().GetComponent<WeaponsSystemController>();
    }

    private void GetInputsFromInputDetector()
    {
        _moveDirection = InputDetector.Instance.GetMoveInput();

        _shootInput = InputDetector.Instance.GetShootInput();

        _boostInput = InputDetector.Instance.GetBoostInput();

        _turnInput = InputDetector.Instance.GetTurnInput();
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

}
