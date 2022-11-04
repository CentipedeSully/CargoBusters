using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachPlayerBehavior : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector2 _enemyInput;
    [SerializeField] private Vector3 _enemyRotationVector;
    [SerializeField] private float _differenceInDegrees;

    //references
    private Rigidbody2D _rigidbody2DRef;
    private AddRotationToObject _rotateScriptRef;
    private MoveObject _moveScriptRef;

    //states
    [SerializeField] private bool _isEngagingTarget = false;


    //Monobehaviors

    private void Awake()
    {
        _rigidbody2DRef = GetComponent<Rigidbody2D>();
        _rotateScriptRef = GetComponent<AddRotationToObject>();
        _moveScriptRef = GetComponent<MoveObject>();
    }

    private void Update()
    {
        EngageTarget();
    }


    //Utils

    private void EngageTarget()
    {
        if (_isEngagingTarget)
        {
            if (_target == null)
                GetPlayerReference();
            CalculateEnemyInputBasedOnRelativePlayerLocation();
            CalculateEnemyRotationVector();
            CommunicateRotationDirection();
            CommunicateMoveInput();
        }
    }

    public void EnableEngagement()
    {
        _isEngagingTarget = true;
    }

    public void DisableEngagement()
    {
        _isEngagingTarget = false;
    }

    private void CalculateEnemyInputBasedOnRelativePlayerLocation()
    {
        if (_target != null)
        {
            _enemyInput =  transform.InverseTransformVector(_target.transform.position - transform.position);
            _enemyInput = new Vector2(Mathf.Clamp(_enemyInput.x,-1,1), Mathf.Clamp(_enemyInput.y,-1,1));

            //_differenceInDegrees = Mathf.Atan2(_enemyInput.y, _enemyInput.x);
        }
    }

    private void CalculateEnemyRotationVector()
    {
        _enemyRotationVector = new Vector3(0, 0,  -1 * _enemyInput.x);
    }

    private void CommunicateRotationDirection()
    {
        _rotateScriptRef.AddRotation(_enemyRotationVector);
    }

    private void CommunicateMoveInput()
    {
        _moveScriptRef.SetDirection(new Vector2(0,_enemyInput.y));
    }

    private void GetPlayerReference()
    {
        _target =  GameObject.Find("Player");
    }

    public Vector2 GetShipInput()
    {
        return _enemyInput;
    }
}
