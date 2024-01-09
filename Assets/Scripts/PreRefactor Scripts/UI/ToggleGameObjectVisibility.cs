using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjectVisibility : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private bool _isVisible = false;

    public void ToggleVisibility()
    {
        if (_isVisible)
        {
            _isVisible = false;
            _object.SetActive(false);
        }

        else
        {
            _isVisible = true;
            _object.SetActive(true);
        }
    }
}
