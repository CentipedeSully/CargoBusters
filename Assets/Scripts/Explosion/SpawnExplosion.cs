using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _forceMagnitude = 5;
    [SerializeField] private float _radius = 1;
    [SerializeField] private int _damage = 1;



    public void CreateExplosion()
    {
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, transform.rotation);
        explosion.GetComponent<ExplodeBehavior>().SetDamage(_damage);
        explosion.GetComponent<ExplodeBehavior>().SetForceMagnitude(_forceMagnitude);
        explosion.GetComponent<ExplodeBehavior>().SetRadius(_radius);

        explosion.GetComponent<ExplodeBehavior>().Explode();
    }
}
