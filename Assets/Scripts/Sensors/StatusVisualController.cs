using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusVisualController : MonoBehaviour
{
    //Declarations
    [SerializeField] private StatusVisualizer _healthPointVisualizer;
    [SerializeField] private StatusVisualizer _shieldPointVisualizer;
    [SerializeField] private StatusVisualizer _vulnerablePointVisualizer;

    private GameObject _targetShip;
    private GameObject _playerShip;
    private float _distanceFromPlayer;
    [SerializeField] private float _visibleDistanceThreshold = 20;
    private bool _isStatusVisible = false;
    private bool _isTargetDisabled = false;

    private ShipInformation _shipInfo;
    private IntegrityBehavior _hullIntegrityRef;
    private IntegrityBehavior _shieldIntegrityRef;


    //Monobehaviors
    private void Update()
    {
        if (_targetShip != null && _playerShip != null)
        {
            CalculateDistanceFromPlayer();
            ToggleVisiblilityIfPlayerInRange();
            if (_isStatusVisible)
                UpdateVisuals();
            else HideVisibility();
        }
    }


    //Utilites
    public void SetPlayerReference()
    {
        if (_playerShip == null)
            _playerShip = PlayerObjectManager.Instance.GetPlayerObject();
    }

    private void CalculateDistanceFromPlayer()
    {
        _distanceFromPlayer = Mathf.Abs(Vector3.Distance(_playerShip.transform.position, _targetShip.transform.position));
    }

    private void ToggleVisiblilityIfPlayerInRange()
    {
        if (_isStatusVisible && _distanceFromPlayer > _visibleDistanceThreshold)
            _isStatusVisible = false;

        else if (_isStatusVisible == false && _distanceFromPlayer <= _visibleDistanceThreshold)
            _isStatusVisible = true;
            
    }

    private void HideVisibility()
    {
        _healthPointVisualizer.SetToValue(0);
        _shieldPointVisualizer.SetToValue(0);
        _vulnerablePointVisualizer.SetToValue(0);
    }

    private void UpdateVisuals()
    {
        _shieldPointVisualizer.SetToValue((int)_shieldIntegrityRef.GetCurrentIntegrity());
        if (_shipInfo.IsDisabled() == true && _isTargetDisabled == false)
        {
            _isTargetDisabled = true;
            _vulnerablePointVisualizer.SetToValue((int)_hullIntegrityRef.GetCurrentIntegrity());
            _healthPointVisualizer.SetToValue(0);
        }
        else if (_shipInfo.IsDisabled() == false && _isTargetDisabled == true)
        {
            _isTargetDisabled = false;
            _vulnerablePointVisualizer.SetToValue(0);
            _healthPointVisualizer.SetToValue((int)_hullIntegrityRef.GetCurrentIntegrity());
        }
        else if (_isTargetDisabled)
            _vulnerablePointVisualizer.SetToValue((int)_hullIntegrityRef.GetCurrentIntegrity());

        else
            _healthPointVisualizer.SetToValue((int)_hullIntegrityRef.GetCurrentIntegrity());

    }

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget.GetComponent<ShipInformation>() != null)
        {
            _targetShip = newTarget;

            _shipInfo = _targetShip.GetComponent<ShipInformation>();
            _shieldIntegrityRef = _targetShip.GetComponent<ShipSystemReferencer>().GetShieldsObject().GetComponent<IntegrityBehavior>();
            _hullIntegrityRef = _targetShip.GetComponent<ShipSystemReferencer>().GetHullObject().GetComponent<IntegrityBehavior>();

            if (PlayerObjectManager.Instance.GetPlayerObject() != null)
                _playerShip = PlayerObjectManager.Instance.GetPlayerObject();
        }
    }


}
