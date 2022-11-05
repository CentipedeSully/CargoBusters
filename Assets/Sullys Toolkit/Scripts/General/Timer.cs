using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private string _name = "Unnamed Timer";
    [SerializeField] private bool _isTicking = false;
    [SerializeField] private float _timePassed = 0;
    [SerializeField] private float _duration = 0;

    public UnityEvent OnTimerExpired;



    //Monobehaviors
    private void Update()
    {
        CountTime();
    }





    //Utilities
    private void CountTime()
    {
        if (_isTicking)
        {
            _timePassed += Time.deltaTime;

            if (_timePassed >= _duration)
            {
                ResetTimer();
                OnTimerExpired?.Invoke();
            }
        }

    }

    private void ResetTimer()
    {
        _timePassed = 0;
        _isTicking = false;
    }


    public void PauseTimer()
    {
        _isTicking = false;
    }

    public void StartOrResumeTimer()
    {
        _isTicking = true;
    }

    public void RestartTimer()
    {
        ResetTimer();
        StartOrResumeTimer();
    }

    public void SetDuration(float value)
    {
        if (value >= 0)
            _duration = value;
    }

    public float GetDuration()
    {
        return _duration;
    }

    public float GetTimePassed()
    {
        return _timePassed;
    }

    public bool IsTicking()
    {
        return _isTicking;
    }

    public bool IsTimerStarted()
    {
        if (_timePassed > 0)
            return true;

        else return false;
    }

    public string GetName()
    {
        return _name;
    }
}
