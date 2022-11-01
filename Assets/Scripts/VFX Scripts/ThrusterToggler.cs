using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ThrusterToggler : MonoBehaviour
{
    [SerializeField] private string _spawnRateFieldName = "Particle Spawn Rate";
    [SerializeField] private int _maxForwardThrusterSpawnRate = 150;
    [SerializeField] private int _maxStrafeThrusterSpawnRate = 50;

    [SerializeField] private List<VisualEffect> _forwardsThrusters;
    [SerializeField] private List<VisualEffect> _reverseThrusters;
    [SerializeField] private List<VisualEffect> _rightStrafeThrusters;
    [SerializeField] private List<VisualEffect> _leftStrafeThrusters;



    public void ActivateForwardsThrusters()
    {
        foreach (VisualEffect thrusterEffect in _forwardsThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, _maxForwardThrusterSpawnRate);
    }

    public void DeactivateForwardsThrusters()
    {
        foreach (VisualEffect thrusterEffect in _forwardsThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, 0);
    }

    public void ActivateReverseThrusters()
    {
        foreach (VisualEffect thrusterEffect in _reverseThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, _maxStrafeThrusterSpawnRate);
    }

    public void DeactivateReverseThrusters()
    {
        foreach (VisualEffect thrusterEffect in _reverseThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, 0);
    }

    public void ActivateRightStrafeThrusters()
    {
        foreach (VisualEffect thrusterEffect in _rightStrafeThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, _maxStrafeThrusterSpawnRate);
    }

    public void DeactivateRightStrafeThrusters()
    {
        foreach (VisualEffect thrusterEffect in _rightStrafeThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, 0);
    }

    public void ActivateLeftStrafeThrusters()
    {
        foreach (VisualEffect thrusterEffect in _leftStrafeThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, _maxStrafeThrusterSpawnRate);
    }

    public void DeactivateLeftStrafeThrusters()
    {
        foreach (VisualEffect thrusterEffect in _leftStrafeThrusters)
            thrusterEffect.SetInt(_spawnRateFieldName, 0);
    }


}
