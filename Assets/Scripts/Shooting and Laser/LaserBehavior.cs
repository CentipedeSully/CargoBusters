using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class LaserBehavior : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 500;
    [SerializeField] private float _lifeExpectancy = 5;
    [SerializeField] private float _currentLifetime = 0;

    private bool _isEnabled = false;
    private bool _isPropelled = false;

    private void OnEnable()
    {
        _isEnabled = true;
        
    }

    private void Update()
    {
        TrackLifetime();
    }

    private void FixedUpdate()
    {
        PropelLaser();
    }

    private void OnDisable()
    {
        ResetProjectile();
        ObjectPooler.PoolObject(this.gameObject);
    }



    private void PropelLaser()
    {
        if (_isEnabled && !_isPropelled)
        {
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.up * _moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
            _isPropelled = true;
        }
            
    }

    private void DisableOnLifetimeExpiration()
    {
        if (_currentLifetime >= _lifeExpectancy)
        {
            _isEnabled = false;
            gameObject.SetActive(false);
        }
            

    }

    private void TrackLifetime()
    { 
        if (_isEnabled)
        {
            _currentLifetime += Time.deltaTime;

            DisableOnLifetimeExpiration();
        }
    }

    private void ResetProjectile()
    {
        _currentLifetime = 0;
        _isPropelled = false;
        _isEnabled = false;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }

}
