using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarpCoreSystemController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isWarpCoreRepaired = false;
    [SerializeField] private bool _isWarpCoreOnline = true;
    [SerializeField] private bool _warpCommand = false;
    [SerializeField] private float _inputDelay = .5f;
    private bool _isInputReady = true;

    [Header("External Event Handles")]
    public UnityEvent OnWarpCoreDisabled;
    public UnityEvent OnWarpCoreEnabled;
    public UnityEvent onWarpSequenceEntered;
    public UnityEvent OnWarpInterrupted;

    //references
    private WarpCoreBehavior _warpCoreRef;


    //Monobehaviors
    private void Awake()
    {
        _warpCoreRef = GetComponent<WarpCoreBehavior>();
    }

    private void Update()
    {
        ToggleWarpOnInput();
    }


    //Utilities
    public void ToggleWarpOnInput()
    {
        if (_isWarpCoreOnline && _warpCommand && _isInputReady && _isWarpCoreRepaired)
        {
            _isInputReady = false;
            Invoke("ReadyInput", _inputDelay);

            if (_warpCoreRef.IsWarpInProgress())
                InterruptWarp();
            else
                onWarpSequenceEntered?.Invoke();
        }
    }

    private void ReadyInput()
    {
        _isInputReady = true;
    }


    //External Control Utils
    public void DisableSystem()
    {
        _isWarpCoreOnline = false;
        OnWarpCoreDisabled?.Invoke();
    }

    public void EnableSystem()
    {
        _isWarpCoreOnline = true;
        OnWarpCoreEnabled?.Invoke();
    }

    public void InterruptWarp()
    {
        OnWarpInterrupted?.Invoke();
    }

    public void RepairWarpCore()
    {
        _isWarpCoreRepaired = true;
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

    public bool IsWarpCoreRepaired()
    {
        return _isWarpCoreRepaired;
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
