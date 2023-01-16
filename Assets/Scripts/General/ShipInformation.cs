using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInformation : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _shipName = "Unnamed Ship";
    [SerializeField] private string _faction = "None";
    [SerializeField] private bool _isPlayer = false;
    [SerializeField] private bool _isDisabled = false;
    [SerializeField] private int _shipID;

    private ShipSystemReferencer _shipSystemReferencer;


    //Monobehaviors
    private void Awake()
    {
        _shipID = GetInstanceID();
    }





    //Utilities
    public ShipSystemReferencer GetSystemReferencer()
    {
        if (_shipSystemReferencer == null)
            _shipSystemReferencer = GetComponent<ShipSystemReferencer>();

        return _shipSystemReferencer;
    }

    public bool IsDisabled()
    {
        return _isDisabled;
    }

    public bool IsPlayer()
    {
        return _isPlayer;
    }

    public void SetShipDisabled(bool value)
    {
        _isDisabled = value;
    }

    public void SetPlayerFlag(bool value)
    {
        _isPlayer = value;
    }

    public int GetShipID()
    {
        return _shipID;
    }

    public string GetShipName()
    {
        return _shipName;
    }

    public void SetShipName(string name)
    {
        _shipName = name;
    }

    public string GetFaction()
    {
        return _faction;
    }

    public void SetFaction(string newFaction)
    {
        _faction = newFaction;
    }
}
