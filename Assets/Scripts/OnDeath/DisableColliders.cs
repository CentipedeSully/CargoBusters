using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliders : MonoBehaviour
{
    public void DisableCompositeCollider()
    {
        GetComponent<CompositeCollider2D>().enabled = false;
    }

    public void EnableCompositeCollider()
    {
        GetComponent<CompositeCollider2D>().enabled = true;
    }
}
