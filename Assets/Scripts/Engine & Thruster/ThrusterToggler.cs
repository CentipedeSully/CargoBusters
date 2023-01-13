using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterToggler : MonoBehaviour
{
    [SerializeField] private List<VisualEffectController> _forwardsThrusters;
    [SerializeField] private List<VisualEffectController> _reverseThrusters;
    [SerializeField] private List<VisualEffectController> _rightStrafeThrusters;
    [SerializeField] private List<VisualEffectController> _leftStrafeThrusters;

    private bool _isForwardsThrustersOn = false;
    private bool _isReverseThrustersOn = false;
    private bool _isRightStrafeThrustersOn = false;
    private bool _isLeftStrafeThrustersOn = false;


    public void ActivateForwardsThrusters()
    {
        foreach (VisualEffectController thrusterEffect in _forwardsThrusters)
            thrusterEffect.ActivateVisualEffect();

        _isForwardsThrustersOn = true;
    }

    public void DeactivateForwardsThrusters()
    {
        foreach (VisualEffectController thrusterEffect in _forwardsThrusters)
            thrusterEffect.DeactivateVisualEffect();

        _isForwardsThrustersOn = false;
    }

    public void ActivateReverseThrusters()
    {
        foreach (VisualEffectController thrusterEffect in _reverseThrusters)
            thrusterEffect.ActivateVisualEffect();

        _isReverseThrustersOn = true;
    }

    public void DeactivateReverseThrusters()
    {
        foreach (VisualEffectController thrusterEffect in _reverseThrusters)
            thrusterEffect.DeactivateVisualEffect();

        _isReverseThrustersOn = false;

    }

    public void ActivateRightStrafeThrusters()
    {
        foreach (VisualEffectController thrusterEffect in _rightStrafeThrusters)
            thrusterEffect.ActivateVisualEffect();

        _isRightStrafeThrustersOn = true;
    }

    public void DeactivateRightStrafeThrusters()
    {
        foreach (VisualEffectController thrusterEffect in _rightStrafeThrusters)
            thrusterEffect.DeactivateVisualEffect();

        _isRightStrafeThrustersOn = false;
    }

    public void ActivateLeftStrafeThrusters()
    {
        foreach (VisualEffectController thrusterEffect in _leftStrafeThrusters)
            thrusterEffect.ActivateVisualEffect();

        _isLeftStrafeThrustersOn = true;
    }

    public void DeactivateLeftStrafeThrusters()
    {
        foreach (VisualEffectController thrusterEffect in _leftStrafeThrusters)
            thrusterEffect.DeactivateVisualEffect();

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
