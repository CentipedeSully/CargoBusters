using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarpCoreBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _inputDetected;
    [SerializeField] private bool _isWarpingInProgress = false;
    [SerializeField] private float _maxBuildDuration = 5;
    [SerializeField] private float _currentBuildTime = 0;
    [SerializeField] private int _ticksPassed = 0;
    [SerializeField] private int _tickPercentThreshold = 8;

    [Header("Events")]
    public UnityEvent OnWarpProgressTick;
    public UnityEvent OnWarpCompleted;


    //Monobehaviors
    private void Update()
    {
        BuildWarpIfWarpInProgress();
    }


    //Utilites
    private void BuildWarpIfWarpInProgress()
    {
        if (_isWarpingInProgress)
        {
            TickProgressOnThresholdReached();
            _currentBuildTime += Time.deltaTime;

            if (_currentBuildTime >= _maxBuildDuration)
            {
                ResetWarpUtilities();
                OldUiManager.Instance.EnableEscaped();
                OldUiManager.Instance.ShowGameOverScreen(2.5f);
                OnWarpCompleted?.Invoke();
            }
        }
    }

    private void TickProgressOnThresholdReached()
    {
        int normalizedProgress = (int)(_currentBuildTime / _maxBuildDuration * 100);
        //Debug.Log("Bust Progress: " + normalizedProgress + ", Ticks Passed: " + _ticksPassed);

        if (normalizedProgress == _tickPercentThreshold * _ticksPassed)
        {
            OnWarpProgressTick?.Invoke();
            _ticksPassed++;
        }
    }

    private void ResetWarpUtilities()
    {
        _currentBuildTime = 0;
        _isWarpingInProgress = false;
        _ticksPassed = 0;

    }


    //Getters/Setters
    public bool IsWarpInProgress()
    {
        return _isWarpingInProgress;
    }

    public void InterruptWarp()
    {
        ResetWarpUtilities();
    }

    public void StartBuildingWarp()
    {
        _isWarpingInProgress = true;
    }

}
