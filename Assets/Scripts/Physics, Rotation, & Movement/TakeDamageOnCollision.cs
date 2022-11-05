using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageOnCollision : MonoBehaviour
{
    //[SerializeField] private float _damagingVelocityThreshold = 50;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            TryDamageObject(gameObject, CalculateCollisionDamage());
            TryDamageObject(collision.gameObject, CalculateCollisionDamage());
        }
    }

    private void TryDamageObject(GameObject damagableObject, float damage)
    {
        if (damagableObject.GetComponent<HealthBehavior>() != null)
            damagableObject.GetComponent<HealthBehavior>().ModifyCurrentHealth(-damage);
    }

    private float CalculateCollisionDamage()
    {
        //if (GetComponent<Rigidbody2D>().velocity.magnitude > _damagingVelocityThreshold)
            return 1;

    }
}
