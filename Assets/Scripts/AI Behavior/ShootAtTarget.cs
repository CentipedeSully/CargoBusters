using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject _target;
    [SerializeField] private bool _isEngagingTarget = false;
    [SerializeField] private Vector3 _forwardsDirection = Vector3.up;
     
    //Cast Utilities
    [SerializeField] private LayerMask _validTargetsLayerMask;
    [SerializeField] private float _shotRangeDistance = 6;
    [SerializeField] private float _castRadius = 1;

    //debug Utils
    private float _gizmoLineDistance;

    //references
    private RaycastHit2D[] _detectedColliders;
    [SerializeField] private WeaponsSystemController _weaponsControllerRef;


    //Monobehaviors
    private void Awake()
    {
        _gizmoLineDistance = _shotRangeDistance;
    }

    private void Update()
    {
        if (_isEngagingTarget && _target != null)
            ShootIfTargetInRange();
    }

    private void OnDrawGizmos()
    {
        DrawShotRange();
    }


    //Utilities
    private void ShootIfTargetInRange()
    {
        Vector3 castDirection = transform.TransformDirection(_forwardsDirection);
        _detectedColliders = Physics2D.CircleCastAll(transform.position, _castRadius, castDirection, _shotRangeDistance, _validTargetsLayerMask);

        if (_detectedColliders.Length > 1)
        {
            Debug.Log(_detectedColliders[1].collider.name);
            //First, set the gizmo draw distance to whatever got hit (our own collider doesn't count). Only Draw To the SECOND HIT COLLIDER, the one after US.
            if (_detectedColliders[1].collider.gameObject.GetComponent<ShipInformation>().GetShipID() != transform.parent.GetComponent<ShipInformation>().GetShipID())
                _gizmoLineDistance = _detectedColliders[1].distance;

            //if the second detected collider's ship ID equals the targets ship ID, then shoot at it
            if (_detectedColliders[1].collider.gameObject.GetComponent<ShipInformation>().GetShipID() == _target.GetComponent<ShipInformation>().GetShipID())
                _weaponsControllerRef.SetShotCommand(true);
            else _weaponsControllerRef.SetShotCommand(false);
        }

        else
        {
            _gizmoLineDistance = _shotRangeDistance;
            _weaponsControllerRef.SetShotCommand(false);
        }
    }

    private void DrawShotRange()
    {
        //Setup
        Gizmos.color = Color.magenta;

        //Draw to any hits, or just draw the max range
        Gizmos.DrawLine( transform.position, transform.position + transform.TransformDirection(new Vector2(0,_gizmoLineDistance)));
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(new Vector2(0, _gizmoLineDistance)), _castRadius);
    }


    //External Control Utils
    public void DisengageTarget()
    {
        _isEngagingTarget = false;
        _weaponsControllerRef.SetShotCommand(false);

        _target = null;
    }

    public void EngageTarget(GameObject target)
    {
        if (target != null)
        {
            _target = target;
            _isEngagingTarget = true;
        }
    }

    //Getters & Setters
    public bool IsEngagingTarget()
    {
        return _isEngagingTarget;
    }

    public GameObject GetCurrentTarget()
    {
        return _target;
    }
}

