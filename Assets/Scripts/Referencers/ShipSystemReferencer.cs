using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystemReferencer : MonoBehaviour
{
    //Declarations
    private ShipInformation _shipInfoRef;
    private CompositeCollider2D _compositeColliderRef;
    private Rigidbody2D _rigidbodyRef;

    [Tooltip("This is used onAwake to find the gameObject in question and to fill its reference")]
    [SerializeField] private string _weaponsObjectName = "Weapon Systems";
    private GameObject _weaponsSystems;

    [Tooltip("This is used onAwake to find the gameObject in question and to fill its reference")]
    [SerializeField] private string _shieldsObjectName = "Shield Systems";
    private GameObject _shieldsSystems;


    [Tooltip("This is used onAwake to find the gameObject in question and to fill its reference")]
    [SerializeField] private string _enginesObjectName = "Engine Systems";
    private GameObject _enginesSystems;
 

    [Tooltip("This is used onAwake to find the gameObject in question and to fill its reference")]
    [SerializeField] private string _hullObjectName = "Hull Systems";
    private GameObject _hullSystems;
 

    [Tooltip("This is used onAwake to find the gameObject in question and to fill its reference")]
    [SerializeField] private string _crewObjectName = "Crew Systems";
    private GameObject _crewSystems;


    [Tooltip("This is used onAwake to find the gameObject in question and to fill its reference")]
    [SerializeField] private string _auxSystemsObjectName = "Auxillary Systems";
    private GameObject _auxillarySystems;







    //Monobehaviors
    private void Awake()
    {
        FillReferences();
    }





    //Utilities
    public ShipInformation GetShipInfo()
    {
        return _shipInfoRef;
    }

    public CompositeCollider2D GetCompositeCollider2D()
    {
        return _compositeColliderRef;
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return _rigidbodyRef;
    }


    public GameObject GetWeaponsObject()
    {
        return _weaponsSystems;
    }

    public GameObject GetShieldsObject()
    {
        return _shieldsSystems;
    }

    public GameObject GetEnginesObject()
    {
        return _enginesSystems;
    }

    public GameObject GetHullObject()
    {
        return _hullSystems;
    }

    public GameObject GetCrewObject()
    {
        return _crewSystems;
    }

    public GameObject GetAuxillaryObject()
    {
        return _auxillarySystems;
    }


    private void FillReferences()
    {
        _shipInfoRef = GetComponent<ShipInformation>();
        _rigidbodyRef = GetComponent<Rigidbody2D>();
        _compositeColliderRef = GetComponent<CompositeCollider2D>();

        _weaponsSystems = transform.Find(_weaponsObjectName).gameObject;
        _shieldsSystems = transform.Find(_shieldsObjectName).gameObject;
        _enginesSystems = transform.Find(_enginesObjectName).gameObject;
        _hullSystems = transform.Find(_hullObjectName).gameObject;
        _crewSystems = transform.Find(_crewObjectName).gameObject;
        _auxillarySystems = transform.Find(_auxSystemsObjectName).gameObject;
    }

}
