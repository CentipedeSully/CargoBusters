using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAttributesOnEnable : MonoBehaviour
{
    [SerializeField] private float _minMoveSpeed = 300;
    [SerializeField] private float _maxMoveSpeed = 600;
    [SerializeField] private float _minRotationSpeed = 50;
    [SerializeField] private float _maxRotationSpeed = 100;


    private AddRotationToObject _rotateScriptRef;
    private MoveObject _moveScriptRef;

    private void Awake()
    {
        _moveScriptRef = GetComponent<MoveObject>();
        _rotateScriptRef = GetComponent<AddRotationToObject>();
    }

    private void OnEnable()
    {
        RandomizeMoveSpeed();
        RandomizeRotationSpeed();
    }


    private void RandomizeMoveSpeed()
    {
        _moveScriptRef.SetSpeed(Random.Range(_minMoveSpeed, _maxMoveSpeed));
    }

    private void RandomizeRotationSpeed()
    {
        _rotateScriptRef.SetRotationSpeed(Random.Range(_minRotationSpeed, _maxRotationSpeed));
    }

}
