using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class ScrapHarvester : MonoSingleton<ScrapHarvester>
{
    //Delcarations
    [SerializeField] private bool _isHarvestingEnabled = false;
    [SerializeField] private int _minScrapDrop = 5;
    [SerializeField] private int _maxScrapDrop = 25;


    //Monobehaviors



    //Utilites
    public void DropExtraScrapOnEnemyDeath()
    {
        if (_isHarvestingEnabled)
            CargoLootDropper.Instance.DropScrapToPlayerInventory(_minScrapDrop,_maxScrapDrop);
    }

    public void EnableHarvesting()
    {
        _isHarvestingEnabled = true;
    }

    public bool IsHarvesterEnabled()
    {
        return _isHarvestingEnabled;
    }


}
