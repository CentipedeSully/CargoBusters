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
    [SerializeField] private GameObject _currentFocusObject;
    [SerializeField] private GameObject _starParticlesPrefab;
    [SerializeField] private string _lookAheadFocusObjectName = "Look Ahead Focus";
    private LookAheadFocus _lookAheadFocusRef;
    private GameObject _starParticlesInstance;

    [Header("Zoom Settings")]

    [SerializeField] private float _minZoomDistance = 2;
    [SerializeField] private float _maxZoomDistance = 20;
    [SerializeField] private float _zoomSpeed = 2;

    [Header("Debug Utilities & Commands")]
    [SerializeField] private bool _isDebugActive = false;
    [SerializeField] private float _currentZoomDistance;
    [SerializeField] private int _currentZoomStep;
    [SerializeField] [Range(-1, 1)] private float _zoomInput;



    //Monobehaviors
    private void Update()
    {
        ZoomBasedOnInput();
    }





    //Internal Utils
    protected override void InitializeAdditionalFields()
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


        if (IsCurrentFocusNull())
            SetCurrentFocus(_playspaceOrigin.gameObject);
        else SetCurrentFocus(_currentFocusObject);


    }

    private void SetupLookAheadFocusIfItExists(GameObject cameraFocus)//!!!!!!!!!!!!!!!!! Find LookAheadRef
    {

    }

    private void CreateNewStarParticlesOnFocus()
    {
        if (_starParticlesPrefab == null)
        {
            STKDebugLogger.LogError("No StarParticlePrefab is set for the CameraController");
            return;
        }

        else
        {
            if (_starParticlesInstance != null)
                Destroy(_starParticlesInstance);

            _starParticlesInstance = Instantiate(_starParticlesPrefab, Vector3.zero, Quaternion.Euler(90, 0, 0), _currentFocusObject.transform);
        }
    }


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
    }// !!!!!!!!!!!!!!!!!!! Hook up inputs to Input Reader Reference


    //Getters, Setters, & Commands
    public bool IsCurrentFocusNull()
    {
        return _currentFocusObject == null;
    }

    public GameObject GetCurrentFocus()
    {
        return _currentFocusObject;
    }

    public void SetCurrentFocus(GameObject newFocusObject)
    {
        if (newFocusObject != null)
        {
            _currentFocusObject = newFocusObject;
            SetupLookAheadFocusIfItExists(newFocusObject);

            if (_lookAheadFocusRef == null)
                _mainVirtualCamera.Follow = newFocusObject.transform;

            else
            {
                _mainVirtualCamera.Follow = _lookAheadFocusRef.transform;
                if (_lookAheadFocusRef.IsCameraOnThisFocus() == false)
                    _lookAheadFocusRef.ToggleLookAheadFocus();

            }

            CreateNewStarParticlesOnFocus();
        }
    }


    public void SetZoomInput(float zoomCommand)
    {
        _zoomInput = Mathf.Clamp(zoomCommand, -1, 1);
    }

    //ALSO Create ChangeCameraFocus Utils

    //Debug Utils
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
