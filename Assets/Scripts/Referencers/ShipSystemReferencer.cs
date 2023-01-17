using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystemReferencer : MonoBehaviour
{
    //Declarations
    private ShipInformation _shipInfoRef;
    private CompositeCollider2D _compositeColliderRef;
    private Rigidbody2D _rigidbodyRef;

    [SerializeField] private GameObject _weaponsSystems;
    [SerializeField] private GameObject _shieldsSystems;
    [SerializeField] private GameObject _enginesSystems;
    [SerializeField] private GameObject _hullSystems;
    [SerializeField] private GameObject _crewSystems;
    [SerializeField] private GameObject _auxillarySystems;
    [SerializeField] private GameObject _warpCoreSystems;


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

    public GameObject GetWarpCoreObject()
    {
        return _warpCoreSystems;
    }


    private void FillReferences()
    {
        _shipInfoRef = GetComponent<ShipInformation>();
        _rigidbodyRef = GetComponent<Rigidbody2D>();
        _compositeColliderRef = GetComponent<CompositeCollider2D>();
    }

}
