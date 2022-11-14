using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class OldThrusterToggler : MonoBehaviour
{
    [SerializeField] private string _spawnRateFieldName = "Particle Spawn Rate";
    [SerializeField] private int _maxForwardThrusterSpawnRate = 150;
    [SerializeField] private int _maxStrafeThrusterSpawnRate = 50;

    [SerializeField] private List<VisualEffect> _forwardsThrusters;
    [SerializeField] private List<VisualEffect> _reverseThrusters;
    [SerializeField] private List<VisualEffect> _rightStrafeThrusters;
    [SerializeField] private List<VisualEffect> _leftStrafeThrusters;

    private bool _isForwardsThrustersOn = false;
    private bool _isReverseThrustersOn = false;
    private bool _isRightStrafeThrustersOn = false;
    private bool _isLeftStrafeThrustersOn = false;


    public void ActivateForwardsThrusters()
    {
        foreach (VisualEffect thrusterEffect in _forwardsThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, _maxForwardThrusterSpawnRate);

        _isForwardsThrustersOn = true;
    }

    public void DeactivateForwardsThrusters()
    {
        foreach (VisualEffect thrusterEffect in _forwardsThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, 0);

        _isForwardsThrustersOn = false;
    }

    public void ActivateReverseThrusters()
    {
        foreach (VisualEffect thrusterEffect in _reverseThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, _maxStrafeThrusterSpawnRate);

        _isReverseThrustersOn = true;
    }

    public void DeactivateReverseThrusters()
    {
        foreach (VisualEffect thrusterEffect in _reverseThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, 0);

        _isReverseThrustersOn = false;

    }

    public void ActivateRightStrafeThrusters()
    {
        foreach (VisualEffect thrusterEffect in _rightStrafeThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, _maxStrafeThrusterSpawnRate);

        _isRightStrafeThrustersOn = true;
    }

    public void DeactivateRightStrafeThrusters()
    {
        foreach (VisualEffect thrusterEffect in _rightStrafeThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, 0);

        _isRightStrafeThrustersOn = false;
    }

    public void ActivateLeftStrafeThrusters()
    {
        foreach (VisualEffect thrusterEffect in _leftStrafeThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, _maxStrafeThrusterSpawnRate);

        _isLeftStrafeThrustersOn = true;
    }

    public void DeactivateLeftStrafeThrusters()
    {
        foreach (VisualEffect thrusterEffect in _leftStrafeThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, 0);

        _isLeftStrafeThrustersOn = false;
    }



    public bool IsForwardsThrustersOn()
    {
        return _isForwardsThrustersOn;
    }

    public bool IsReversethrustersOn()
    {
        return _isReverseThrustersOn;
    }

    public bool IsRightStrafeThrustersOn()
    {
        return _isRightStrafeThrustersOn;
    }

    public bool IsLeftStrafeThrustersOn()
    {
        return _isLeftStrafeThrustersOn;
    }
}
