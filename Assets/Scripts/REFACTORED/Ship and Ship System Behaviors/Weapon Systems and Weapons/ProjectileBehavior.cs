using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public interface IProjectile
{
    int GetOwnerID();

    int GetInstanceID();

    GameObject GetGameObject();

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

public class ProjectileBehavior : MonoBehaviour, IProjectile, IDamageable
{
    //Declarations
    [Header("Projectile Metadata")]
    [SerializeField] protected bool _isFired = false;
    [SerializeField] protected int _ownerID = 0;
    [SerializeField] protected int _selfID = 0;
    [SerializeField] protected int _damageableID = 0;
    [SerializeField] protected float _currentLifetime = 0;
    [SerializeField] protected float _maxLifetime;
    [SerializeField] protected float _speed;
    [SerializeField] protected Vector2 _travelDirection;
    [SerializeField] protected int _damage;

    [Header("Debug Utils")]
    [SerializeField] private bool _isDebugActive = true;



    //Monobehaviours
    private void Awake()
    {
        _selfID = GetInstanceID();
    }

    private void Update()
    {
        if (_isFired)
        {
            TravelWithoutPhysics();
            CountLifetime();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D detectedCollider = collision;
        if (_isFired && GameManager.Instance.GetDamageableTagsList().Contains(detectedCollider.tag))
        {
            IDamageable damageableComponentRef = detectedCollider.GetComponent<IDamageable>();

            STKDebugLogger.LogStatement(_isDebugActive, $"Owner Ship ID: {_ownerID}");
            STKDebugLogger.LogStatement(_isDebugActive, $"Hit Collider: {detectedCollider.name}");
            STKDebugLogger.LogStatement(_isDebugActive, $"Is Object Damageable: {damageableComponentRef != null}");

            if (damageableComponentRef != null)
            {
                STKDebugLogger.LogStatement(_isDebugActive, $"Damageable Object ID: {damageableComponentRef.GetInstanceID()}");
                if (damageableComponentRef.GetInstanceID() != _ownerID)
                {
                    damageableComponentRef.TakeDamage(_damage, false);
                    ExpireProjectile();
                }
                
            }
            
        }
    }

    //Internal Utils
    //...




    //Getters, Setters, & Commands
    public virtual GameObject GetGameObject()
    {
        return gameObject;
    }

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

    public virtual void TakeDamage(int value, bool negateDeath)
    {
        ExpireProjectile();
    }
}
