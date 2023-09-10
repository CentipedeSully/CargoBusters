using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

public class KillNegater : MonoSingleton<KillNegater>
{
    //Declarations
    [SerializeField] private int _negateKillChance = 0;




    //Monobehaviors




    //Utilites
    public bool IsKillNegatedRollSuccessful()
    {
        return _negateKillChance >= RollD100();
    }

    private int RollD100()
    {
        return Random.Range(1, 101);
    }

    public void IncreaseKillNegationChance(int chance)
    {
        _negateKillChance += chance;
    }

    public int GetKilNegationChance()
    {
        return _negateKillChance;
    }

}
