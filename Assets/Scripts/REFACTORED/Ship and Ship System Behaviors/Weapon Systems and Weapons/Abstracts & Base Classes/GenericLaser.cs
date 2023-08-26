using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericLaser : AbstractLaserWeapon
{
    //Declarations
    [Header("Laser Settings")]
    [SerializeField] [Min(.1f)] protected float _laserRange = 2;
    [SerializeField] [Min(.1f)] protected float _laserWidth = .2f;
    [SerializeField] protected Vector2 _localShotDirection = Vector2.up;
    [SerializeField] protected List<string> _validTags;
    protected LineRenderer _lineRenderer;

    [Header("Accumulation Settings")]
    [SerializeField] protected float _maxAccumulationTime = .25f;
    [SerializeField] protected float _currentAccumulationTime = 0;
    [SerializeField] protected float _missGracePeriod = .1f;

    [SerializeField] protected bool _isBeamActive = false;
    [SerializeField] protected bool _isBeamHittingTarget = false;
    [SerializeField] protected Collider2D _currentTarget;


    //Monobehaviours
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        if (_validTags == null)
            _validTags = new List<string>();
    }


    //Internal Utils
    protected virtual void BuildLaser()
    {
        RaycastHit2D[] detectedObjects = Physics2D.CircleCastAll(transform.position, _laserWidth / 2, _localShotDirection * _laserRange);

        
        for (int i = 0; i < detectedObjects.Length; i ++)
        {
            RaycastHit2D detection = detectedObjects[i];

            if (_validTags.Contains(detection.collider.tag) && detection.collider.GetInstanceID() != _parentShip.GetInstanceID())
            {
                RenderLine(detection.distance);
                break;
            }
        }

        if (detectedObjects.Length == 0)
        {
            RenderLine(_laserRange);
        }

        //UpdateDamageAccumulation()
    }

    protected virtual void CeaseLaser()
    {
        StopRenderingLine();
        //EndDamageAccumulation()
        
    }

    protected virtual void RenderLine(float distance)
    {
        Vector2 endPoint =_localShotDirection * distance;
        _lineRenderer.positionCount = 2;

        _lineRenderer.SetPosition(1, endPoint);
    }

    protected virtual void StopRenderingLine()
    {
        _lineRenderer.positionCount = 0;
    }

    protected override void FireLaser()
    {
        
    }




    //Getters, Setters, & Commands
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

}
