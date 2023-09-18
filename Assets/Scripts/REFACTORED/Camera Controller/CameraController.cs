using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using SullysToolkit;


public class CameraController : MonoSingleton<CameraController>
{
    //Declarations
    [Header("Camera Controller Settings")]
    [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;
    [SerializeField] private Transform _playspaceOrigin;
    [SerializeField] private LookAheadFocus _lookAheadFocusRef;
    [SerializeField] private ParticleSystem _starParticlesRef;
    [SerializeField] private GameObject _minimapCamera;
    [SerializeField] private GameObject _followObject;
    [SerializeField] private bool _doesFollowObjectHaveScanner = false;

    [Header("Zoom Settings")]
    [SerializeField] private float _minZoomDistance = 2;
    [SerializeField] private float _maxZoomDistance = 20;
    [SerializeField] private float _zoomSpeed = 2;

    [Header("Debug Utilities & Commands")]
    [SerializeField] private bool _isDebugActive = false;
    [SerializeField] private float _currentZoomDistance;
    [SerializeField] private int _currentZoomStep;
    [SerializeField] [Range(-1, 1)] private float _zoomInput;

    [Header("Testing Values")]
    [SerializeField] private GameObject _testFollowObjectDebug;


    [Header("Commands")]
    [SerializeField] private bool _followDebugObjectCmd;
    [SerializeField] private bool _releaseCameraFocusCmd;
    [SerializeField] private bool _stopParticlesCmd;
    [SerializeField] private bool _playParticlesCmd;



    //Monobehaviors
    private void Update()
    {
        ZoomBasedOnInput();

        if (_isDebugActive)
            ListenForDebugCommands();
    }

    private void LateUpdate()
    {
        UpdateMinimapPositionToFollowObject();
    }



    //Internal Utils
    protected override void InitializeAwakeUtils()
    {
        InitializeUtils();
    }

    private void InitializeUtils()
    {
        if (_mainVirtualCamera == null)
            STKDebugLogger.LogError("No main camera set for the CameraController");

        if (_playspaceOrigin == null)
        {
            _playspaceOrigin = transform;
            STKDebugLogger.LogWarning("CameraController's playspaceOrigin set to CameraController's transform.");
        }

        if (_lookAheadFocusRef == null)
            STKDebugLogger.LogError("An object with a LookAheadFocus isn't set in the CameraController");
        else
            _mainVirtualCamera.Follow = _lookAheadFocusRef.transform;

    }


    //FIX THIS vvv
    private void ZoomBasedOnInput()
    {
        _currentZoomDistance = _mainVirtualCamera.m_Lens.OrthographicSize;
        float newZoomDistance = _currentZoomDistance;

        //Need to implement: if pressed, lerp to next step.
        if (_zoomInput < 0 && _currentZoomDistance < _maxZoomDistance)
            newZoomDistance += _zoomSpeed * Time.deltaTime;

        else if (_zoomInput > 0 && _currentZoomDistance > _minZoomDistance)
            newZoomDistance -= _zoomSpeed * Time.deltaTime;


        if (newZoomDistance != _currentZoomDistance)
        {
            _mainVirtualCamera.m_Lens.OrthographicSize = newZoomDistance;
            _currentZoomDistance = _mainVirtualCamera.m_Lens.OrthographicSize;
        }
    }
    //FIX THIS ^^^

    private void UpdateMinimapPositionToFollowObject()
    {
        if (_followObject != null)
        {
            if (_doesFollowObjectHaveScanner)
            {
                _minimapCamera.transform.position = new Vector3(_followObject.transform.position.x, 
                                                                _followObject.transform.position.y, 
                                                                _minimapCamera.transform.position.z);
            }
                
        }
    }




    //Getters, Setters, & Commands
    public bool IsCameraFocusingOnObject()
    {
        return _followObject != null;
    }

    public GameObject GetCurrentCameraFocus()
    {
        return _followObject;
    }

    public void SetCameraFocusToNewFollowObject(GameObject newFollowObject)
    {
        if (newFollowObject != null)
        {
            _followObject = newFollowObject;

            //Determine what UI to show...
            //Currently just all there is to show is Minimap. Detemine if the minimap should be shown

            if (_followObject.GetComponent<ScannerBehaviour>() != null)
                _doesFollowObjectHaveScanner = true;
            else _doesFollowObjectHaveScanner = false;

            // parent the lookAhead object to the new Follow object. Be sure to go to that object
            _lookAheadFocusRef.transform.SetParent(_followObject.transform, false);

            EnableLookAhead();
        }
    }

    public void ReleaseCameraFocus()
    {
        //unparent from the current
        _lookAheadFocusRef.transform.SetParent(transform, true);
        //Disable All ShipUI objects, since we arent focused on any ships

        DisableLookAhead();
        _followObject = null;
    }



    public void StopStarParticles()
    {
        if (_starParticlesRef.isPlaying)
            _starParticlesRef.Stop();
    }

    public void PlayStarParticles()
    {
        if (_starParticlesRef.isStopped)
            _starParticlesRef.Play();
    }



    public void DisableLookAhead()
    {
        _lookAheadFocusRef.SetLookAheadActivity(false);
    }

    public void EnableLookAhead()
    {
        _lookAheadFocusRef.SetLookAheadActivity(true);
    }



    public void SetZoomInput(float zoomCommand)
    {
        _zoomInput = Mathf.Clamp(zoomCommand, -1, 1);
    }





    //Debug Utils
    private void ListenForDebugCommands()
    {
        if (_playParticlesCmd)
        {
            _playParticlesCmd = false;
            PlayStarParticles();
        }

        if (_stopParticlesCmd)
        {
            _stopParticlesCmd = false;
            StopStarParticles();
        }

        if (_followDebugObjectCmd)
        {
            _followDebugObjectCmd = false;
            SetCameraFocusToNewFollowObject(_testFollowObjectDebug);
        }

        if (_releaseCameraFocusCmd)
        {
            _releaseCameraFocusCmd = false;
            ReleaseCameraFocus();
        }

    }

    public bool IsDebugActive()
    {
        return _isDebugActive;
    }

    public void EnterDebug()
    {
        _isDebugActive = true;
    }

    public void ExitDebug()
    {
        _isDebugActive = false;
    }

    public void ToggleDebug()
    {
        if (IsDebugActive())
            ExitDebug();

        else EnterDebug();
    }

}
