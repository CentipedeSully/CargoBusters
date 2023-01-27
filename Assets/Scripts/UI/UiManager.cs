using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;
using TMPro;

public class UiManager : MonoSingleton<UiManager>
{
    //Declarations
    [SerializeField] private BubbleCollectionController _healthControllerRef;
    [SerializeField] private BubbleCollectionController _shieldsControllerRef;
    [SerializeField] private BubbleCollectionController _cargoBusterControllerRef;
    [SerializeField] private BubbleCollectionController _warpControllerRef;
    [SerializeField] private DisplayAnimController _intermissionTimerDisplayRef;
    [SerializeField] private TextMeshProUGUI _intermissionTimeTextRef;
    [SerializeField] private DisplayAnimController _outOfBoundsTimerDisplayRef;
    [SerializeField] private TextMeshProUGUI _outOfBoundsTimeTextRef;


    //monobehaviors
    //...


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

    public BubbleCollectionController GetWarpUiController()
    {
        return _warpControllerRef;
    }

    public DisplayAnimController GetIntermissionTimerDisplay()
    {
        return _intermissionTimerDisplayRef;
    }

    public TextMeshProUGUI GetIntermissionTimerText()
    {
        return _intermissionTimeTextRef;
    }

    public DisplayAnimController GetBoundaryTimerDisplay()
    {
        return _outOfBoundsTimerDisplayRef;
    }

    public TextMeshProUGUI GetBoundaryTimerText()
    {
        return _outOfBoundsTimeTextRef;
    }
}
