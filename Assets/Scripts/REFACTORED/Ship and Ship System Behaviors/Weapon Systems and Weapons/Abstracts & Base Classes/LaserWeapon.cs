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
    [SerializeField] protected List<string> _validTags;
    [SerializeField] [Min(0)] protected int _piercePower = 0;
    protected LineRenderer _lineRenderer;

    [Header("Accumulation Settings")]
    [SerializeField] protected bool _isLaserStarted = false;
    [SerializeField] [Min(.01f)] protected float _baseAccumulationOnImpact = .05f;
    [SerializeField] [Min(.01f)] protected float _accumulationDegenValue = .01f; 
    [SerializeField] protected float _maxAccumulationTime = .25f;
    [SerializeField] protected Dictionary<int,float> _targetIDsWithAccumulation;
    [SerializeField] protected List<int> _IDsDetectedThisFrame;


    public event ShipWeaponEvent OnLaserFireEntered;
    public event ShipWeaponEvent OnLaserFireExited;


    //Monobehaviours
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        if (_validTags == null)
            _validTags = new List<string>();

        _targetIDsWithAccumulation = new Dictionary<int, float>();
        _IDsDetectedThisFrame = new List<int>();
    }

    private void Update()
    {
        ManageAccumulationOnTrackedTargets();
    }

    private void LateUpdate()
    {
        ResetThisFramesDetectedIDs();
    }


    //Internal Utils
    protected virtual void RenderLaserGraphic(float distance)
    {
        Vector2 endPoint = _localShotDirection * distance;
        Vector3 endPoint3D = new Vector3(endPoint.x, endPoint.y, transform.position.z);

        _lineRenderer.positionCount = 2;

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

    protected virtual bool IsColliderValid(Collider2D detectedCollider)
    {
        return _validTags.Contains(detectedCollider.tag) && 
               detectedCollider.GetInstanceID() != _parentShip.GetInstanceID() &&
               _IDsDetectedThisFrame.Contains(detectedCollider.GetInstanceID()) == false;
    }

    protected virtual void CastLaserAndCaptureTargets()
    {
        RaycastHit2D[] detectedObjects = Physics2D.CircleCastAll(transform.position, _laserWidth / 2, _localShotDirection * _laserRange);
        int validDetectionsFound = 0;
        float renderDistace = _laserRange;
        _IDsDetectedThisFrame = new List<int>();


        for (int i = 0; i < detectedObjects.Length; i++)
        {
            RaycastHit2D detection = detectedObjects[i];

            if (IsColliderValid(detection.collider))  
            {
                int detectedColliderID = detection.collider.GetInstanceID();
                _IDsDetectedThisFrame.Add(detectedColliderID);
                validDetectionsFound += 1;


                //start tracking the target's accumulation if it isn't already being tracked
                if (_targetIDsWithAccumulation.ContainsKey(detectedColliderID) == false)
                    _targetIDsWithAccumulation.Add(detectedColliderID, _baseAccumulationOnImpact);


                //Determine if the laser should continue down its range: are we done piercing targets?
                if (validDetectionsFound == _piercePower + 1)
                {
                    renderDistace = detection.distance;
                    break;
                }   
            }
        }

        RenderLaserGraphic(renderDistace);
    }

    protected virtual void ManageAccumulationOnTrackedTargets()
    {
        foreach (KeyValuePair<int, float> targetEntry in _targetIDsWithAccumulation)
        {
            int entryID = targetEntry.Key;                

            //if this tracked target got hit this frame, then accumulate more value to the target
            if (_IDsDetectedThisFrame.Contains(entryID))
            {
                //accumulate time each frame this object exists within the laser's reach
                _targetIDsWithAccumulation[entryID] += Time.deltaTime;

                //apply damage if the accumulation value reached the max 
                if (_targetIDsWithAccumulation[entryID] >= _maxAccumulationTime)
                {
                    GameObject matchingObject = FindObjectInScene(entryID);

                    if (matchingObject!= null)
                        matchingObject.GetComponent<IDamageable>()?.TakeDamage(_damage, false);

                    //reset target's accumulation damage
                    _targetIDsWithAccumulation[entryID] = _baseAccumulationOnImpact;
                }

                //otherwise, degenerate this target's accumulation
                else
                {
                    _targetIDsWithAccumulation[entryID] -= _accumulationDegenValue;

                    //Stop tracking the target's accumulation if its accumulation value degenerated to zero
                    if (_targetIDsWithAccumulation[entryID] <= 0)
                        _targetIDsWithAccumulation.Remove(entryID);
                }

            }
        }
    }

    protected virtual GameObject FindObjectInScene(int instanceID)
    {
        GameObject[] foundObjects;
        foreach (string tag in _validTags)
        {
            foundObjects = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject matchingObject in foundObjects)
            {
                if (matchingObject.GetInstanceID() == instanceID)
                    return matchingObject;
            }

        }

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
