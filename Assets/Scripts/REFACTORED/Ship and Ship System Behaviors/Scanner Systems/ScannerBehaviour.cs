using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;


public interface IScannable
{
    int GetInstanceID();

    GameObject GetGameObject();

    string GetNameAsScannedObject();


}


public class ScannerBehaviour : MonoBehaviour
{
    //Declarations
    [Header("Scanner Settings")]
    [SerializeField] private float _scanRadius;
    [SerializeField] private int _scanCount;
    private List<IScannable> _scannablesWithinRange;
    private Collider2D[] _scanResults;

    [SerializeField] private int _parentID = 0;
    private bool _isScannerInitialized = false;

    [Header("Targeting Settings")]
    [SerializeField] private Transform _currentTarget;
    [SerializeField] private GameObject _targetingSprite;

    [Header("Debug Utils")]
    [SerializeField] private bool _isDebugActive = false;
    [SerializeField] private Color _scanRadiusGizmoColor = Color.yellow;
    [SerializeField] private bool _targetClosestScanCmd = false;
    [SerializeField] private bool _clearCurrentTargetCmd = false;
 


    //Monobehaviours
    private void Update()
    {
        if (_isScannerInitialized)
        {
            CaptureScannablesWithinRange();
            RemoveOutOfRangeScans();
            CountScans();
        }

        if (_isDebugActive)
            ListenForDebugCommands();

    }

    private void OnDrawGizmosSelected()
    {
        DrawScanGizmo();
    }




    //Internal Utils
    private void CaptureScannablesWithinRange()
    {
        _scanResults = Physics2D.OverlapCircleAll(transform.position, _scanRadius);

        foreach (Collider2D capturedCollider in _scanResults)
        {
            IScannable scannableObject = capturedCollider.GetComponent<IScannable>();
            if (scannableObject == null)
                continue;
            else if (_scannablesWithinRange.Contains(scannableObject) == false && scannableObject.GetInstanceID() != _parentID)
            {
                _scannablesWithinRange.Add(scannableObject);
                LogStatement($"Captured Scan: {scannableObject.GetNameAsScannedObject()}");
            }
                
        }
    }

    private float CalculateDistance(GameObject objectInQuestion)
    {
        if (objectInQuestion== null)
        {
            LogWarning($"Attempted to calculate distance of Null object");
            return float.MinValue;
        }

        else
        {
            Vector2 relativeDistanceVector = objectInQuestion.transform.position - transform.position;
            //LogStatement($"Object ({objectInQuestion.name}) distance: {relativeDistanceVector.magnitude}");
            return relativeDistanceVector.magnitude;
        }
    }

    private void RemoveOutOfRangeScans()
    {
        for (int i = _scannablesWithinRange.Count - 1; i >= 0; i--)
        {
            if (CalculateDistance(_scannablesWithinRange[i].GetGameObject()) > _scanRadius)
            {
                if (_scannablesWithinRange[i].GetGameObject().transform == _currentTarget)
                    ClearCurrentTarget();

                LogStatement($"Removed scan due to scan out of range: {_scannablesWithinRange[i].GetNameAsScannedObject()}");
                _scannablesWithinRange.Remove(_scannablesWithinRange[i]);
            }
        }
    }

    private void CountScans()
    {
        _scanCount = _scannablesWithinRange.Count;
    }

    private IScannable FindClosestScan()
    {
        IScannable closest = null;

        foreach (IScannable scan in _scannablesWithinRange)
        {
            if (closest == null)
                closest = scan;
            else
            {
                if (CalculateDistance(closest.GetGameObject()) > CalculateDistance(scan.GetGameObject()))
                    closest = scan;
            }
        }

        return closest;
    }

    private void ClearCurrentTarget()
    {
        _currentTarget = null;
    }

    private void SetCurrentTarget(IScannable newTarget)
    {
        if (_currentTarget != null)
            ClearCurrentTarget();

        _currentTarget = newTarget.GetGameObject().transform;
    }




    //Getters, Setters, and Commands
    public void InitializeScanner(int parentID)
    {
        SetParentID(parentID);
        _scannablesWithinRange = new List<IScannable>();
        _isScannerInitialized = true;
    }

    public void SetParentID(int ID)
    {
        _parentID = ID;
    }

    public int GetParentID()
    {
        return _parentID;
    }

    public void TargetClosestScan()
    {
        if (_scanCount > 0)
            SetCurrentTarget(FindClosestScan());
    }




    //Debug Utils
    private void DrawScanGizmo()
    {
        Gizmos.color = _scanRadiusGizmoColor;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }

    private void LogStatement(string statement)
    {
        STKDebugLogger.LogStatement(_isDebugActive, statement);
    }

    private void LogWarning(string warning)
    {
        STKDebugLogger.LogWarning(warning);
    }

    private void LogError(string error)
    {
        STKDebugLogger.LogError(error);
    }

    private void ListenForDebugCommands()
    {
        if (_targetClosestScanCmd)
        {
            _targetClosestScanCmd = false;
            TargetClosestScan();
        }

        if (_clearCurrentTargetCmd)
        {
            _clearCurrentTargetCmd = false;
            ClearCurrentTarget();
        }
    }

}
