using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class SpawnLaserOnInput : MonoBehaviour
{
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _laserSpawnPosition;
    [SerializeField] private GameObject _createdLaser;

    private bool _shootInput = false;
    private bool _isShotReady = true;
    [SerializeField] private float _shotCooldownDuration = .5f;





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



        //Apply Speed Offset by the player's yMove velocity
        _createdLaser.gameObject.GetComponent<LaserBehavior>().SetSpeedOffset(CalculateLaserSpeedOffset());
        

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
        
        //Debug.Log($"Relative velocity: {relativeVelocity}, Y Offset: {offset}");

        return offset;
    }

}
