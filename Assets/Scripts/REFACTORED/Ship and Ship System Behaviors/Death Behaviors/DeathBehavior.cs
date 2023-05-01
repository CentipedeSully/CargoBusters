using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehavior : MonoBehaviour, IDeathBehavior
{
    //Declarations
    [Header("Death Behavior Utilites")]

    [SerializeField] [Min(0)] private float _deathSequenceDuration;
    [SerializeField] private bool _isDeathTriggered;
    [SerializeField] private bool _showDebug;

    //References
    private AbstractShip _parentShip;
    private IInstanceTracker _instanceTracker;





    //Monobehaviour
    //...


    //Interface Utils
    public void SetParentShipAndInitializeAwakeReferences(AbstractShip parent)
    {
        _parentShip = parent;
    }

    public void InitializeGameManagerDependentReferences()
    {
        _instanceTracker = GameManager.Instance.GetInstanceTracker();
    }

    public float GetDeathSequenceDuration()
    {
        return _deathSequenceDuration;
    }

    public bool IsDying()
    {
        return _isDeathTriggered;
    }

    public void TriggerDeathSequence()
    {
        if (_isDeathTriggered == false)
        {
            _isDeathTriggered = true;
            Invoke("ReportDeathAndDie", _deathSequenceDuration);
        }

    }

    public void ToggleDebugMode()
    {
        if (_showDebug)
            _showDebug = false;
        else _showDebug = true;
    }

    public bool IsDebugActive()
    {
        return _showDebug;
    }


    //Utils
    private void ReportDeathAndDie()
    {
        _instanceTracker.ReportDeath(this.gameObject);
        Destroy(this.gameObject);
    }


    //Debugging
    private void LogResponse(string responseDescription)
    {
        if (_parentShip == null)
        {
            Debug.LogError($"NULL_PARENT_SHIP on DeathBehavior of obejct: {this.gameObject}");
            Debug.Log("NULL_SHIP " + responseDescription);
        }

        else
            Debug.Log(_parentShip.name + " " + responseDescription);
    }



}
