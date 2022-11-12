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

    private ShipSystemReferencer _shipSystemReferencer;


    //Monobehaviors






    //Utilities
    public ShipSystemReferencer GetSystemReferencer()
    {
        if (_shipSystemReferencer == null)
            _shipSystemReferencer = GetComponent<ShipSystemReferencer>();

        return _shipSystemReferencer;
    }




}
