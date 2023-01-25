using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class UiManager : MonoSingleton<UiManager>
{
    //Declarations
    [SerializeField] private BubbleCollectionController _healthControllerRef;
    [SerializeField] private BubbleCollectionController _shieldsControllerRef;
    [SerializeField] private BubbleCollectionController _cargoBusterControllerRef;


    //monobehaviors



    //Utilities

    //Getters And Setters
    public BubbleCollectionController GetHealthUiController()
    {
        return _healthControllerRef;
    }

    public BubbleCollectionController GetShieldsUiController()
    {
        return _shieldsControllerRef;
    }

    public BubbleCollectionController GetCargoBusterUiController()
    {
        return _cargoBusterControllerRef;
    }
}
