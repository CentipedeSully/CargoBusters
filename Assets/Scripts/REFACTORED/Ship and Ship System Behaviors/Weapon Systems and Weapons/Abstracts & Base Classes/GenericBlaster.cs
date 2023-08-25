using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBlaster : AbstractShipWeapon
{
    //Declarations
    [Header("Projectile Settings")]
    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected Vector2 _localShotDirection = Vector2.up;
    [SerializeField] [Min(0)] protected float _projectileSpeed = 5;
    [SerializeField] [Min(.1f)] protected float _projectileLifetime = 1f;



    //Monobehaviours




    //Internal Utils
    protected virtual GameObject SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        newProjectile.transform.SetParent(GameManager.Instance.GetProjectileContainer(), true);
        return newProjectile;
    }

    protected override void FireProjectile()
    {
        GameObject projectileObject = SpawnProjectile();
        ProjectileBehavior projectileRef = projectileObject.GetComponent<ProjectileBehavior>();

        float relativeProjectileSpeed = _projectileSpeed + Mathf.Abs(_parentShip.GetComponent<Rigidbody2D>().velocity.magnitude);
        Vector2 relativeDirection = (Vector2)transform.TransformDirection(_localShotDirection);

        projectileRef.InitializeProjectile(_parentShip.GetInstanceID(), relativeProjectileSpeed , relativeDirection, _projectileLifetime);
    }



    //Getters, Setter, & Commands
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

}
