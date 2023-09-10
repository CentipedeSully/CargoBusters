using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class OldSpawnLaserOnInput : MonoBehaviour
{
    [SerializeField] private GameObject _activeLaserContainer;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _laserSpawnPosition;
    [SerializeField] private GameObject _createdLaser;
    [SerializeField] private float _angularSpread = 2;

    private bool _shootInput = false;
    private bool _isShotReady = true;

    [SerializeField] private float _shotCooldownDuration = .5f;
    [SerializeField] private float _laserPushForce = 5;
    [SerializeField] private int _laserDamage = 1;

    [SerializeField] private FlashBarrelOnFire _barrelFlashScriptRef;

    private void Awake()
    {
        _activeLaserContainer = GameObject.Find("Lasers Container");
    }


    private void Update()
    {
        ShootLaser();
        
    }

    private void ShootLaser()
    {
        if (_isShotReady && _shootInput == true)
        { 
            GetLaserFromObjectPoolerToSpawnLocation();

            //Trigger flash
            _barrelFlashScriptRef.TriggerFlash();

            CooldownShot();
        }
    }

    private void GetLaserFromObjectPoolerToSpawnLocation()
    {
        //Get Laser
        _createdLaser = ObjectPooler.TakePooledGameObject(_laserPrefab, _activeLaserContainer.transform);

        //Reposition Laser
        _createdLaser.transform.SetPositionAndRotation(_laserSpawnPosition.transform.position, Quaternion.Euler(transform.rotation.eulerAngles));

        //Apply Randomized Spread
        _createdLaser.transform.Rotate(0, 0, Random.Range(-_angularSpread/2, _angularSpread/2));

        //Set the laser's shooterID to this object's ID
        _createdLaser.GetComponent<LaserBehavior>().SetShooterID(gameObject.GetInstanceID());

        //Set the laser's push force
        _createdLaser.GetComponent<LaserBehavior>().SetPushForce(_laserPushForce);

        //Set laser damage
        _createdLaser.GetComponent<LaserBehavior>().SetDamage(_laserDamage);

        //Apply Speed Offset by the player's yMove velocity
        //_createdLaser.gameObject.GetComponent<LaserBehavior>().SetSpeedOffset(CalculateLaserSpeedOffset());

        //Enable Laser Behavior
        _createdLaser.SetActive(true);
        _createdLaser.GetComponent<LaserBehavior>().EnableLaserBehavior();

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


    private float CalculateLaserSpeedOffset()
    {
        
        Vector2 relativeVelocity = transform.InverseTransformVector(GetComponent<Rigidbody2D>().GetRelativePointVelocity(Vector2.zero));
        float offset = relativeVelocity.y;
        

        return offset;
    }

}
