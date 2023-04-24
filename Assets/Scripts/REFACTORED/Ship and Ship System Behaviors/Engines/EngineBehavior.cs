using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineBehavior : ShipSubsystem, IEngineBehavior
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
    private void Awake()
    {
        InitializeReferences();
    }

    private void Update()
    {
        MoveIfEnginesEnabled();
    }


    //Interface Utils

    void IEngineBehavior.DisableEngines()
    {
        ClearEngineControls();
        DisableSubsystem();
    }

    void IEngineBehavior.EnableEngines()
    {
        EnableSubsystem();
    }

    float IEngineBehavior.GetStrafeForce()
    {
        return _strafeForce;
    }

    float IEngineBehavior.GetThrustForce()
    {
        return _thrustForce;
    }

    float IEngineBehavior.GetTurnSpeed()
    {
        return _turnSpeed;
    }

    bool IEngineBehavior.IsDebugActive()
    {
        return _showDebug;
    }

    bool IEngineBehavior.IsEngineDisabled()
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

    void IEngineBehavior.SetParent(Ship parent)
    {
        this._parentShip = parent;
    }

    void IEngineBehavior.SetStrafeForce(float newValue)
    {
        if (newValue >= 0)
            _strafeForce = newValue;
    }

    void IEngineBehavior.SetStrafeInput(float newValue)
    {
        _strafeInput = newValue;
    }

    void IEngineBehavior.SetThrustInput(float newValue)
    {
        _thrustInput = newValue;
    }

    void IEngineBehavior.SetThustForce(float newValue)
    {
        if (newValue >= 0)
            _thrustForce = newValue;
    }

    void IEngineBehavior.SetTurnInput(float newValue)
    {
        _turnInput = newValue;
    }

    void IEngineBehavior.SetTurnSpeed(float newValue)
    {
        if (newValue >= 0)
            _turnSpeed = newValue;
    }

    void IEngineBehavior.ToggleDebugMode()
    {
        if (_showDebug)
            _showDebug = false;
        else _showDebug = true;
    }




    //Utils
    private void InitializeReferences()
    {
        _shipRigidbody2D = GetComponent<Rigidbody2D>();
    }

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



}
