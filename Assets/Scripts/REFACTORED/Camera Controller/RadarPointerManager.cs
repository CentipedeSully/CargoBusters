using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum IconType
{
    neutral,
    hostile,
    friendly,
    undetermined
}

public class RadarPointerManager : MonoBehaviour
{
    //Declarations
    [Tooltip("How far away on the minimap will the icon be drawn from self to the target entity")]
    [SerializeField] private float _radarPointerDistance;

    [Tooltip("This is the object doing the scanning. All distances will be calculated from this object's transform")]
    [SerializeField] private GameObject _centerObject;

    /// <summary>
    /// Problem: 
    ///     1) How will targets be tracked? 
    ///     2) What happens when a target is lost for any reason?
    ///     3) How will distances be tracked? 
    ///     4) How will Icon types be determined?
    ///     5) How will Icons game objects be 
    ///         a) created 
    ///         b) destroyed 
    ///         c) stored
    ///         d) handled when the center object is changed
    /// 
    /// Solution: 
    ///     1) The center object should have a scanner behaviour. That behaviour already tracks
    ///     scannable gameObjects (via ID) within a certain range. Get the radar objects from the scanner.
    ///     
    ///     2) If the ID doesn't exist within the scanner, then it either doesn't exist anymore (within range) 
    ///     or it never existed.
    ///     
    ///     3) Distances will be tracked each frame via a dictionary of (ID,range) pairs. If the Id exists 
    ///     on the scanner, update the distance. If the ID doesn't exist, then remove that Id from the dict
    ///     
    ///     4) Icon types may change anytime, so they'll be determined each frame. [needs to be completed]
    /// 
    /// Problem: 
    /// </summary>
    [Tooltip("The list of active, unhidden pointer objects")]
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
        _radarPointerDistance = Mathf.Max(0, newDistance);
    }

    public void SetCenterObject(GameObject newObject)
    {
        if (newObject != null)
            _centerObject = newObject;
    }




}
