using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineBehavior : ShipSubsystem, IEngineSubsystemBehavior
{
    //Declarations
    [Header("Engines Attributes")]
    [SerializeField][Min(0)] private float _thrustForce = 500;
    [SerializeField] [Min(0)] private float _strafeForce = 500;
    [SerializeField] [Min(0)] private float _turnSpeed = 20;

    [Header("Engine Controls")]
    [SerializeField] private float _thrustInput;
    [SerializeField] private float _strafeInput;
    [SerializeField] private float _turnInput;


    //References
    private Rigidbody2D _shipRigidbody2D;




    //Monobehaviour
    private void Update()
    {
        MoveIfEnginesEnabled();
    }




    //Interface Utils
    public void DisableEngines()
    {
        ClearEngineControls();
        DisableSubsystem();
    }

    public void EnableEngines()
    {
        EnableSubsystem();
    }

    public float GetStrafeForce()
    {
        return _strafeForce;
    }

    public float GetThrustForce()
    {
        return _thrustForce;
    }

    public float GetTurnSpeed()
    {
        return _turnSpeed;
    }

    public bool IsEngineDisabled()
    {
        return _isDisabled;
    }

    public void MoveIfEnginesEnabled()
    {
        if (_isDisabled == false)
        {
            ApplyThrustToShip();
            ApplyStrafeToShip();
            ApplyTurnToShip();
        }
    }

    public void SetParentShipAndInitializeAwakeReferences(Ship parent)
    {
        this._parentShip = parent;
        _shipRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void InitializeGameManagerDependentReferences()
    {
        //...
    }



    public void SetStrafeForce(float newValue)
    {
        if (newValue >= 0)
            _strafeForce = newValue;
    }

    public void SetStrafeInput(float newValue)
    {
        _strafeInput = newValue;
    }

    public void SetThrustInput(float newValue)
    {
        _thrustInput = newValue;
    }

    public void SetThustForce(float newValue)
    {
        if (newValue >= 0)
            _thrustForce = newValue;
    }

    public void SetTurnInput(float newValue)
    {
        _turnInput = newValue;
    }

    public void SetTurnSpeed(float newValue)
    {
        if (newValue >= 0)
            _turnSpeed = newValue;
    }



    //Utils
    private void ClearEngineControls()
    {
        _turnInput = 0;
        _thrustInput = 0;
        _strafeInput = 0;
    }

    private void ApplyThrustToShip()
    {
        _shipRigidbody2D.AddRelativeForce(new Vector2(0, _thrustInput)* _thrustForce * Time.deltaTime);
    }

    private void ApplyStrafeToShip()
    {
        _shipRigidbody2D.AddRelativeForce(new Vector2(_strafeInput, 0) * _strafeForce * Time.deltaTime);
    }

    private void ApplyTurnToShip()
    {
        //Calculate distance to turn in degrees
        float modifierRotation = _turnInput * _turnSpeed * Time.deltaTime;

        //translate current rotation from native Quaterions into Eulers
        Vector3 shipRotation = transform.eulerAngles;

        //add distance to turn to the current rotation
        shipRotation.z += modifierRotation;

        //reapply the newly-composite rotation to the ship as a Quaternion
        transform.rotation = Quaternion.Euler(shipRotation);   
    }




    //Debugging
    //...


}
