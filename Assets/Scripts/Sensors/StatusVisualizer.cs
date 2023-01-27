using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusVisualizer : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> _pointObjects;
    private int _activeIndex;


    //Monobehaviors
    private void Awake()
    {
        //Start the visualizer with no points active.
        ClearPoints();
    }


    //Utilites
    private void ClearPoints()
    {
        _activeIndex = _pointObjects.Count;

        foreach (GameObject point in _pointObjects)
            point.SetActive(false);
    }

    public void ActivateSinglePoint()
    {
        if (_activeIndex > 0)
        {
            _activeIndex--;
            _pointObjects[_activeIndex].SetActive(true);
        }
    }

    public void DeactivateSinglePoint()
    {
        if (_activeIndex < _pointObjects.Count)
        {
            _pointObjects[_activeIndex].SetActive(false);
            _activeIndex++;
        }
    }

    public void SetToValue(int desiredPoints)
    {
        if (desiredPoints <= _pointObjects.Count && desiredPoints >= 0)
        {
            ClearPoints();
            int count = 0;
            while (count < desiredPoints)
            {
                ActivateSinglePoint();
                count++;
            }
        }
    }
}
