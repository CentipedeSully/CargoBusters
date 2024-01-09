using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    //Declarations
    [SerializeField] private Rigidbody2D _rigidbody2DReference;
    [SerializeField] private float _moveSpeed = 100;
    [SerializeField] private Vector2 _moveDirection = Vector2.zero;
    private bool _errorDetected = false;



    //Monobehaviors
    private void OnEnable()
    {
        //TryInitializingRigidbody2DReference();
    }

    private void FixedUpdate()
    {
        if (!_errorDetected)
            Move();
    }



    //Utilities
    private void TryInitializingRigidbody2DReference()
    {
        if (TryGetComponent<Rigidbody2D>(out _rigidbody2DReference) == false)
        {
            _errorDetected = true;
            Debug.LogError("No Rigidbody2D found on object: " + gameObject.name);
        }
    }

    private void Move()
    {
        _rigidbody2DReference.AddRelativeForce(_moveDirection * _moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newMoveDirection)
    {
        _moveDirection = newMoveDirection;
    }

    public void SetSpeed(float value)
    {
        _moveSpeed = value;
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }

    public float GetSpeed()
    {
        return _moveSpeed;
    }
}
