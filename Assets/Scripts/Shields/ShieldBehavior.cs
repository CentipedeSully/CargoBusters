using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class ShieldBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] float _shieldIntegrity = 0;
    [SerializeField] float _maxShieldIntegrity = 5;
    [SerializeField] float _regenDelay = 5;
    [SerializeField] float _regenRate = .5f;

    [SerializeField] private Regenerator _shieldRegeneratorRef;
    [SerializeField] private Timer _shieldTimerRef;
    [SerializeField] private ShieldVFXController _vfxShieldReference;


    //Monobehaviors
    private void Awake()
    {
        _shieldTimerRef = GetComponent<Timer>();
        _shieldRegeneratorRef = GetComponent<Regenerator>();
        

        _shieldRegeneratorRef.SetRegenRate(_regenRate);
        _shieldTimerRef.SetDuration(_regenDelay);
    }

    private void OnEnable()
    {
        _shieldRegeneratorRef.OnRegenerationTick.AddListener(IncreaseIntegrity);
    }

    private void OnDisable()
    {
        _shieldRegeneratorRef.OnRegenerationTick.RemoveListener(IncreaseIntegrity);
    }

    private void Start()
    {
        if (_shieldIntegrity < _maxShieldIntegrity)
            _shieldTimerRef.StartOrResumeTimer();
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

    public void SetRegenRate(float value)
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
        if (value > 0)
        {
            SetShieldIntegrity(_shieldIntegrity + value);

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
            _shieldTimerRef.RestartTimer();

            if (_shieldIntegrity == 0)
                _vfxShieldReference.PlayShieldbreakAnim();
            else _vfxShieldReference.PlayShieldDamagedAnim();
        }
    }

    public void StartRegenerating()
    {
        if (_shieldIntegrity == 0)
            _vfxShieldReference.PlayShieldRestoreAnim();

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
}
