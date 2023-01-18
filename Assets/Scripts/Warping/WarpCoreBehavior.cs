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

    [Header("Events")]
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
            _currentBuildTime += Time.deltaTime;

            if (_currentBuildTime >= _maxBuildDuration)
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
