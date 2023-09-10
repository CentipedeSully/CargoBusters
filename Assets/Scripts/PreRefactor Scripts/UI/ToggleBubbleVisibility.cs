using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBubbleVisibility : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> _bubbles;
    [Tooltip("All Bubbles before the index are assumed to be showing. Index increases as bubbles are shown")]
    [SerializeField] private int _nextHiddenBubbleIndex = 0;

    //monobheaviors
    //...


    //Utilites

    
    //Externals
    public void ShowNextBubble()
    {
        if (_nextHiddenBubbleIndex < _bubbles.Count)
        {
            int childCount = _bubbles[_nextHiddenBubbleIndex].transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                _bubbles[_nextHiddenBubbleIndex].transform.GetChild(i).gameObject.SetActive(true);
            }
            _nextHiddenBubbleIndex++;
        }
    }

}
