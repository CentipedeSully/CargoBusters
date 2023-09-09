using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAheadFocus : MonoBehaviour
{
    //Declarations
    [Header("Look Ahead Settings")]
    [SerializeField][Min(0)] private float _maxLookAheadDistance = 1.5f;
    [SerializeField] [Min(0)] private float _lerpTimeStep = .01f;
    [SerializeField] private bool _isCameraOnThisFocus = false;
    [SerializeField] private Rigidbody2D _shipRB;
    private InputReader _inputReaderRef;

    [Header("Debugging Utilities")]
    [SerializeField] private bool _isDebugActive = false;
    [SerializeField] private Vector2 _inputDirection = Vector2.zero;




    //Monobehaviours
    private void Update()
    {
        if (_isCameraOnThisFocus)
        {
            ReadInput();
            DriftFocusTowardsInputDirection();
        }
    }




    //Internal Utils
    private void DriftFocusTowardsInputDirection()
    {
        Vector2 targetPosition = _inputDirection * _maxLookAheadDistance;
        Vector2 currentPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);

        if ( currentPosition != targetPosition)
            transform.localPosition =  Vector2.Lerp(currentPosition, targetPosition, _lerpTimeStep);
        

    }

    private void ReadInput()
    {
        if (_inputReaderRef == null) 
            _inputReaderRef = GameManager.Instance.GetInputReader();
        
        if (_inputReaderRef != null)
            _inputDirection = new Vector2(_inputReaderRef.GetPlayerStrafeInput(),_inputReaderRef.GetPlayerThrustInput());
    }


    //Getters, Setters, & Commands
    public void SetInput(Vector2 newInput)
    {
        _inputDirection = newInput;
        _inputDirection.x = Mathf.Clamp(_inputDirection.x, -1, 1);
        _inputDirection.y = Mathf.Clamp(_inputDirection.y, -1, 1);
    }

    public void ToggleLookAheadFocus()
    {
        if (_isCameraOnThisFocus)
            _isCameraOnThisFocus = false;
        else _isCameraOnThisFocus = true;
    }

    public bool IsCameraOnThisFocus()
    {
        return _isCameraOnThisFocus;
    }

}
