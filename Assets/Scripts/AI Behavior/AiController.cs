using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiController : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isPursuingTarget = false;
    [SerializeField] private GameObject _currentTarget;

    [Header("Events")]
    public UnityEvent<GameObject> OnPursuitEntered;
    public UnityEvent OnPursuitExited;


    //Monobheavior


    //Utilites


    //External Control Utils
    public void EnterPursuit(GameObject target)
    {
        if (target != null)
        {
            _currentTarget = target;
            _isPursuingTarget = true;
            OnPursuitEntered?.Invoke(_currentTarget);
        }
    }

    public void ExitPursuit()
    {
        _isPursuingTarget = false;
        _currentTarget = null;
        OnPursuitExited?.Invoke();
    }

    //getters and Setters
    public bool IsPursuingTarget()
    {
        return _isPursuingTarget;
    }
}
