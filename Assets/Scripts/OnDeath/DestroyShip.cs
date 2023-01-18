using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShip : MonoBehaviour
{
    [SerializeField] private float _delay = .25f;

    public void DestroyShipObject()
    {
        Destroy(transform.parent.gameObject, _delay);
    }

    public void DestroyShipObject(float specificDelay)
    {
        Destroy(transform.parent.gameObject, specificDelay);
    }

}
