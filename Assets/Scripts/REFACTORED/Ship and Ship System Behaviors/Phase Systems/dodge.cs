using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dodge : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _TriggerDodgeViaInspector = false;
    [SerializeField] private float _cooldownDuration = .5f;
    [SerializeField] private bool _isDodgeReady = true;
    [SerializeField] private bool _isDodging = false;
    [SerializeField] private AnimationCurve _boostPowerCurve;

    [SerializeField] private float _currentDodgeDuration;
    [SerializeField] private float _maxDodgeDuration = 1;
    [SerializeField] private float _originalEngineSpeed;
    [SerializeField] private float _boostSpeed;

    private Rigidbody2D _rigidbody2DRef;
    private EngineBehavior _enginesRef;

    //Monobehaviours
    private void Awake()
    {
        _rigidbody2DRef = GetComponent<Rigidbody2D>();
        _enginesRef = GetComponent<EngineBehavior>();
        _originalEngineSpeed = _enginesRef.GetThrustForce();
        _maxDodgeDuration = _boostPowerCurve.keys[_boostPowerCurve.length - 1].time;
    }

    private void Update()
    {
        if (_TriggerDodgeViaInspector)
        {
            _TriggerDodgeViaInspector = false;
            Dodge();
        }
            

        if (_isDodging)
        {
            _boostSpeed = _boostPowerCurve.Evaluate(_currentDodgeDuration);
            _enginesRef.SetThustForce(_originalEngineSpeed + _boostSpeed);

            _currentDodgeDuration += Time.deltaTime;

            if (_currentDodgeDuration >= _maxDodgeDuration)
            {
                _currentDodgeDuration = 0;
                _isDodging = false;
                _enginesRef.SetThustForce(_originalEngineSpeed);
            }
        }
    }


    //Utils
    public void Dodge()
    {
        if (_isDodgeReady && !_isDodging)
        {
            //Setup Dodge Utils
            _isDodging = true;
            _isDodgeReady = false;


            //== Cooldown Dodge
            Invoke("ReadyDodge", _cooldownDuration);
        }
    }

    private void ReadyDodge()
    {
        _isDodgeReady = true;
    }




}
