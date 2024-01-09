using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlasterWeapon : AbstractShipWeapon
{
    //Declarations
    [Header("Projectile Settings")]
    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected Vector2 _localShotDirection = Vector2.up;
    [SerializeField] [Min(0)] protected float _projectileSpeed = 5;
    [SerializeField] [Min(.1f)] protected float _projectileLifetime = 1f;

    public event ShipWeaponEvent OnProjectileFired;


    //Monobehaviours
    //...



    //Internal Utils
    protected override void PerformWeaponFireLogic()
    {
        FireProjectile();
        OnProjectileFired?.Invoke();
        EnterCooldown();
    }

    protected virtual GameObject SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        newProjectile.transform.SetParent(GameManager.Instance.GetProjectileContainer(), true);
        return newProjectile;
    }

    protected virtual void FireProjectile()
    {
        GameObject projectileObject = SpawnProjectile();
        ProjectileBehavior projectileRef = projectileObject.GetComponent<ProjectileBehavior>();

        float relativeProjectileSpeed = _projectileSpeed + Mathf.Abs(_parentShip.GetComponent<Rigidbody2D>().velocity.magnitude);
        Vector2 relativeDirection = (Vector2)transform.TransformDirection(_localShotDirection);

        projectileRef.InitializeProjectile(_parentShip.GetInstanceID(), relativeProjectileSpeed, relativeDirection, _projectileLifetime, _damage);
    }




    //Getters Setters, & Commands
    public float GetProjectileSpeed()
    {
        return _projectileSpeed;
    }

    public void SetProjectileSpeed(float newSpeed)
    {
        _projectileSpeed = Mathf.Max(0, newSpeed);
    }

    public Vector2 GetLocalShotDirection()
    {
        return _localShotDirection;
    }

    public void SetLocalShotDirection(Vector2 newDirection)
    {
        _localShotDirection = newDirection.normalized;
    }

    public float GetProjectileLifetime()
    {
        return _projectileLifetime;
    }

    public void SetProjectileLifetime(float newLifetime)
    {
        _projectileLifetime = Mathf.Max(.1f, newLifetime);
    }

    public GameObject GetProjectilePrefab()
    {
        return _projectilePrefab;
    }

    public void SetProjectilePrefab(GameObject newPrefab)
    {
        if (newPrefab != null)
            _projectilePrefab = newPrefab;
    }


    //Debug Utils
    //...

}
