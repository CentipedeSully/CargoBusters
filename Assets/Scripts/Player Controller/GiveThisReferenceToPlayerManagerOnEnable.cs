using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveThisReferenceToPlayerManagerOnEnable : MonoBehaviour
{
    private void Start()
    {
        if (PlayerObjectManager.Instance)
            PlayerObjectManager.Instance.SetPlayerObject(gameObject);
    }
}
