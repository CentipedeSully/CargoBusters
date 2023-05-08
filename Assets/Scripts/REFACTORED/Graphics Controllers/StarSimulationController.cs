using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSimulationController : MonoBehaviour
{
    //Declarations
    [SerializeField] [Min(0)] private float _starSpeedMultiplier = .5f;
    [SerializeField] private Rigidbody2D _followRigidBody;
    [SerializeField] private Vector2 _particleVelocity;


    //references
    private ParticleSystem.VelocityOverLifetimeModule _cachedVelocityModule;
    private AnimationCurve _cachedVelocityCurve;
    private ParticleSystem.MinMaxCurve _cachedMinMaxCurve;



    //Monobehaviors
    private void Start()
    {
        SetupSimulationUtils();
    }

    private void Update()
    {
        SimulateParticleMovementOnRigidbody();
    }



    //Utils
    private void SetupSimulationUtils()
    {
        //Fill referernce
        _cachedVelocityModule = GetComponent<ParticleSystem>().velocityOverLifetime;

        //enable the velocity over lifetime subcomponent of the particle system  
        _cachedVelocityModule.enabled = true;

        //set space to world
        _cachedVelocityModule.space = ParticleSystemSimulationSpace.World;


        //setup a curve for the velocity module to follow. It'll be the same for x,y and z
        _cachedVelocityCurve = new AnimationCurve();

        //The speed will be constant throughout the particle's lifetime
        _cachedVelocityCurve.AddKey(0, 1);
        _cachedVelocityCurve.AddKey(1, 1);


        //Create a special MinMax Curve from our animation curve. Used exclusively by the VelocityModule
        _cachedMinMaxCurve = new ParticleSystem.MinMaxCurve(1,_cachedVelocityCurve);

        //apply the created MinMax Curve to the Velocity Module
        _cachedVelocityModule.x = _cachedMinMaxCurve;
        _cachedVelocityModule.y = _cachedMinMaxCurve;
        _cachedVelocityModule.z = _cachedMinMaxCurve;

        //Now, just edit the multipliers of each axis separately to change the particles's speeds
        // :)


    }

    private void SimulateParticleMovementOnRigidbody()
    {
        if (_followRigidBody != null)
        {
            //Calculate the simulation's particle velocity
            _particleVelocity = -1 * _followRigidBody.velocity;

            //apply the new velocity's values to the correct axes of the cached velocity module
            _cachedVelocityModule.xMultiplier = _starSpeedMultiplier * _particleVelocity.x;
            _cachedVelocityModule.yMultiplier = _starSpeedMultiplier * _particleVelocity.y;
            _cachedVelocityModule.zMultiplier = 0;

        }
    }


    //Getters & Setters


}
