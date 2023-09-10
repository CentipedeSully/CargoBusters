using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportDeath : MonoBehaviour
{
    //declarations
    private bool _isDeathAlreadyReported = false;
    [SerializeField] private int _timesReported = 0;

    //monos


    //utils
    public void ReportShipDeath()
    {
        _timesReported++;
        if (_isDeathAlreadyReported == false)
        {
            if (transform.parent.GetComponent<ShipInformation>().IsPlayer())
                PlayerObjectManager.Instance.ReportPlayerDeath();

            else
            {
                SpawnController.Instance.ReportEnemyDeath();
                ScrapHarvester.Instance.DropExtraScrapOnEnemyDeath();
            }
                

            _isDeathAlreadyReported = true;
        }
    }
}
