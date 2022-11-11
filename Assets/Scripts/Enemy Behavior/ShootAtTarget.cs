using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    //Cast Utilities
    [SerializeField] private Transform _laserSpawnPointTransform;
    [SerializeField] private LayerMask _validTargetsLayerMask;
    [SerializeField] private float _shotRangeDistance = 6;
    [SerializeField] private float _castRadius = 1;
    [SerializeField] private Vector2 _shotDirection = Vector2.zero;
    [SerializeField] private bool _shootSignal = false;
    private RaycastHit2D _detectedCollider;
    [SerializeField] private bool _isShootEnabled = true;

    [SerializeField] private SpawnLaserOnInput _spawnLaserScriptRef;


    //Monobehaviors
    private void Awake()
    {
        _spawnLaserScriptRef = GetComponent<SpawnLaserOnInput>();
    }

    private void Update()
    {
        if (_isShootEnabled)
        {
            CalculateWorldSpaceShotVector();
            CheckRangeForTarget();
            CommunicateShootSignalToLaserSpawner();
        }
       
    }

    private void OnDrawGizmos()
    {
        DrawShotRange(_detectedCollider);
    }

    //Utilities
    private void CommunicateShootSignalToLaserSpawner()
    {
        _spawnLaserScriptRef.SetShootInput(_shootSignal);
    }

    public void SetTarget(GameObject newTarget)
    {
        _target = newTarget;
    }

    private void CheckRangeForTarget()
    {
        _detectedCollider = Physics2D.CircleCast(_laserSpawnPointTransform.position, _castRadius, _shotDirection, _shotRangeDistance, _validTargetsLayerMask);

        if (_detectedCollider.collider != null)
            _shootSignal = true;
        else _shootSignal = false;
    }

    private void CalculateWorldSpaceShotVector()
    {
        _shotDirection = _laserSpawnPointTransform.TransformVector(new Vector2(0, _shotRangeDistance));
    }


    private void DrawShotRange(RaycastHit2D detectedCollider)
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine( _laserSpawnPointTransform.position, _laserSpawnPointTransform.position + _laserSpawnPointTransform.TransformDirection(new Vector2(0,_shotRangeDistance)));
        Gizmos.DrawWireSphere(_laserSpawnPointTransform.position + _laserSpawnPointTransform.TransformDirection(new Vector2(0, _shotRangeDistance)), _castRadius);
    }

    public void DisableShooting()
    {
        _isShootEnabled = false;
        _spawnLaserScriptRef.SetShootInput(false);
    }

    public void EnableShooting()
    {
        _isShootEnabled = true;
    }
}

