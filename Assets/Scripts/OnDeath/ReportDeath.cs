using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportDeath : MonoBehaviour
{
    //declarations
    private bool _isDeathAlreadyReported = false;

    //monos


    //utils
    public void ReportShipDeath()
    {
        if (_isDeathAlreadyReported == false)
        {
            if (transform.parent.GetComponent<ShipInformation>().IsPlayer())
                PlayerObjectManager.Instance.ReportPlayerDeath();

            else
                SpawnController.Instance.ReportEnemyDeath();

            _isDeathAlreadyReported = true;
        }
    }
}
