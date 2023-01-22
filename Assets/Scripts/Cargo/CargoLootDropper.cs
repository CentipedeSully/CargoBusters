using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoLootDropper : MonoBehaviour
{
    //Declarations
    [Tooltip("0 == Scrap, 1 == EnergyCells, 2 == WarpCoils, 3 == PlasmaAccelerators, 4 == CannonAlloys")]
    [SerializeField][Range(1,100)] private int _scrapDropChance = 100;
    [SerializeField] [Min (0)] private int _scrapDropMin = 15;
    [SerializeField] [Min (0)] private int _scrapDropMax = 35;

    [SerializeField] [Range(1, 100)] private int _energyCellDropChance = 25;
    [SerializeField] [Min(0)] private int _energyCellDropMin = 1;
    [SerializeField] [Min(0)] private int _energyCellDropMax = 3;

    [SerializeField] [Range(1, 100)] private int _warpCoilDropChance = 10;
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

    private int RollD100()
    {
        return Random.Range(1, 101);
    }



}

