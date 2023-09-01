using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserWeapon : AbstractShipWeapon
{
    //Declarations
    [Header("Laser Settings")]
    [SerializeField] [Min(.1f)] protected float _laserRange = 2;
    [SerializeField] [Min(.1f)] protected float _laserWidth = .2f;
    [SerializeField] protected Vector2 _localShotDirection = Vector2.up;
    [SerializeField] [Min(0)] protected int _piercePower = 0;
    protected LineRenderer _lineRenderer;
    [SerializeField] protected Vector3 _worldShotDirection;
    [SerializeField] protected Vector3 _calculatedLaserEndPoint;

    [Header("Accumulation Settings")]
    [SerializeField] protected bool _isLaserStarted = false;
    [SerializeField] [Min(.01f)] protected float _baseAccumulationOnImpact = 1;
    [SerializeField] protected float _maxAccumulationTime = 60;
    [SerializeField] protected Dictionary<int,float> _targetIDsWithAccumulation;
    [SerializeField] protected List<int> _IDsDetectedThisFrame;


    [Header("Debug Utils")]
    [SerializeField] protected Color _laserCastGizmoColor = Color.red;
    protected Vector3 _gizmoCastVector;
    protected Vector3 _lazerEndPointGizmo;
    public event ShipWeaponEvent OnLaserFireEntered;
    public event ShipWeaponEvent OnLaserFireExited;


    //Monobehaviours
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _targetIDsWithAccumulation = new Dictionary<int, float>();
        _IDsDetectedThisFrame = new List<int>();
    }

    private void LateUpdate()
    {
        ManageAccumulationOnTrackedTargets();
        ResetThisFramesDetectedIDs();
    }

    private void OnDrawGizmosSelected()
    {
        DrawCastGizmos();
    }


    //Internal Utils
    protected virtual void RenderLaserGraphic(float distance)
    {
        Vector2 endPoint = _localShotDirection * distance;
        Vector3 endPoint3D = new Vector3(endPoint.x, endPoint.y, transform.position.z);

        _lineRenderer.positionCount = 2;

        _lineRenderer.startWidth = _laserWidth;
        _lineRenderer.endWidth = _laserWidth;

        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, endPoint3D);

    }

    protected virtual void StopRenderingLaserGraphic()
    {
        _lineRenderer.positionCount = 0;
    }

    protected virtual void StartLaser()
    {
        _isLaserStarted = true;
        OnLaserFireEntered?.Invoke();
    }

    protected virtual bool IsDetectionValid(RaycastHit2D detection)
    {
        IDamageable damageableRef = detection.collider.GetComponent<IDamageable>();
        if (damageableRef == null)
            return false;
        LogStatement($"Detected ColliderUD: {damageableRef.GetInstanceID()}");
        LogStatement($"Parent ship ColliderID: {_parentShip.GetInstanceID()}");
        LogStatement($"Is Detected ID == parentID: {damageableRef.GetInstanceID() == _parentShip.GetInstanceID()}");
        bool isValid = GameManager.Instance.GetWeaponInteractablesList().Contains(detection.collider.tag) &&
               damageableRef.GetInstanceID() != _parentShip.GetInstanceID() &&
               _IDsDetectedThisFrame.Contains(damageableRef.GetInstanceID()) == false;
        LogStatement($"Is Detection Valid: {isValid}");
        return isValid;
    }

    protected virtual void DrawCastGizmos()
    {
        Gizmos.color = _laserCastGizmoColor;
        if (_isFiring)
        {
            Gizmos.DrawLine(transform.position, _calculatedLaserEndPoint);
            Gizmos.DrawWireSphere(_calculatedLaserEndPoint, _laserWidth / 2);
        }
    }

    protected virtual void CastLaserAndCaptureTargets()
    {

        _worldShotDirection = transform.TransformVector(_localShotDirection * _laserRange);
        _calculatedLaserEndPoint = transform.TransformPoint(_localShotDirection* _laserRange);
        RaycastHit2D[] detectedObjects = Physics2D.CircleCastAll(transform.position, _laserWidth / 2, _worldShotDirection, _laserRange);

        int validDetectionsFound = 0;
        float localRenderDistace = _laserRange;
        _IDsDetectedThisFrame = new List<int>();


        for (int i = 0; i < detectedObjects.Length; i++)
        {
            RaycastHit2D detection = detectedObjects[i];

            if (IsDetectionValid(detection))  
            {
                int detectedParentID = detection.collider.GetComponent<IDamageable>().GetInstanceID();
                _IDsDetectedThisFrame.Add(detectedParentID);
                validDetectionsFound += 1;


                //start tracking the target's accumulation if it isn't already being tracked
                if (_targetIDsWithAccumulation.ContainsKey(detectedParentID) == false)
                {
                    _targetIDsWithAccumulation.Add(detectedParentID, _baseAccumulationOnImpact);
                    LogStatement($"New ID detected. ID added to Accumulation Tracker");
                }


                //Determine if the laser should continue down its range: are we done piercing targets?
                if (validDetectionsFound == _piercePower + 1)
                {
                    // Make sure to keep the laser straight!!! 
                    _calculatedLaserEndPoint =  transform.TransformPoint(_localShotDirection * (detection.distance + _laserWidth)); 
                    localRenderDistace = detection.distance;
                    break;
                }   
            }
        }

        RenderLaserGraphic(localRenderDistace);
    }

    protected virtual void ManageAccumulationOnTrackedTargets()
    {
        //Create a side dictionary to record all changes for the original. Cant edit the original while iterating through it :(
        Dictionary<int, float> _adjustedAccumulationData = new Dictionary<int, float>();

        foreach (KeyValuePair<int,float> entry in _targetIDsWithAccumulation)
            _adjustedAccumulationData.Add(entry.Key, entry.Value);

        //Adjust the copy dictionary based on both our original dictionary and our current frame's laser cast results
        foreach (KeyValuePair<int, float> targetEntry in _targetIDsWithAccumulation)
        {
            int entryID = targetEntry.Key;                

            //if this tracked target got hit this frame, then accumulate more value to the target
            if (_IDsDetectedThisFrame.Contains(entryID))
            {
                //accumulate time each frame this object exists within the laser's reach
                _adjustedAccumulationData[entryID] = _adjustedAccumulationData[entryID] + Time.deltaTime;
                //LogStatement($"ID {entryID}'s accumulation is increasing. NewValue: {_adjustedAccumulationData[entryID]}");

                //apply damage if the accumulation value reached the max 
                if (_adjustedAccumulationData[entryID] >= _maxAccumulationTime)
                {
                    GameObject matchingObject = FindDamageableObjectInScene(entryID);

                    if (matchingObject!= null)
                        matchingObject.GetComponent<IDamageable>()?.TakeDamage(_damage, false);

                    //reset target's accumulation damage
                    _adjustedAccumulationData[entryID] = _baseAccumulationOnImpact;
                    LogStatement($"ID {entryID}'s accumulation has maxed out. Reseting accumulation to base value ({_baseAccumulationOnImpact})");
                }
            }

            //otherwise, degenerate this target's accumulation
            else
            {
                _adjustedAccumulationData[entryID] = _adjustedAccumulationData[entryID] - Time.deltaTime;
                //LogStatement($"ID {entryID}'s accumulation has degenerated by {Time.deltaTime} pts. NewValue: {_adjustedAccumulationData[entryID]}");

                //Stop tracking the target's accumulation if its accumulation value degenerated to zero or beyond
                if (_adjustedAccumulationData[entryID] <= 0)
                {
                    _adjustedAccumulationData.Remove(entryID);
                    //LogStatement($"ID {entryID}'s accumulation has fully Degenerated. ID removed from tracker");
                }

            }
        }


        //Apply our edits to the original dictionary
        _targetIDsWithAccumulation.Clear();

        foreach (KeyValuePair<int, float> entry in _adjustedAccumulationData)
            _targetIDsWithAccumulation.Add(entry.Key, entry.Value);

    }

    protected virtual GameObject FindDamageableObjectInScene(int instanceID)
    {
        GameObject[] foundObjects;
        foreach (string tag in GameManager.Instance.GetWeaponInteractablesList())
        {
            foundObjects = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject matchingObject in foundObjects)
            {
                IDamageable damageableRef = matchingObject.GetComponent<IDamageable>();

                if (damageableRef != null)
                {
                    if (damageableRef.GetInstanceID() == instanceID)
                        return matchingObject;
                }
                
            }

        }
        LogStatement($"Object of InstanceID '{instanceID}' not found in scene");
        return null;
    }

    protected virtual void ResetThisFramesDetectedIDs()
    {
        if (_IDsDetectedThisFrame.Count > 0)
            _IDsDetectedThisFrame.Clear();
    }

    protected virtual void EndLaser()
    {
        _isLaserStarted = false;
        OnLaserFireExited?.Invoke();
        StopRenderingLaserGraphic();
    }




    protected override void PerformWeaponFireLogic()
    {
        LogStatement($"Ship {_parentShip.GetName()} is Firing {_weaponName}");
        if (_isLaserStarted == false)
            StartLaser();

        CastLaserAndCaptureTargets();

    }

    protected override void EndWeaponsActivity()
    {
        if (_isWarmingUp)
        {
            LogStatement($"{_weaponName} on {_parentShip.GetName()} aborted warmup");
            CancelWarmup();
        }

        else if (_isFiring)
        {
            LogStatement($"{_weaponName} on {_parentShip.GetName()} aborted firing");
            EndLaser();
            EnterCooldown();

        }
    }



    //Getters Setters, & Commands
    public virtual float GetRange()
    {
        return _laserRange;
    }

    public virtual void SetRange(float newRange)
    {
        _laserRange = Mathf.Max(.1f, newRange);
    }

    public virtual Vector2 GetLocalShotDirection()
    {
        return _localShotDirection;
    }

    public virtual void SetLocalShotDirection(Vector2 newDirection)
    {
        if (newDirection.magnitude != 0)
            _localShotDirection = newDirection.normalized;
    }

    public virtual float GetLaserWidth()
    {
        return _laserWidth;
    }

    public virtual void SetLaserWidth(float value)
    {
        _laserWidth = Mathf.Max(.1f, value);
    }

    public virtual float GetBaseAccumulationOnImpact()
    {
        return _baseAccumulationOnImpact;
    }

    public virtual void SetBaseAccumulationOnImpact(float value)
    {
        _baseAccumulationOnImpact = Mathf.Max(value, 0.1f);
    }

    public virtual float GetMaxAccumulationTime()
    {
        return _maxAccumulationTime;
    }

    public virtual void SetMaxAccumulationTime(float value)
    {
        _maxAccumulationTime = Mathf.Max(.1f,value);
    }


    //Debug Uitls
    //...

}
