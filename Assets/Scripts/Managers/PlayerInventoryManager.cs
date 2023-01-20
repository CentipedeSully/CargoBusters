using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SullysToolkit;

public class PlayerInventoryManager : MonoSingleton<PlayerInventoryManager>
{
    //Declarations
    [Tooltip("ItemCode: 0")]
    [SerializeField] private int _currentScrap = 0;
    [SerializeField] private int _scrapTotalCollected = 0;

    [Tooltip("ItemCode: 1")]
    [SerializeField] private int _currentEnergyCells = 0;
    [SerializeField] private int _energyCellsTotalCollected = 0;

    [Tooltip("ItemCode: 2")]
    [SerializeField] private int _currentWarpCoils = 0;
    [SerializeField] private int _warpCoilsTotoalCollected = 0;

    [Tooltip("ItemCode: 3")]
    [SerializeField] private int _currentPlasmaAccelerators = 0;
    [SerializeField] private int _plasmaAcceleratorsTotoalCollected = 0;

    [Tooltip("ItemCode: 4")]
    [SerializeField] private int _currentCannonAlloys = 0;
    [SerializeField] private int _cannonAlloysTotalCollected = 0;


    public UnityEvent<int, int> OnItemAmountIncremented;
    public UnityEvent<int, int> OnItemAmountDecremented;


    //Monobehaviors
    //...


    //Utilites
    public void IncrementItemCount(int itemCode, int amount)
    {
        if (amount < 0)
            amount = 0;

        switch (itemCode)
        {
            case 0:
                _scrapTotalCollected += amount;
                _currentScrap += amount;
                break;

            case 1:
                _energyCellsTotalCollected += amount;
                _currentEnergyCells += amount;
                break;

            case 2:
                _warpCoilsTotoalCollected += amount;
                _currentWarpCoils += amount;
                break;

            case 3:
                _plasmaAcceleratorsTotoalCollected += amount;
                _currentPlasmaAccelerators += amount;
                break;

            case 4:
                _cannonAlloysTotalCollected += amount;
                _currentCannonAlloys += amount;
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
                if (_currentScrap <= amount)
                {
                    _currentScrap -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
                }
                    
                break;

            case 1:
                if (_currentEnergyCells <= amount)
                {
                    _currentEnergyCells -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
                }
                    
                break;

            case 2:
                if (_currentWarpCoils <= amount)
                {
                    _currentWarpCoils -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
                }
                    
                break;

            case 3:
                if (_currentPlasmaAccelerators <= amount)
                {
                    _currentPlasmaAccelerators -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
                }
                    
                break;

            case 4:
                if (_currentCannonAlloys <= amount)
                {
                    _currentCannonAlloys -= amount;
                    OnItemAmountDecremented?.Invoke(itemCode, amount);
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

}
