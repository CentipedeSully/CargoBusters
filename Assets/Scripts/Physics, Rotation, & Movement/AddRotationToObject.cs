using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRotationToObject: MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _rotationSpeed = 1;

    private void OnEnable()
    {
        _rotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        ApplyRotation();
    }


    private void ApplyRotation()
    {
        //Debug.Log(Quaternion.Euler(_rotation * Time.deltaTime * _rotationSpeed));
        transform.rotation = Quaternion.Euler(_rotation);
    }

    public void AddRotation(Vector3 newRotation)
    {
        _rotation += newRotation * Time.deltaTime * _rotationSpeed;

        _rotation = new Vector3 (ClampAngle(_rotation.x), ClampAngle(_rotation.y), ClampAngle(_rotation.z));
    }

    private float ClampAngle(float value, float minValue = float.MinValue, float maxValue = float.MaxValue)
    {
        if (value < 0) value += 360;
        else if (value >= 360) value -= 360;

        return Mathf.Clamp(value, minValue, maxValue);
    }
}
