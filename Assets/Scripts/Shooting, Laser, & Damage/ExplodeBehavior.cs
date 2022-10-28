using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBehavior : MonoBehaviour
{
    //Declarations
    private float _radius = 1;
    private float _damage = 1;
    private float _pushOffset = 100;
    [SerializeField] private float _forceMagnitude = 5;

    private bool _explodeCommand = false;


    //Monobehaviors
    private void FixedUpdate()
    {
        if (_explodeCommand)
        {
            PushAwayAllRigidbodiesInRadius();
            _explodeCommand = false;

            Destroy(gameObject);
        }
    }


    //Utilities
    private void PushAwayAllRigidbodiesInRadius()
    {
        //Collect all colliders within the radius
        Collider2D[] allCollidersWithinArea = Physics2D.OverlapCircleAll(transform.position, _radius);

        //push away all rigidbodies that're in the list of colliders
        for (int i = 0; i < allCollidersWithinArea.Length; i++)
        {
            //Debug.Log($"{allCollidersWithinArea[i].gameObject.name} Rigidbody Detected: {allCollidersWithinArea[i].gameObject.GetComponent<Rigidbody2D>() != null}");
            if (allCollidersWithinArea[i].gameObject.GetComponent<Rigidbody2D>() != null)
                PushRigidbodyAway(allCollidersWithinArea[i].gameObject.GetComponent<Rigidbody2D>());
        }


        
    }

    private void PushRigidbodyAway(Rigidbody2D rigidbody)
    {
        rigidbody.AddForce( NormalizeForceVector(rigidbody.transform.position - transform.position) * Time.deltaTime * _forceMagnitude * _pushOffset, ForceMode2D.Impulse);
    }

    private Vector3 NormalizeForceVector(Vector3 positionalVector)
    {
        return new Vector3(Mathf.Clamp(positionalVector.x, -1, 1), Mathf.Clamp(positionalVector.y, -1, 1), Mathf.Clamp(positionalVector.z, -1, 1));
    }



    public void SetRadius(float value)
    {
        if (value > 0)
            _radius = value;
    }

    public void SetDamage(float value)
    {
        _damage = value;
    }

    public void SetForceMagnitude(float value)
    {
        _forceMagnitude = value;
    }

    public void Explode()
    {
        _explodeCommand = true;
    }

}
