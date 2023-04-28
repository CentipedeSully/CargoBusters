using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInstanceTracker
{
    void ReportDeath(GameObject instance);

    int GetCount(string tag);

    Transform GetContainer(string tag);
}



public class InstanceTracker : MonoBehaviour, IInstanceTracker
{
    //Declarations
    [SerializeField] private Transform _playerShipsContainer;
    [SerializeField] private Transform _nonPlayerShipsContainer;
    [SerializeField] private Transform _projectilesContainer;
    [SerializeField] private Transform _explosionsContainer;

    [SerializeField] private int _nonPlayerShipCount;
    [SerializeField] private int _playerShipCount;
    [SerializeField] private int _projectilesCount;
    [SerializeField] private int _explosionsCount;

    [SerializeField] private string _NPShipTag;
    [SerializeField] private string _playerShipTag;
    [SerializeField] private string _projectileTag;
    [SerializeField] private string _explosionTag;



    //Monobehaviours
    //...




    //Interface Utils
    public void ReportDeath(GameObject deadObject)
    {
        if (deadObject.CompareTag(_NPShipTag))
            _nonPlayerShipCount--;

        else  if (deadObject.CompareTag(_playerShipTag))
            _playerShipCount--;

        else if (deadObject.CompareTag(_projectileTag))
            _projectilesCount--;

        else if (deadObject.CompareTag(_explosionTag))
            _explosionsCount--;

    }


    public int GetCount(string objectTag)
    {
        if (objectTag == _playerShipTag)
            return _playerShipCount;
        else if (objectTag == _NPShipTag)
            return _nonPlayerShipCount;
        else if (objectTag == _projectileTag)
            return _projectilesCount;
        else if (objectTag == _explosionTag)
            return _explosionsCount;
        else return 0;
    }

    public Transform GetContainer(string objectTag)
    {
        if (objectTag == _playerShipTag)
            return _playerShipsContainer;
        else if (objectTag == _NPShipTag)
            return _nonPlayerShipsContainer;
        else if (objectTag == _projectileTag)
            return _projectilesContainer;
        else if (objectTag == _explosionTag)
            return _explosionsContainer;
        else
        {
            Debug.LogError($"Requested Nonexistent Instance Container '{objectTag}'. returning null transform.");
            return null;
        }
    }

    //PROBLEM! PLAYER SHIPS ARENT DIFFERENT FROM NPSHIPS. DETERMINE ORGANIZATION THAT'S BEYOND TAG


    //Debugging
    //...



}
