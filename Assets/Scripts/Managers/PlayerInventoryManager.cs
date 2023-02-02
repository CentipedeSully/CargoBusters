using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SullysToolkit;

public class PlayerInventoryManager : MonoSingleton<PlayerInventoryManager>
{
    //Declarations
    [SerializeField] private bool _isDebugEnabled = false;

    [Header("Scrap")]
    [Tooltip("ItemCode: 0")]
    [SerializeField] private int _currentScrap = 0;
    [SerializeField] private int _scrapTotalCollected = 0;

    [Header("Energy Cells")]
    [Tooltip("ItemCode: 1")]
    [SerializeField] private int _currentEnergyCells = 0;
    [SerializeField] private int _energyCellsTotalCollected = 0;

    [Header("Warp Coils")]
    [Tooltip("ItemCode: 2")]
    [SerializeField] private int _currentWarpCoils = 0;
    [SerializeField] private int _warpCoilsTotoalCollected = 0;

    [Header("Plasma Accelerators")]
    [Tooltip("ItemCode: 3")]
    [SerializeField] private int _currentPlasmaAccelerators = 0;
    [SerializeField] private int _plasmaAcceleratorsTotoalCollected = 0;

    [Header("Cannon Alloys")]
    [Tooltip("ItemCode: 4")]
    [SerializeField] private int _currentCannonAlloys = 0;
    [SerializeField] private int _cannonAlloysTotalCollected = 0;

    [Header("Events")]
    public UnityEvent<int, int> OnItemAmountIncremented;
    public UnityEvent<int, int> OnItemAmountDecremented;


    //Monobehaviors
    //...


    //Utilites
    private void LogItemAdded(int count, string itemName)
    {
        if (_isDebugEnabled)
            Debug.Log(count + " " + itemName + " added to inventory");
    }

    private void LogItemRemoved(int count, string itemName)
    {
        if (_isDebugEnabled)
            Debug.Log(count + " " + itemName + " removed to inventory");
    }

    public void IncrementItemCount(int itemCode, int amount)
    {
        if (amount < 0)
            amount = 0;

        switch (itemCode)
        {
            case 0:
                _scrapTotalCollected += amount;
                _currentScrap += amount;
                LogItemAdded(amount, "scrap");
                UiManager.Instance.GetScrapCountText().text = _currentScrap.ToString();
                UiManager.Instance.GetScrapCollectedText().text = _scrapTotalCollected.ToString();
                break;

            case 1:
                _energyCellsTotalCollected += amount;
                _currentEnergyCells += amount;
                LogItemAdded(amount, "energy cells");
                UiManager.Instance.GetEnergyCellsCountText().text = _currentEnergyCells.ToString();
                break;

            case 2:
                _warpCoilsTotoalCollected += amount;
                _currentWarpCoils += amount;
                LogItemAdded(amount, "warp coils");
                UiManager.Instance.GetWarpCoilsCountText().text = _currentWarpCoils.ToString();
                break;

            case 3:
                _plasmaAcceleratorsTotoalCollected += amount;
                _currentPlasmaAccelerators += amount;
                LogItemAdded(amount, "plasma accelerators");
                UiManager.Instance.GetPlasmaAcceleratorsCountText().text = _currentPlasmaAccelerators.ToString();
                break;

            case 4:
                _cannonAlloysTotalCollected += amount;
                _currentCannonAlloys += amount;
                LogItemAdded(amount, "cannon alloys");
                UiManager.Instance.GetCannonAlloysCountText().text = _currentCannonAlloys.ToString();
                break;
        }

        OnItemAmountIncremented?.Invoke(itemCode, amount);
    }

    public void DecrementItemCount(int itemCode, int amount)
    {
        amount = Mathf.Abs(amount);

        switch (itemCode)
        {
            case 0:
                if (_currentScrap >= amount)
                {
                    _currentScrap -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
                    LogItemRemoved(amount, "scrap");
                    UiManager.Instance.GetScrapCountText().text = _currentScrap.ToString();
                }
                    
                break;

            case 1:
                if (_currentEnergyCells >= amount)
                {
                    _currentEnergyCells -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
                    LogItemRemoved(amount, "energy cells");
                    UiManager.Instance.GetEnergyCellsCountText().text = _currentEnergyCells.ToString();
                }
                    
                break;

            case 2:
                if (_currentWarpCoils >= amount)
                {
                    _currentWarpCoils -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
                    LogItemRemoved(amount, "warp coils");
                    UiManager.Instance.GetWarpCoilsCountText().text = _currentWarpCoils.ToString();
                }
                    
                break;

            case 3:
                if (_currentPlasmaAccelerators >= amount)
                {
                    _currentPlasmaAccelerators -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
                    LogItemRemoved(amount, "plasma accelerators");
                    UiManager.Instance.GetPlasmaAcceleratorsCountText().text = _currentPlasmaAccelerators.ToString();
                }
                    
                break;

            case 4:
                if (_currentCannonAlloys >= amount)
                {
                    _currentCannonAlloys -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
                    LogItemRemoved(amount, "cannon alloys");
                    UiManager.Instance.GetCannonAlloysCountText().text = _currentCannonAlloys.ToString();
                }

                break;
        }
    }

    public int GetItemCount(int code)
    {
        switch (code)
        {
            case 0:
                return _currentScrap;

            case 1:
                return _currentEnergyCells;

            case 2:
                return _currentWarpCoils;

            case 3:
                return _currentPlasmaAccelerators;

            case 4:
                return _currentCannonAlloys;

            default:
                return -1;
        }
    }

    public void DebugAddAll()
    {
        IncrementItemCount(0, 500);
        IncrementItemCount(1, 10);
        IncrementItemCount(2, 10);
        IncrementItemCount(3, 10);
        IncrementItemCount(4, 10);

    }

}
