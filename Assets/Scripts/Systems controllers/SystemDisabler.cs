using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SystemDisabler : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isDisablerTicking = false;
    [SerializeField] private bool _isPenalized = false;
    [SerializeField] private int _boundaryCountdownDurationMax = 10;
    [SerializeField] private int _currentCountdownDuation = 0;
    private WaitForSeconds _cachedWaitForSeconds;
    private IEnumerator _timerReference;

    private bool _shieldsExist = false;
    private ShipInformation _shipInfoRef;
    private ShipSystemReferencer _systemReferencer;
    private ShieldsSystemController _shieldsControllerRef;
    private EnginesSystemController _enginesSystemControllerRef;
    private WeaponsSystemController _weaponsSystemControllerRef;
    private WarpCoreSystemController _warpCoreSystemControllerRef;
    private CargoSystemController _cargoSystemControllerRef;
    private CargoBusterBehavior _cargoBusterRef;

    [Header("Events")]
    public UnityEvent OnSystemsDisabled;
    public UnityEvent onBoundaryTimerStarted;
    public UnityEvent onBoundaryTimerInterrupted;
    public UnityEvent OnBoundaryTimerExpired;
    public UnityEvent<int> OnBoundaryTimerTick;


    //Monos
    private void Awake()
    {
        InitializeReferences();
    }


    //Utilites
    private void InitializeReferences()
    {
        _shipInfoRef = GetComponent<ShipInformation>();
        _systemReferencer = GetComponent<ShipSystemReferencer>();

        if (_systemReferencer.GetShieldsObject() != null)
        {
            _shieldsExist = true;
            _shieldsControllerRef = _systemReferencer.GetShieldsObject().GetComponent<ShieldsSystemController>();
        }

        _enginesSystemControllerRef = _systemReferencer.GetEnginesObject().GetComponent<EnginesSystemController>();
        _weaponsSystemControllerRef = _systemReferencer.GetWeaponsObject().GetComponent<WeaponsSystemController>();
        _warpCoreSystemControllerRef = _systemReferencer.GetWarpCoreObject().GetComponent<WarpCoreSystemController>();
        _cargoSystemControllerRef = _systemReferencer.GetCargoObject().GetComponent<CargoSystemController>();
        _cargoBusterRef = _systemReferencer.GetCargoBuster();
    }

    public void DisableAllSystems()
    {
        _shipInfoRef.SetShipDisabled(true);
        _weaponsSystemControllerRef.DisableWeapons();
        _enginesSystemControllerRef.DisableEngines();
        _warpCoreSystemControllerRef.DisableSystem();
        if (_shieldsExist)
            _shieldsControllerRef.DisableShields();

        _cargoSystemControllerRef.DisableCargoSecurity();
        _cargoBusterRef.DisableBuster();

        OnSystemsDisabled?.Invoke();
    }

    public void EnableAllSystems()
    {
        _shipInfoRef.SetShipDisabled(false);
        _weaponsSystemControllerRef.EnableWeapons();
        _enginesSystemControllerRef.EnableEngines();
        _warpCoreSystemControllerRef.EnableSystem();
        if (_shieldsExist)
            _shieldsControllerRef.EnableShields();

        _cargoSystemControllerRef.EnableCargoSecurity();
        _cargoBusterRef.EnableBuster();
    }

    private IEnumerator TickOutOfBoundsTimer()
    {
        _cachedWaitForSeconds = new WaitForSeconds(1);
        
        //Tick remaining time
        OnBoundaryTimerTick?.Invoke(_boundaryCountdownDurationMax - _currentCountdownDuation);

        while(_currentCountdownDuation < _boundaryCountdownDurationMax)
        {
            yield return _cachedWaitForSeconds;
            _currentCountdownDuation += 1;
            OnBoundaryTimerTick?.Invoke(_boundaryCountdownDurationMax - _currentCountdownDuation);
        }

        _isPenalized = true;
        _isDisablerTicking = false;
        OnBoundaryTimerExpired?.Invoke();
    }

    public void StartBoundaryTimer()
    {
        if (_isPenalized == false)
        {
            if (_timerReference == null)
            {
                onBoundaryTimerStarted?.Invoke();
                _isDisablerTicking = true;
                _timerReference = TickOutOfBoundsTimer();
                StartCoroutine(_timerReference);
            }

        }
    }

    public void EndBoundaryTimer()
    {
        if (_timerReference != null)
        {
            onBoundaryTimerInterrupted?.Invoke();
            _isDisablerTicking = false;
            _currentCountdownDuation = 0;
            StopCoroutine(_timerReference);
            _timerReference = null;
        }
       
    }
}
