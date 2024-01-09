using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Regenerator : MonoBehaviour
{
    //Declarations
    [Tooltip("Used to differentiate btwn multiple regenerators that may exist on a single game object")]
    [SerializeField] private string _regeneratorName;

    [Tooltip("The amount to regenerate each tick")]
    [SerializeField] private int _regenRate = 0;

    [Tooltip("The amount of time in seconds btwn ticks. First tick happens once the regenerator activates")]
    [SerializeField] private float _regenTickDuration = 1;


    [SerializeField] private bool _isRegenerating = false;
    [SerializeField] private bool _isRegenEnabled = true;

    [Space(10)]
    public UnityEvent<int> OnRegenerationTick;


    //Monobehaviors
    //...


    //Utilities
    public void StartRegen()
    {
        if (_isRegenerating == false && _isRegenEnabled)
        {
            _isRegenerating = true;
            InvokeRepeating("TickRegeneration", _regenTickDuration, _regenTickDuration);
        }
    }

    public void StopRegen()
    {
        if (_isRegenerating)
        {
            _isRegenerating = false;
            CancelInvoke("TickRegeneration");
        }
    }

    public void SetRegenEnabled(bool value)
    {
        _isRegenEnabled = value;
        if (_isRegenEnabled == false)
            StopRegen();
    }

    private void TickRegeneration()
    {
        //Debug.Log("Regen Ticked");
        OnRegenerationTick?.Invoke(_regenRate);
    }


    public void SetRegenRate(int value)
    {
        if (value >= 0)
            _regenRate = value;
    }

    public void SetTickDuration(float value)
    {
        if (value > 0)
            _regenTickDuration = value;
    }

    public float GetRegenRate()
    {
        return _regenRate;
    }

    public float GetTickDuration()
    {
        return _regenTickDuration;
    }

    public bool IsRegenerating()
    {
        return _isRegenerating;
    }

    public bool IsRegenEnabled()
    {
        return _isRegenEnabled;
    }

    public string GetName()
    {
        return _regeneratorName;
    }
}
