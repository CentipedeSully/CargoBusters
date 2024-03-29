using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollectionController : MonoBehaviour
{
    //Declarations
    [Tooltip("Determines wheter or not the latest empty bubble should be charging or not")]
    [SerializeField] private bool _isAnimateRegenEnabled = false;
    [Tooltip("The last element is treated as the final bubble of the collection: If the first index is filled, then the Ui is displaying all bubbles as filled")]
    [SerializeField] private List<BubbleAnimatorController> _bubbleUiObjects;
    [SerializeField] private bool _fillCollectionOnStart = false;
    [SerializeField] private int _currentFilledIndex = 0;


    //Monobehaviors
    private void Awake()
    {
        if (_fillCollectionOnStart == false)
            _currentFilledIndex = _bubbleUiObjects.Count;
    }

    private void Start()
    {
        if (_fillCollectionOnStart)
            FillAll();
    }


    //Utilites
    private void ChargeLatestEmptyIndex()
    {
        if (_isAnimateRegenEnabled)
        {
            foreach (BubbleAnimatorController bubbleController in _bubbleUiObjects)
                bubbleController.StopChargingBubble();

            if (_currentFilledIndex == 1)
                _bubbleUiObjects[0].ChargeBubble();

            else if (_currentFilledIndex > 1)
                _bubbleUiObjects[_currentFilledIndex - 1].ChargeBubble();
        }
    }


    //External Control Utils
    public void DrainAll()
    {
        foreach (BubbleAnimatorController UiBubble in _bubbleUiObjects)
            UiBubble.DrainBubble();

        _currentFilledIndex = _bubbleUiObjects.Count;
        ChargeLatestEmptyIndex();
    }

    public void FillAll()
    {
        foreach (BubbleAnimatorController UiBubble in _bubbleUiObjects)
            UiBubble.FillBubble();

        _currentFilledIndex = 0;
    }

    public void DrainSingle()
    {
        if (_currentFilledIndex < _bubbleUiObjects.Count)
        {
            _bubbleUiObjects[_currentFilledIndex].DrainBubble();
            _currentFilledIndex++;
            ChargeLatestEmptyIndex();
        }
    }

    public void FillSingle()
    {
        if (_currentFilledIndex > 0)
        {
            _currentFilledIndex--;
            _bubbleUiObjects[_currentFilledIndex].FillBubble();
            ChargeLatestEmptyIndex();
        }
    }


    //Getters and Setters
    public void SetIsRegeneratingState(bool newValue)
    {
        _isAnimateRegenEnabled = newValue;

        if (_isAnimateRegenEnabled)
            ChargeLatestEmptyIndex();

        else
        {
            foreach (BubbleAnimatorController bubbleController in _bubbleUiObjects)
                bubbleController.StopChargingBubble();
        }
    }

    public bool IsCollectionRegenerating()
    {
        return _isAnimateRegenEnabled;
    }


}
