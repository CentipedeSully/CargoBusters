using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnLaserOnCommand : MonoBehaviour
{
    //Declarations
    [SerializeField] private string _laserContainerName = "Laser Container";
    private GameObject _laserContainerObj;
    [SerializeField] private GameObject _laserPrefab;
    private GameObject _createdLaser;
    [SerializeField] private float _angularSpread = 2;

    [SerializeField] private bool _shootInput = false;
    [SerializeField] private bool _isShotReady = true;

    [SerializeField] private float _shotCooldownDuration = .5f;
    [SerializeField] private float _laserPushForce = 5;
    [SerializeField] private float _laserDamage = 1;

    [Header("Events")]
    public UnityEvent OnFire;

    //Monobehaviors
    private void Awake()
    {
        _laserContainerObj = GameObject.Find(_laserContainerName);
    }


    private void Update()
    {
        ShootLaser();

    }



    //Utilities
    private void ShootLaser()
    {
        if (_isShotReady && _shootInput == true)
        {
            SpawnAndSetupLaser();

            OnFire?.Invoke();

            CooldownShot();
        }
    }

    private void SpawnAndSetupLaser()
    {
        //Create Laser
        _createdLaser = Instantiate(_laserPrefab, transform.position, Quaternion.Euler(transform.rotation.eulerAngles), _laserContainerObj.transform);

        //Apply Randomized Spread
        _createdLaser.transform.Rotate(0, 0, Random.Range(-_angularSpread / 2, _angularSpread / 2));

        //Set the laser's shooterID to this object's ID
        _createdLaser.GetComponent<LaserBehavior>().SetShooterID(transform.parent.parent.GetComponent<ShipInformation>().GetShipID());

        //Set the laser's push force
        _createdLaser.GetComponent<LaserBehavior>().SetPushForce(_laserPushForce);

        //Set laser damage
        _createdLaser.GetComponent<LaserBehavior>().SetDamage(_laserDamage);

        //Apply Speed Offset by the player's yMove velocity
        _createdLaser.GetComponent<LaserBehavior>().SetSpeedOffset(CalculateLaserSpeedOffset());

        //Enable Laser Behavior
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


    private Vector2 CalculateLaserSpeedOffset()
    {

        Vector2 relativeVelocity = transform.parent.parent.InverseTransformVector(transform.parent.parent.GetComponent<Rigidbody2D>().GetRelativePointVelocity(Vector2.zero));
        return relativeVelocity;


        //return offset;
    }

}
