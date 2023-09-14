using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SullysToolkit;

struct FactionInfo
{
    private string _name;
    private List<string> _enemies;
    private List<string> _allies;


    public FactionInfo(string name)
    {
        _name = name;
        _enemies = new List<string>();
        _allies = new List<string>();

    }


    public string GetName()
    {
        return _name;
    }

    public List<string> GetEnemies()
    {
        return _enemies;
    }

    public List<string> GetAllies()
    {
        return _allies;
    }
}

public class FactionRelationshipManager : MonoBehaviour
{
    //Declarations
    private List<FactionInfo> _factionsList;
    [SerializeField] private bool _isDebugActive = false;




    //Monobehaviors
    private void Awake()
    {
        if (_factionsList == null)
            _factionsList = new List<FactionInfo>();
    }




    //Internal Utils
    private FactionInfo GetFactionInfo(string factionName)
    {
        foreach(FactionInfo faction in _factionsList)
        {
            if (factionName == faction.GetName())
                return faction;
        }

        STKDebugLogger.LogWarning($"Attempted to retieve nonexistent faction {factionName}. Returning  default Faction");
        return default;
    }





    //Getters, Setters, & Commands
    public bool DoesFactionExist(string factionName)
    {
        foreach (FactionInfo faction in _factionsList)
        {
            if (faction.GetName() == factionName)
                return true;
        }

        return false;
    }

    public void AddFaction(string factionName)
    {
        if (DoesFactionExist(factionName) == false)
        {
            FactionInfo newFaction = new FactionInfo(factionName);
            _factionsList.Add(newFaction);
        }


    }

    public void AddEnemyToFaction(string enemyName, string faction)
    {
        if (DoesFactionExist(enemyName) && DoesFactionExist(faction))
            GetFactionInfo(faction).GetEnemies().Add(enemyName);
    }

    public void AddAllyToFaction(string allyName, string faction)
    {
        if (DoesFactionExist(allyName) && DoesFactionExist(faction))
            GetFactionInfo(faction).GetAllies().Add(allyName);
    }

    public void RemoveEnemyFromFaction(string enemyName, string faction)
    {
        if (DoesFactionExist(enemyName) && DoesFactionExist(faction))
            GetFactionInfo(faction).GetEnemies().Remove(enemyName);
    }

    public void RemoveAllyFromFaction(string allyName, string faction)
    {
        if (DoesFactionExist(allyName) && DoesFactionExist(faction))
            GetFactionInfo(faction).GetEnemies().Remove(allyName);
    }

    public bool IsTargetFactionMyEnemy(string targetFaction, string selfFaction)
    {
        return GetFactionInfo(selfFaction).GetEnemies().Contains(targetFaction);
    }

    public bool IsTargetFactionMyAlly(string targetFaction, string selfFaction)
    {
        return GetFactionInfo(selfFaction).GetAllies().Contains(targetFaction);
    }


    //Debugging
    public void LogFactions()
    {
        
        string factionLog = "====== Factions: ======\n";

        foreach (FactionInfo faction in _factionsList)
        {
            factionLog += "-------------------";
            factionLog += $"Name: {faction.GetName()} \n ";

            factionLog += "Enemies: \n";
            factionLog += BuildLogOfCollectionContents(faction.GetEnemies());

            factionLog += "Allies: \n";
            factionLog += BuildLogOfCollectionContents(faction.GetAllies());
        }

        factionLog += "====== End of Factions ======";

        STKDebugLogger.LogStatement(_isDebugActive, factionLog);
    }

    private string BuildLogOfCollectionContents(List<string> collection)
    {
        string log = "";

        foreach (string item in collection)
            log += item + ", ";

        log += "\n";
        return log;
    }

}
