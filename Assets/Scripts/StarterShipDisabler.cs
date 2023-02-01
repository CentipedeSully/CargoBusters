using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterShipDisabler : MonoBehaviour
{
    [SerializeField] private DamageVisualController _damageVisualsRef;

    void Start()
    {
        GetComponent<SystemDisabler>().DisableSystemsWithoutReportingDeath();

        _damageVisualsRef.ShowMaxDamageVisuals();
    }

}
