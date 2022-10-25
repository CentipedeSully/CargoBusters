using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAtPosition : MonoBehaviour
{
    [SerializeField] private Vector3 _startingPosition = Vector3.zero;

    


    void Start()
    {
        transform.position = _startingPosition;
    }
}
