using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveThisReferenceToPlayerManagerOnEnable : MonoBehaviour
{
    private void Start()
    {
        if (OldPlayerObjectManager.Instance)
            OldPlayerObjectManager.Instance.SetPlayerObject(gameObject);
    }
}
