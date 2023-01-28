using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StatusVisualizer : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> _pointObjects;
    [SerializeField] private int _activeIndex;
    [SerializeField] private string _spawnRateFieldName = "SpawnRate";


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
            point.GetComponent<VisualEffect>().SetInt(_spawnRateFieldName,0);
    }

    public void ActivateSinglePoint()
    {
        if (_activeIndex > 0)
        {
            _activeIndex--;
            _pointObjects[_activeIndex].GetComponent<VisualEffect>().SetInt(_spawnRateFieldName, 32); ;
        }
    }

    public void DeactivateSinglePoint()
    {
        if (_activeIndex < _pointObjects.Count)
        {
            _pointObjects[_activeIndex].GetComponent<VisualEffect>().SetInt(_spawnRateFieldName, 0);
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
