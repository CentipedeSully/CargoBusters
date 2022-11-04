using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInstanceID : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"Object Name: {gameObject.name}, Object ID: {gameObject.GetInstanceID()}, Object Tag: {gameObject.tag}");
    }
}
