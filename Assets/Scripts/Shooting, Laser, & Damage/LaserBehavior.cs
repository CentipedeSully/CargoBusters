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
    [SerializeField] private Vector2 _speedOffset;
    [SerializeField] private float _forceMagnitude = 5;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private int _damage = 0;
    [SerializeField]private float _shooterID;
    [SerializeField] private bool _isShooterIDSet = false;
    private bool _isEnabled = false;



    //Monobehaviors
    private void Update()
    {
        TrackLifetime();
        MoveLaser();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ship") && _isShooterIDSet && _shooterID != collision.gameObject.GetComponent<ShipInformation>().GetShipID())
        {
            CreateExplosion();
            EndLaser();
        }
    }



    //Utilities
    public void EnableLaserBehavior()
    {
        if (!_isEnabled)
            _isEnabled = true;
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
                Destroy(this.gameObject);
            }
        }
    }

    private void MoveLaser()
    {
        transform.Translate(new Vector2(_speedOffset.x * Time.deltaTime, 1 * Time.deltaTime * (_moveSpeed + _speedOffset.y)));
    }

    private void EndLaser()
    {
        /*
        _isEnabled = false;
        ResetProjectile();
        PoolSelf();
        */

        Destroy(gameObject);
    }

    private void ResetProjectile()
    {
        _currentLifetime = 0;
        _speedOffset = Vector2.zero;
        _isEnabled = false;
        _isShooterIDSet = false;
        _forceMagnitude = 0;

    }

    private void PoolSelf()
    {
        ObjectPooler.PoolObject(this.gameObject);
    }

    public void SetSpeedOffset(Vector2 value)
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

    public void SetDamage(int value)
    {
        _damage = value;
    }
}
