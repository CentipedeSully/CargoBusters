using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehavior : MonoBehaviour, IDeathBehavior
{
    //Declarations
    [Header("Death Behavior Utilites")]

    [SerializeField] [Min(0)] private float _deathSequenceDuration;
    [SerializeField] private bool _isDeathTriggered;


    [Header("Debug Utils")]
    [SerializeField] private bool _isDebugActive;
    [SerializeField] private bool _forceDeathCmd;



    //References
    private AbstractShip _parentShip;





    //Monobehaviour
    private void Update()
    {
        if (_isDebugActive)
            ListenForDebugCommands();
    }


    //Interface Utils
    public void SetParentShipAndInitializeAwakeReferences(AbstractShip parent)
    {
        _parentShip = parent;
    }

    public void InitializeGameManagerDependentReferences()
    {
        
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
        if (_isDebugActive)
            _isDebugActive = false;
        else _isDebugActive = true;
    }

    public bool IsDebugActive()
    {
        return _isDebugActive;
    }


    //Utils
    private void ReportDeathAndDie()
    {
        GameManager.Instance.GetInstanceTracker().RemoveShip(_parentShip.GetInstanceID());
        Destroy(this.gameObject);
    }


    //Debugging
    private void LogResponse(string responseDescription)
    {
        if (_parentShip == null)
        {
            Debug.LogError($"NULL_PARENT_SHIP on DeathBehavior of obejct: {this.gameObject}\n" + "NULL_SHIP " + responseDescription);
        }

        else
            Debug.Log(_parentShip.name + " " + responseDescription);
    }

    private void ListenForDebugCommands()
    {
        if (_forceDeathCmd)
        {
            _forceDeathCmd = false;
            TriggerDeathSequence();
        }
    }

}
