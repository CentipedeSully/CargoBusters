using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInstanceTracker
{
    void ReportDeath(GameObject instance);

    Transform GetPlayerShipContainer();

    Transform GetNonPlayerShipContainer();

    Transform GetProjectileContainer();

    Transform GetExplosionContainer();

    Transform GetWeaponContainer();
}



public class InstanceTracker : MonoBehaviour, IInstanceTracker
{
    //Declarations
    [SerializeField] private Transform _playerShipsContainer;
    [SerializeField] private Transform _nonPlayerShipsContainer;
    [SerializeField] private Transform _projectilesContainer;
    [SerializeField] private Transform _explosionsContainer;
    [SerializeField] private Transform _weaponsContainer;




    //Monobehaviours
    //...




    //Interface Utils
    public void ReportDeath(GameObject deadObject)
    {
        //...
    }

    public Transform GetExplosionContainer()
    {
        return _explosionsContainer;
    }

    public Transform GetNonPlayerShipContainer()
    {
        return _nonPlayerShipsContainer;
    }

    public Transform GetPlayerShipContainer()
    {
        return _playerShipsContainer;
    }

    public Transform GetProjectileContainer()
    {
        return _projectilesContainer;
    }

    public Transform GetWeaponContainer()
    {
        return _weaponsContainer;
    }


    //Debugging
    //...



}
