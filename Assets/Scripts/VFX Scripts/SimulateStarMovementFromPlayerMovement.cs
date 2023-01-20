using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SimulateStarMovementFromPlayerMovement : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject _player;
    [SerializeField] private bool _isPlayerSet = false;
    [SerializeField] private Vector3 _simulatedVelocity;
    [SerializeField] private string _VfxVelocityFieldName;
    private VisualEffect _VFXReference;
    private Rigidbody2D _playerRB;



    //Monobehaviors
    private void Awake()
    {
        _VFXReference = GetComponent<VisualEffect>();
        
    }

    private void Update()
    {
        if (_isPlayerSet)
        {
            CalculateParticleMovementVelocity();
            SetVFXPositionToPlayerPosition();
        }
    }



    //Utilities
    private void CalculateParticleMovementVelocity()
    {
        if (_playerRB != null)
            _simulatedVelocity = -1 * _playerRB.velocity;
        else
            _simulatedVelocity = Vector3.zero;

        _VFXReference.SetVector3(_VfxVelocityFieldName, _simulatedVelocity);
    }
    
    private void SetVFXPositionToPlayerPosition()
    {
        if (_player != null)
            transform.position = _player.transform.position;
    }

    public void SetPlayerObject(GameObject player)
    {
        if (player != null)
        {
            _player = player;
            _isPlayerSet = true;

            _playerRB = _player.GetComponent<Rigidbody2D>();
        }
    }
}
