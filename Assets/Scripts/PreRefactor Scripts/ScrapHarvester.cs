using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class ScrapHarvester : MonoSingleton<ScrapHarvester>
{
    //Delcarations
    [SerializeField] private bool _isHarvestingEnabled = false;
    [SerializeField] private int _minScrapDrop = 15;
    [SerializeField] private int _maxScrapDrop = 35;


    //Monobehaviors



    //Utilites
    public void DropExtraScrapOnEnemyDeath()
    {
        if (_isHarvestingEnabled)
        {
            int scrapMultiplier = SpawnController.Instance.GetRoundCount() + 1;
            CargoLootDropper.Instance.DropScrapToPlayerInventory(_minScrapDrop * scrapMultiplier, _maxScrapDrop * scrapMultiplier);
        }
            
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
