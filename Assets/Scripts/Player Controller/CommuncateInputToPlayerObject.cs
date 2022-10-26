using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommuncateInputToPlayerObject : MonoBehaviour
{
    //Declarations
    private Vector2 _moveDirection = Vector2.zero;
    [SerializeField] private float _turnInput = 0;
    private bool _shootInput = false;
    private bool _boostInput = false;

    private MoveObject _playerMoveScriptReference;
    private AddRotationToObject _playerRotateScriptReference;
    private SpawnLaserOnInput _playerSpawnLaserScriptReference;


    //monos
    private void OnEnable()
    {
        InitializeReferencesIfNull();
    }


    private void Update()
    {
        GetInputsFromInputDetector();

        ShareMoveInputWithPlayerMoveScript();
        ShareRotateInputWithPlayerRotateScript();
        ShareShootInputWithLaserSpawnScript();
    }


    //Utils
    private void InitializeReferencesIfNull()
    {
        if (_playerMoveScriptReference == null)
            _playerMoveScriptReference = GetComponent<MoveObject>();

        if (_playerRotateScriptReference == null)
            _playerRotateScriptReference = GetComponent<AddRotationToObject>();

        if (_playerSpawnLaserScriptReference == null)
            _playerSpawnLaserScriptReference = GetComponent<SpawnLaserOnInput>();
    }

    private void GetInputsFromInputDetector()
    {
        _moveDirection = InputDetector.Instance.GetMoveInput();

        _shootInput = InputDetector.Instance.GetShootInput();

        _boostInput = InputDetector.Instance.GetBoostInput();

        _turnInput = InputDetector.Instance.GetTurnInput();
    }

    private void ShareMoveInputWithPlayerMoveScript()
    {
        _playerMoveScriptReference.SetDirection(_moveDirection);
    }

    private void ShareRotateInputWithPlayerRotateScript()
    {
        _playerRotateScriptReference.AddRotation(new Vector3(0, 0, _turnInput));
    }

    private void ShareShootInputWithLaserSpawnScript()
    {
        _playerSpawnLaserScriptReference.SetShootInput( InputDetector.Instance.GetShootInput());
    }


}
