using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class SpawnLaserOnInput : MonoBehaviour
{
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _laserSpawnPosition;
    private GameObject _createdLaser;

    private bool _shootInput = false;
    private bool _isShotReady = true;
    private float _shotCooldownDuration = .5f;





    private void Update()
    {
        ShootLaser();
    }

    private void ShootLaser()
    {
        if (_isShotReady && _shootInput == true)
        { 
            GetLaserFromObjectPoolerToSpawnLocation();
            CooldownShot();
        }
    }

    private void GetLaserFromObjectPoolerToSpawnLocation()
    {
        //Get Laser
        _createdLaser = ObjectPooler.TakePooledGameObject(_laserPrefab);

        //Reposition Laser
        _createdLaser.transform.SetPositionAndRotation(_laserSpawnPosition.transform.position, Quaternion.Euler(transform.rotation.eulerAngles));
    }

    private void CooldownShot()
    {
        _isShotReady = false;
        Invoke("EnableShooting", _shotCooldownDuration);
    }

    private void EnableShooting()
    {
        _isShotReady = true;
    }

    public void SetShootInput(bool newValue)
    {
        _shootInput = newValue;
    }


}
