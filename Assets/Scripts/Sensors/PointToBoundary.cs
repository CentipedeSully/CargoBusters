using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToBoundary : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _BoundaryObjName = "Arena Boundary Limiter";
    private GameObject _boundaryOriginRef;
    [SerializeField] private GameObject _pointerObject;
    [SerializeField] bool _isPointerVisible = false;
    [SerializeField] bool _isThisShipPlayer = false;

    //Monobehaviors
    private void Awake()
    {
        _isThisShipPlayer = transform.parent.parent.GetComponent<ShipInformation>().IsPlayer();
        _boundaryOriginRef = GameObject.Find(_BoundaryObjName);
    }

    private void Start()
    {
        HidePointer();
    }

    private void Update()
    {
        DisplayPointer();
    }

    //Utilites
    private void DisplayPointer()
    {
        if (_isPointerVisible && _isThisShipPlayer)
        {
            //realposition of origin * .1
            _pointerObject.transform.localPosition = transform.InverseTransformPoint(_boundaryOriginRef.transform.position) * .1f;
        }
    }

    public void ShowPointer()
    {
        if (_isThisShipPlayer)
        {
            _isPointerVisible = true;
            _pointerObject.SetActive(true);
        }
    }

    public void HidePointer()
    {
        _isPointerVisible = false;
        _pointerObject.SetActive(false);
    }

}
