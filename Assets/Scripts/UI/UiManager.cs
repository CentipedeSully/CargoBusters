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
    [SerializeField] private GameObject _controlsDisplayRef;

    [Header("Inventory References")]
    [SerializeField] private DisplayAnimController _inventoryDisplayRef;
    [SerializeField] private TextMeshProUGUI _eCellsCount;
    [SerializeField] private TextMeshProUGUI _wCoilsCount;
    [SerializeField] private TextMeshProUGUI _pAccelsCount;
    [SerializeField] private TextMeshProUGUI _cAlloysCount;
    [SerializeField] private TextMeshProUGUI _scrapCount;

    [Header("Upgrader Reference")]
    [SerializeField] private UpgradeDescController _descriptionController;
    
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

    public DisplayAnimController GetInventoryDisplay()
    {
        return _inventoryDisplayRef;
    }

    public TextMeshProUGUI GetEnergyCellsCountText()
    {
        return _eCellsCount;
    }

    public TextMeshProUGUI GetWarpCoilsCountText()
    {
        return _wCoilsCount;
    }

    public TextMeshProUGUI GetPlasmaAcceleratorsCountText()
    {
        return _pAccelsCount;
    }

    public TextMeshProUGUI GetCannonAlloysCountText()
    {
        return _cAlloysCount;
    }

    public TextMeshProUGUI GetScrapCountText()
    {
        return _scrapCount;
    }

    public UpgradeDescController GetUpgradeDescController()
    {
        return _descriptionController;
    }

    public GameObject GetControlsDisplay()
    {
        return _controlsDisplayRef;
    }
}
