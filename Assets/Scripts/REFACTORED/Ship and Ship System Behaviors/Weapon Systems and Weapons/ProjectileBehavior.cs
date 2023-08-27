using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public interface IProjectile
{
    int GetOwnerID();

    void SetOwnerID(int value);

    float GetMaxLifetime();

    void SetMaxLifetime(float newValue);

    float GetCurrentLifetime();

    void ResetCurrentLifetime();

    Vector2 GetCurrentDirection();

    void SetCurrentDirection(Vector2 newDirection);

    void InitializeProjectile(int newOwner, float speed, Vector2 direction, float maxLifetime, int damage);

    void ExpireProjectile();

    void TravelWithoutPhysics();

    void CountLifetime();

    int GetDamage();

    void SetDamage(int value);

}

public class ProjectileBehavior : MonoBehaviour, IProjectile
{
    //Declarations
    [Header("Projectile Metadata")]
    [SerializeField] protected bool _isFired = false;
    [SerializeField] protected int _ownerID = 0;
    [SerializeField] protected float _currentLifetime = 0;
    [SerializeField] protected float _maxLifetime;
    [SerializeField] protected float _speed;
    [SerializeField] protected Vector2 _travelDirection;
    [SerializeField] protected int _damage;

    [Header("Debug Utils")]
    [SerializeField] private bool _isDebugActive;



    //Monobehaviours
    private void Update()
    {
        if (_isFired)
        {
            TravelWithoutPhysics();
            CountLifetime();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetInstanceID() != _ownerID && _isFired)
        {
            STKDebugLogger.LogStatement(_isDebugActive, $"Hit Collider: {collision.collider.name}");
            ExpireProjectile();
        }
    }

    //Internal Utils
    //...
    



    //Getters, Setters, & Commands
    public virtual int GetOwnerID()
    {
        return _ownerID;
    }

    public virtual void SetOwnerID(int newID)
    {
        _ownerID = newID;
    }

    public virtual float GetMaxLifetime()
    {
        return _maxLifetime;
    }

    public virtual void SetMaxLifetime(float newValue)
    {
        _maxLifetime = newValue;
    }

    public virtual float GetCurrentLifetime()
    {
        return _currentLifetime;
    }

    public virtual void ResetCurrentLifetime()
    {
        _currentLifetime = 0;
    }

    public virtual Vector2 GetCurrentDirection()
    {
        return _travelDirection;
    }

    public virtual void SetCurrentDirection(Vector2 newDirection)
    {
        _travelDirection = newDirection;
    }

    public virtual int GetDamage()
    {
        return _damage;
    }

    public virtual void SetDamage(int newValue)
    {
        _damage = Mathf.Max(0, newValue);
    }

    public virtual void InitializeProjectile(int newOwner, float speed, Vector2 direction, float maxLifetime, int damage)
    {
        _ownerID = newOwner;
        _speed = speed;
        _travelDirection = direction;
        _maxLifetime = maxLifetime;
        _damage = damage;

        _isFired = true;
    }

    public virtual void ExpireProjectile()
    {
        Destroy(this.gameObject);
    }

    public virtual void TravelWithoutPhysics()
    {
        Vector2 distance = _travelDirection * _speed * Time.deltaTime;
        transform.position = transform.position + (Vector3)distance;
    }

    public virtual void CountLifetime()
    {
        _currentLifetime += Time.deltaTime;

        if (_currentLifetime >= _maxLifetime)
            ExpireProjectile();
    }


}