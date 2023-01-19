using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliders : MonoBehaviour
{
    [SerializeField]private bool _isCollidersEnabled = true;
    [SerializeField] private GameObject _collidersParentObject;


    public void DisableCompositeCollider()
    {
        _collidersParentObject.SetActive(false);
    }

    public void EnableCompositeCollider()
    {
        _collidersParentObject.SetActive(true);
    }
}
