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
    [SerializeField] private List<int> _detectedIDsWithinRange;
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
    [SerializeField] private bool _logDetectionsCmd = false;
    [SerializeField] private bool _sortDetectionsCmd = false;
 


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
            else if (_detectedIDsWithinRange.Contains(scannableObject.GetInstanceID()) == false && scannableObject.GetInstanceID() != _parentID)
            {
                _detectedIDsWithinRange.Add(scannableObject.GetInstanceID());
                LogStatement($"Captured Scan: {scannableObject.GetNameAsScannedObject()}");
            }
                
        }
    }

    private bool IsObjectCurrentTarget(GameObject suspectedObject)
    {
        return suspectedObject.transform == _currentTarget;
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
        GameObject detectedObject = null;

        for (int i = _detectedIDsWithinRange.Count - 1; i >= 0; i--)
        {

            detectedObject = GameManager.Instance.FindObjectWithID(_detectedIDsWithinRange[i]);

            if (detectedObject == null)
            {
                LogStatement($"Removed nonexistent object of index: {_detectedIDsWithinRange[i]}");
                _detectedIDsWithinRange.RemoveAt(i);

            }

            else if (detectedObject.activeInHierarchy == false)
            {
                if (IsObjectCurrentTarget(detectedObject))
                    ClearCurrentTarget();

                LogStatement($"Removed scan due to scan inactive: {detectedObject.GetComponent<IScannable>().GetNameAsScannedObject()}");
                _detectedIDsWithinRange.Remove(_detectedIDsWithinRange[i]);

            }

            else if (CalculateDistance(detectedObject) > _scanRadius)
            {
                if (IsObjectCurrentTarget(detectedObject))
                    ClearCurrentTarget();

                LogStatement($"Removed scan due to scan out of range: {detectedObject.GetComponent<IScannable>().GetNameAsScannedObject()}");
                _detectedIDsWithinRange.Remove(_detectedIDsWithinRange[i]);
            }

        }
    }

    private void CountScans()
    {
        _scanCount = _detectedIDsWithinRange.Count;
    }

    private IScannable FindClosestScan()
    {
        GameObject closest = null;

        foreach (int detectedID in _detectedIDsWithinRange)
        {
            GameObject detectedObject = GameManager.Instance.FindObjectWithID(detectedID);


            if (closest == null)
                closest = detectedObject;
            else
            {
                if (CalculateDistance(closest) > CalculateDistance(detectedObject))
                    closest = detectedObject;
            }
        }

        return closest.GetComponent<IScannable>();
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

    private void SortDetectionsFromClosestToFurthest()
    {
        if (_scanCount < 2)
            return;

        List<int> sortedList = new List<int>();
        IScannable closestDetection = null;

        //step 1: find the closest detection
        //step 2: add the closest detection to the new List
        //step 3: remove the detection from the old list
        //step 4: repeat the process until no detections remain

        int maxIterations = _detectedIDsWithinRange.Count;
        for (int i = 0; i < maxIterations; i++)
        {
            closestDetection = FindClosestScan();
            sortedList.Add(closestDetection.GetInstanceID());
            _detectedIDsWithinRange.Remove(closestDetection.GetInstanceID());
        }

        _detectedIDsWithinRange = sortedList;
        LogStatement($"Is detectionsList == newlySortedList (reference-wise): {_detectedIDsWithinRange == sortedList}");
        LogDetections();
        return;
    }

    


    //Getters, Setters, and Commands
    public void InitializeScanner(int parentID)
    {
        SetParentID(parentID);
        _detectedIDsWithinRange = new List<int>();
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

    private void LogDetections()
    {
        string DetectionLog = "Detections: \n";
        GameObject detectedObject = null;

        for (int i = 0; i < _detectedIDsWithinRange.Count; i++)
        {
            detectedObject = GameManager.Instance.FindObjectWithID(_detectedIDsWithinRange[i]);
            DetectionLog += detectedObject.GetComponent<IScannable>().GetNameAsScannedObject() + "\n";
        }
            

        DetectionLog += "---End of Detections Log---";
        LogStatement(DetectionLog);
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

        if (_logDetectionsCmd)
        {
            _logDetectionsCmd = false;
            LogDetections();
        }

        if (_sortDetectionsCmd)
        {
            _sortDetectionsCmd = false;
            SortDetectionsFromClosestToFurthest();
        }
    }

}
