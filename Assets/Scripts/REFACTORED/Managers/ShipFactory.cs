using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class ShipFactory : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> _shipPrefabs;
    [SerializeField] private bool _isDebugActive = false;



    //Monobehaviours




    //Internal Utils





    //Getters, Setters, & Commands






    //Debugging Utils
    private void LogStatement(string statement)
    {
        STKDebugLogger.LogStatement(_isDebugActive,statement);
    }

    private void LogWarning(string warning)
    {
        STKDebugLogger.LogWarning(warning);
    }

    private void LogError(string error)
    {
        STKDebugLogger.LogError(error);
    }



}
