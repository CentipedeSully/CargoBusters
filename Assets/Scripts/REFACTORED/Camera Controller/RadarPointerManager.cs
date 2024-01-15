using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPointerManager : MonoBehaviour
{
    //Declarations
    [SerializeField] private float _minObjectDistance;
    [SerializeField] private List<GameObject> _pointersList;
    private Dictionary<GameObject, float> _radarDistanceDict;



    //Monobehaviours
    private void Awake()
    {
        InitializeUtils();
    }



    //Internal Utils
    private void InitializeUtils()
    {
        _pointersList = new List<GameObject>();
        _radarDistanceDict = new Dictionary<GameObject, float>();
    }




    //External Utils
    public void SetMinimumObjectDistance( float newDistance)
    {
        _minObjectDistance = Mathf.Max(0, newDistance);
    }




}
