using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class OldShieldBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] float _shieldIntegrity = 0;
    [SerializeField] float _maxShieldIntegrity = 5;
    [SerializeField] float _regenDelay = 5;
    [SerializeField] int _regenRate = 1;
    [SerializeField] bool _shieldsReadyOnStart = false;
    [SerializeField] bool _isShieldsDisabled = false;

    [SerializeField] private UniversalStateMachine _shieldsStateMachine;

    [SerializeField] private Regenerator _shieldRegeneratorRef;
    [SerializeField] private Timer _shieldTimerRef;
    [SerializeField] private ShieldVFXController _vfxShieldReference;
    [SerializeField] private OldDamageHandler _damageHandlerRef;


    //Monobehaviors
    private void Awake()
    {
        _shieldTimerRef = GetComponent<Timer>();
        _shieldRegeneratorRef = GetComponent<Regenerator>();
        _shieldsStateMachine = GetComponent<UniversalStateMachine>();



        _shieldRegeneratorRef.SetRegenRate(_regenRate);
        _shieldTimerRef.SetDuration(_regenDelay);
    }

    private void OnEnable()
    {
        //_shieldRegeneratorRef.OnRegenerationTick.AddListener(IncreaseIntegrity);
        _damageHandlerRef.OnShieldDamaged.AddListener(DecreaseIntegrity);
        _damageHandlerRef.OnHealthDamaged.AddListener(InterruptRegenDelay);
        _damageHandlerRef.OnHealthDamaged.AddListener(ForceShieldDeplete);
    }

    private void OnDisable()
    {
        //_shieldRegeneratorRef.OnRegenerationTick.RemoveListener(IncreaseIntegrity);
        _damageHandlerRef.OnShieldDamaged.RemoveListener(DecreaseIntegrity);
        _damageHandlerRef.OnHealthDamaged.RemoveListener(InterruptRegenDelay);
        _damageHandlerRef.OnHealthDamaged.RemoveListener(ForceShieldDeplete);
    }

    private void Start()
    {
        SetupShieldsOnStart();
        InitializeStates();
            
    }




    //Utilities
    public void SetShieldIntegrity(float value)
    {
        _shieldIntegrity = Mathf.Clamp(value, 0, _maxShieldIntegrity);
    }

    public void SetMaxIntegrity(float value)
    {
        if (value >= 0)
        {
            _maxShieldIntegrity = value;

            if (_shieldIntegrity > _maxShieldIntegrity)
                _shieldIntegrity = _maxShieldIntegrity;
        }
    }

    public void SetRegenDelay(float value)
    {
        if (value > 0)
        {
            _regenDelay = value;
            _shieldTimerRef.SetDuration(value);
        }
            
    }

    public void SetRegenRate(int value)
    {
        if (value >= 0)
        {
            _regenRate = value;
            _shieldRegeneratorRef.SetRegenRate(value);
        }
            
    }


    public float GetShieldIntegrity()
    {
        return _shieldIntegrity;
    }

    public float GetMaxIntegrity()
    {
        return _maxShieldIntegrity;
    }

    public float GetRegenDelay()
    {
        return _regenDelay;
    }

    public float GetRegenRate()
    {
        return _regenRate;
    }


    public void IncreaseIntegrity(float value)
    {
        if (value > 0 && _isShieldsDisabled == false)
        {
            SetShieldIntegrity(_shieldIntegrity + value);

            if (_shieldsStateMachine.GetStateActivity("isDepleted"))
            {
                _shieldsStateMachine.UpdateStateActivity("isDepleted", false);
                _shieldsStateMachine.UpdateStateActivity("isAvailable", true);

                _vfxShieldReference.PlayShieldRestoreAnim();
            }

            if (_shieldIntegrity == _maxShieldIntegrity)
                _shieldRegeneratorRef.StopRegen();
        }
            
    }

    public void DecreaseIntegrity(float value)
    {
        if (value > 0)
        {
            //reduce shield integrity
            SetShieldIntegrity(_shieldIntegrity - value);

            //stop regenerating if regenerating
            _shieldRegeneratorRef.StopRegen();

            //restart regenDelay counting
            if (!_isShieldsDisabled)
                _shieldTimerRef.RestartTimer();

            if (_shieldsStateMachine.GetStateActivity("isAvailable") && _shieldIntegrity > 0)
                _vfxShieldReference.PlayShieldDamagedAnim();
            else if (_shieldsStateMachine.GetStateActivity("isAvailable") && _shieldIntegrity == 0)
            {
                _vfxShieldReference.PlayShieldbreakAnim();
                _shieldsStateMachine.UpdateStateActivity("isDepleted", true);
                _shieldsStateMachine.UpdateStateActivity("isAvailable", false);
            }
        }
    }

    public void StartRegenerating()
    {
        _shieldRegeneratorRef.StartRegen();
    }

    public bool IsShieldDepleted()
    {
        return _shieldIntegrity == 0;
    }

    public bool IsRegenerating()
    {
        return _shieldRegeneratorRef.IsRegenerating();
    }

    public void InterruptRegenDelay()
    {
        _shieldTimerRef.RestartTimer();
    }

    public void InterruptRegenDelay(float damage)
    {
        InterruptRegenDelay();
    }

    public void ForceShieldDeplete(float damage)
    {
        _vfxShieldReference.DepleteShields();
    }

    private void InitializeStates()
    {
        _shieldsStateMachine.AddState("isDepleted");
        _shieldsStateMachine.AddState("isAvailable");

        if (_shieldIntegrity == 0)
        {
            _shieldsStateMachine.UpdateStateActivity("isDepleted", true);
            _vfxShieldReference.DepleteShields();
            _shieldTimerRef.StartOrResumeTimer();
        }

        else if (_shieldIntegrity < _maxShieldIntegrity)
        {
            _shieldsStateMachine.UpdateStateActivity("isAvailable", true);
            _shieldTimerRef.StartOrResumeTimer();
        }

        else if (_shieldIntegrity == _maxShieldIntegrity)
        {
            _shieldsStateMachine.UpdateStateActivity("isAvailable", true);
        }
    }

    public void CalculateStates()
    {
        if (_shieldIntegrity == 0)
        {
            _shieldsStateMachine.UpdateStateActivity("isDepleted", true);
            _shieldsStateMachine.UpdateStateActivity("isAvailable", false);
        }
    }

    private void SetupShieldsOnStart()
    {
        if (_shieldsReadyOnStart)
            _shieldIntegrity = _maxShieldIntegrity;
        else _shieldIntegrity = 0;
    }

    public void DisableShields()
    {
        _isShieldsDisabled = true;
        DecreaseIntegrity(999);
        _shieldTimerRef.ResetTimer();
    }

    public void EnableShields()
    {
        _isShieldsDisabled = false;
        _shieldTimerRef.StartOrResumeTimer();
    }
}
