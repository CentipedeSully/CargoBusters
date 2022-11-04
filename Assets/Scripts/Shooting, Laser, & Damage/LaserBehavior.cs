using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class LaserBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _lifeExpectancy = 5;
    [SerializeField] private float _currentLifetime = 0;
    [SerializeField] private float _speedOffset = 0;
    [SerializeField] private float _forceMagnitude = 5;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _damage = 0;

    private float _shooterID;
    private bool _isShooterIDSet = false;
    private bool _isEnabled = false;



    //Monobehaviors
    private void Update()
    {
        TrackLifetime();
        MoveLaser();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isShooterIDSet && _shooterID != collision.gameObject.GetInstanceID())
        {
            //Debug.Log($"Hit Detected. Name: {collision.gameObject.name }, ID: {collision.gameObject.GetInstanceID()}");
            CreateExplosion();
            EndLaser();
        }
    }



    //Utilities
    public void EnableLaserBehavior()
    {
        if (!_isEnabled)
            _isEnabled = true;
        //else Debug.LogError("Attempting to enable laser that's already enabled");
    }

    private void TrackLifetime()
    {
        if (_isEnabled)
        {
            _currentLifetime += Time.deltaTime;

            if (_currentLifetime >= _lifeExpectancy)
            {
                CreateExplosion();
                EndLaser();
            }
        }
    }

    private void MoveLaser()
    {
        transform.Translate(Vector2.up * Time.deltaTime * (_moveSpeed + _speedOffset));
    }

    private void EndLaser()
    {
        _isEnabled = false;
        ResetProjectile();
        PoolSelf();
    }

    private void ResetProjectile()
    {
        _currentLifetime = 0;
        _speedOffset = 0;
        _isEnabled = false;
        _isShooterIDSet = false;
        _forceMagnitude = 0;

    }

    private void PoolSelf()
    {
        ObjectPooler.PoolObject(this.gameObject);
    }

    public void SetSpeedOffset(float value)
    {
        _speedOffset = value;
    }

    public void SetShooterID(float value)
    {
        _isShooterIDSet = true;
        _shooterID = value;
    }

    private void CreateExplosion()
    {
        GameObject explosion = Instantiate(_explosionPrefab,transform.position,transform.rotation);

        explosion.GetComponent<ExplodeBehavior>().SetForceMagnitude(_forceMagnitude);
        explosion.GetComponent<ExplodeBehavior>().SetDamage(_damage);
        explosion.GetComponent<ExplodeBehavior>().Explode();
    }

    public void SetPushForce(float value)
    {
        _forceMagnitude = value;
    }

    public void SetDamage(float value)
    {
        _damage = value;
    }
}
