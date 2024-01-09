using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BoundaryInhibitorBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private float _boundaryRadius = 50;
    [SerializeField] private string _vfxRadiusFieldName = "Radius";
    [SerializeField] private float _playerDistanceFromOrigin = Mathf.Infinity;
    [SerializeField] private bool _isPlayerPenalized = false;

    //references
    private GameObject _playerObj;
    private SystemDisabler _playerSystemDisabler;
    private VisualEffect _vfxReference;


    //Monos
    private void Awake()
    {
        _vfxReference = GetComponent<VisualEffect>();
        LinkVFXRadius();
    }

    private void Update()
    {
        LinkVFXRadius();
        WatchPlayerDistance();
    }


    //Utils
    private void LinkVFXRadius()
    {
        _vfxReference.SetFloat(_vfxRadiusFieldName, _boundaryRadius);
    }

    private void WatchPlayerDistance()
    {
        if (_playerObj != null && !_isPlayerPenalized)
        {
            //Recalculate player distance
            _playerDistanceFromOrigin = Mathf.Abs(Vector2.Distance(_playerObj.transform.position, transform.position));

            //Start the disabler timer if the player is OutOfBounds and the timer isn't ticking
            if (_playerDistanceFromOrigin > _boundaryRadius)
            {
                if (_playerSystemDisabler.IsDisablerTicking() == false)
                    _playerSystemDisabler.StartBoundaryTimer();
            }
            else
            {
                if (_playerSystemDisabler.IsDisablerTicking())
                    _playerSystemDisabler.EndBoundaryTimer();
            }
        }

    }

    //External Control Utils
    public void SetPlayer()
    {
        _playerObj = OldPlayerObjectManager.Instance.GetPlayerObject();
        _playerSystemDisabler = _playerObj.GetComponent<SystemDisabler>();
    }
}
