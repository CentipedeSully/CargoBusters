using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PointToEnemy : MonoBehaviour
{
    //Declarations
    private bool _isPointerEnabled = false;
    private bool _isTargetWithinSensorRange = false;
    [SerializeField] private float _tooCloseDistanceThreshold = 5;
    [SerializeField] private float _tooFarDistanceThreshold = 60;

    //references
    [SerializeField] private GameObject _notVulnerableEnemyDot;
    [SerializeField] private GameObject _vulnerableEnemyDot;
    private GameObject _enemyShip;
    private GameObject _playerShip;


    //Monobheaviors
    private void Start()
    {
        _playerShip = PlayerObjectManager.Instance.GetPlayerObject();
    }

    private void Update()
    {
        if (_isPointerEnabled)
            ShowDotIfShipWithinRange();
    }


    //Utilities
    private void ShowDotIfShipWithinRange()
    {
        if (_enemyShip == null)
            Destroy(gameObject);
        else if (_enemyShip.GetComponent<ShipSystemReferencer>().GetCargoObject().GetComponent<CargoSystemController>().IsCargoBusted() == true)
            Destroy(gameObject);
        else
        {
            float enemyDistance = Mathf.Abs(Vector3.Distance(_enemyShip.transform.position, _playerShip.transform.position));
            bool isEnemyCargoSecurityOnline = _enemyShip.GetComponent<ShipSystemReferencer>().GetCargoObject().GetComponent<CargoSystemController>().IsCargoSecuritySystemOnline();
            CalculateDotDistances();

            if (enemyDistance > _tooCloseDistanceThreshold && enemyDistance < _tooFarDistanceThreshold)
            {
                _isTargetWithinSensorRange = true;
                if (isEnemyCargoSecurityOnline == false)
                    ChangeDotToVulnerable();
                else ChangeDotToNotVulnerable();
            }

            else
            {
                _isTargetWithinSensorRange = false;
                HideBothDots();
            }
                
        }
    }

    private void ChangeDotToVulnerable()
    {
        _notVulnerableEnemyDot.SetActive(false);
        _vulnerableEnemyDot.SetActive(true);
    }

    private void ChangeDotToNotVulnerable()
    {
        _notVulnerableEnemyDot.SetActive(true);
        _vulnerableEnemyDot.SetActive(false);
    }

    private void HideBothDots()
    {
        _notVulnerableEnemyDot.SetActive(false);
        _vulnerableEnemyDot.SetActive(false);
    }

    private void CalculateDotDistances()
    {
        _notVulnerableEnemyDot.transform.localPosition = transform.InverseTransformPoint(_enemyShip.transform.position) * .1f;
        _vulnerableEnemyDot.transform.localPosition = transform.InverseTransformPoint(_enemyShip.transform.position) * .1f;
    }


    //External Control Utils
    public void SetEnemyTarget(GameObject enemyShipObject)
    {
        _enemyShip = enemyShipObject;
    }

    public void EnablePointer()
    {
        _isPointerEnabled = true;
    }

    public void DisablePointer()
    {
        _isPointerEnabled = false;
    }

    public bool IsTargetWithinSensorRange()
    {
        return _isTargetWithinSensorRange;
    }
}
