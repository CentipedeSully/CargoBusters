using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class CargoLootDropper : MonoSingleton<CargoLootDropper>
{
    //Declarations
    [Tooltip("0 == Scrap, 1 == EnergyCells, 2 == WarpCoils, 3 == PlasmaAccelerators, 4 == CannonAlloys")]
    [SerializeField][Range(1,100)] private int _scrapDropChance = 100;
    [SerializeField] [Min (0)] private int _scrapDropMin = 15;
    [SerializeField] [Min (0)] private int _scrapDropMax = 35;

    [SerializeField] [Range(1, 100)] private int _energyCellDropChance = 15;
    [SerializeField] [Min(0)] private int _energyCellDropMin = 1;
    [SerializeField] [Min(0)] private int _energyCellDropMax = 3;

    [SerializeField] [Range(1, 100)] private int _warpCoilDropChance = 15;
    [SerializeField] [Min(0)] private int _warpCoilDropMin = 1;
    [SerializeField] [Min(0)] private int _warpCoilDropMax = 1;

    [SerializeField] [Range(1, 100)] private int _plasmaAcceleratorDropChance = 15;
    [SerializeField] [Min(0)] private int _plasmaAcceleratorDropMin = 1;
    [SerializeField] [Min(0)] private int _plasmaAcceleratorDropMax = 1;

    [SerializeField] [Range(1, 100)] private int _cannonAlloyDropChance = 15;
    [SerializeField] [Min(0)] private int _cannonAlloyDropMin = 1;
    [SerializeField] [Min(0)] private int _cannonAlloyDropMax = 1;


    //Monobehaviors
    //...


    //Utilities
    public void DropItemsToPlayerInventory()
    {
        if (_scrapDropChance >= RollD100())
            PlayerInventoryManager.Instance.IncrementItemCount(0, Random.Range(_scrapDropMin, _scrapDropMax + 1));

        if (_energyCellDropChance >= RollD100())
            PlayerInventoryManager.Instance.IncrementItemCount(1, Random.Range(_energyCellDropMin, _energyCellDropMax + 1));

        if (_warpCoilDropChance >= RollD100())
            PlayerInventoryManager.Instance.IncrementItemCount(2, Random.Range(_warpCoilDropMin, _warpCoilDropMax + 1));

        if (_plasmaAcceleratorDropChance >= RollD100())
            PlayerInventoryManager.Instance.IncrementItemCount(3, Random.Range(_plasmaAcceleratorDropMin, _plasmaAcceleratorDropMax + 1));

        if (_cannonAlloyDropChance >= RollD100())
            PlayerInventoryManager.Instance.IncrementItemCount(4, Random.Range(_cannonAlloyDropMin, _cannonAlloyDropMax + 1));
    }

    public void DropScrapToPlayerInventory(int min, int max)
    {
        PlayerInventoryManager.Instance.IncrementItemCount(0, Random.Range(min, max + 1));
    }

    private int RollD100()
    {
        return Random.Range(1, 101);
    }

    public void IncreaseDropRate(int chance)
    {
        _energyCellDropChance += chance;
        _warpCoilDropChance += chance;
        _plasmaAcceleratorDropChance += chance;
        _cannonAlloyDropChance += chance;
    }

    public int GetDropChance()
    {
        return _warpCoilDropChance;
    }

}

