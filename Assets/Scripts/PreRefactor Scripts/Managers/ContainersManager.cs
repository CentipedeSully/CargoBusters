using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class ContainersManager : MonoSingleton<ContainersManager>
{
    //Declarations
    [SerializeField] private GameObject _lasersContainer;
    [SerializeField] private GameObject _explosionsContainer;
    [SerializeField] private GameObject _shipsContainer;
    [SerializeField] private OldPlayerObjectManager _playerObjectManager;
    [SerializeField] private GameObject _visualizerContainer;


    //Monos
    //...


    //Monosingletons
    //...


    //Utils
    public GameObject GetLasersContainer()
    {
        return _lasersContainer;
    }

    public GameObject GetExplosionsContainer()
    {
        return _explosionsContainer;
    }

    public GameObject GetShipsContainer()
    {
        return _shipsContainer;
    }

    public GameObject GetVisualizerContainer()
    {
        return _visualizerContainer;
    }
}
