using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarpCoreSystemController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isWarpCoreOnline = true;
    [SerializeField] private bool _isWarpingInProgress = false;
    [SerializeField] private bool _warpCommand = false;
    private float _warpCommandInputDelay = .5f;
    private bool _isWarpInterrupted = true;
    private float _warpInterruptCooldown = .3f;
    [SerializeField] private bool _isWarpCommandInputReady = true;
    [SerializeField] private float _buildDuration = 5;
    [SerializeField] private float _currentBuildTime = 0;

    [Header("External Event Handles")]
    public UnityEvent OnWarpCoreDisabled;
    public UnityEvent OnWarpCoreEnabled;
    public UnityEvent onWarpSequenceEntered;
    public UnityEvent OnWarpInterrupted;
    public UnityEvent OnWarpCompleted;


    //Monobehaviors
    private void Update()
    {
        BuildWarpIfWarpInProgress();
        ToggleWarpSequenceOnCommandIfPossible();
    }


    //Utilities
    private void BuildWarpIfWarpInProgress()
    {
        if (_isWarpCoreOnline && _isWarpingInProgress)
        {
            _currentBuildTime += Time.deltaTime;

            if (_currentBuildTime >= _buildDuration)
            {
                ResetWarpUtilities();
                OnWarpCompleted?.Invoke();
            }
        }
    }

    private void ResetWarpUtilities()
    {
        _currentBuildTime = 0;
        _isWarpingInProgress = false;
    }

    private void ToggleWarpSequenceOnCommandIfPossible()
    {
        if (_isWarpCommandInputReady && _warpCommand && !_isWarpInterrupted)
        {
            if (!_isWarpingInProgress)
            {
                _isWarpingInProgress = true;
                _isWarpCommandInputReady = false;
                Invoke("ReadyWarpCommandInput", _warpCommandInputDelay);
                onWarpSequenceEntered?.Invoke();
            }

            else InterruptWarp();
        }
    }

    private void ReadyWarpCommandInput()
    {
        _isWarpCommandInputReady = true;
    }

    private void EndInterruptCooldown()
    {
        _isWarpInterrupted = false;
    }

    //External Control Utils
    public void DisableSystem()
    {
        _isWarpCoreOnline = false;
        InterruptWarp();
        OnWarpCoreDisabled?.Invoke();
    }

    public void EnableSystem()
    {
        _isWarpCoreOnline = true;
        OnWarpCoreEnabled?.Invoke();
    }

    public void InterruptWarp()
    {
        if (_isWarpingInProgress)
        {
            ResetWarpUtilities();
            _isWarpInterrupted = true;
            Invoke("EndInterruptCooldown", _warpInterruptCooldown);
            OnWarpInterrupted?.Invoke();
        }
    }


    //Basic Getters/Setters
    public void SetWarpCommand(bool value)
    {
        _warpCommand = value;
    }

    public bool IsWarpCoreOnline()
    {
        return _isWarpCoreOnline;
    }

    public bool IsWarpInProgress()
    {
        return _isWarpingInProgress;
    }


    //Debugging
    public void LogCompletion()
    {
        Debug.Log("Warp Completed");
    }

    public void LogInterruption()
    {
        Debug.Log("Interruption Successful");
    }

    public void LogWarpStart()
    {
        Debug.Log("Warp Sequence Intialized");
    }
}
