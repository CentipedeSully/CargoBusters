using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class PlayerObjectManager : MonoSingleton<PlayerObjectManager>
{
    //Declarations
    private GameObject _playerObject;



    //Monos





    //Utilities

    public void SetPlayerObject(GameObject newPlayerObject)
    {
        _playerObject = newPlayerObject;
    }

    public GameObject GetPlayerObject()
    {
        return _playerObject;
    }

}
