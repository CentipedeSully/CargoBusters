using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDisabler : MonoBehaviour
{
    //Declarations     
    private bool _shieldsExist = false;

    private ShipInformation _shipInfoRef;
    private ShipSystemReferencer _systemReferencer;
    private ShieldsSystemController _shieldsControllerRef;
    private EnginesSystemController _enginesSystemControllerRef;
    private WeaponsSystemController _weaponsSystemControllerRef;


    //Monos
    private void Awake()
    {
        InitializeReferences();
    }


    //Utilites
    private void InitializeReferences()
    {
        _shipInfoRef = GetComponent<ShipInformation>();
        _systemReferencer = GetComponent<ShipSystemReferencer>();

        if (_systemReferencer.GetShieldsObject() != null)
        {
            _shieldsExist = true;
            _shieldsControllerRef = _systemReferencer.GetShieldsObject().GetComponent<ShieldsSystemController>();
        }

        _enginesSystemControllerRef = _systemReferencer.GetEnginesObject().GetComponent<EnginesSystemController>();
        _weaponsSystemControllerRef = _systemReferencer.GetWeaponsObject().GetComponent<WeaponsSystemController>();
    }

    public void DisableAllSystems()
    {
        _shipInfoRef.SetShipDisabled(true);
        _weaponsSystemControllerRef.DisableWeapons();
        _enginesSystemControllerRef.DisableEngines();
        if (_shieldsExist)
            _shieldsControllerRef.DisableShields();
    }

    public void EnablesAllSystems()
    {
        _shipInfoRef.SetShipDisabled(false);
        _weaponsSystemControllerRef.EnableWeapons();
        _enginesSystemControllerRef.EnableEngines();
        if (_shieldsExist)
            _shieldsControllerRef.EnableShields();
    }
}
