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
    [Header("Settings")]
    [SerializeField] private string _independentFactionName = "Independent";
    [SerializeField] private string _enforcersFactionName = "Enforcers";
    [SerializeField] private string _piratesFactionName = "Pirates";
    private List<FactionInfo> _factionsList;


    [Header("Debug Utils")]
    [SerializeField] private bool _isDebugActive = false;

    [Header("Testing Values")]
    [SerializeField] private string _factionNameDebug;
    [SerializeField] private string _otherFactionNameDebug;
    [SerializeField] private string _factionAllyNameDebug;
    [SerializeField] private string _factionEnemyNameDebug;

    [Header("Commands")]
    [SerializeField] private bool _logFactionsCmd;
    [SerializeField] private bool _isFactionEnemyOfOtherCmd;
    [SerializeField] private bool _isFactionAllyOfOtherCmd;
    [SerializeField] private bool _addFactionCmd;
    [SerializeField] private bool _removeFactionCmd;
    [SerializeField] private bool _addAllyCmd;
    [SerializeField] private bool _removeAllyCmd;
    [SerializeField] private bool _AddEnemyCmd;
    [SerializeField] private bool _removeEnemyCmd;




    //Monobehaviors
    private void Awake()
    {
        if (_factionsList == null)
            _factionsList = new List<FactionInfo>();

        InitializeBaseFactions();
    }

    private void Update()
    {
        if (_isDebugActive)
            ListenForDebugCommands();
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

    private void InitializeBaseFactions()
    {
        AddFaction(_independentFactionName);
        AddFaction(_enforcersFactionName);
        AddFaction(_piratesFactionName);


        //Relationships
        AddEnemyToFaction(_piratesFactionName, _independentFactionName);
        AddEnemyToFaction(_piratesFactionName, _enforcersFactionName);

        AddEnemyToFaction(_independentFactionName, _piratesFactionName);
        AddEnemyToFaction(_enforcersFactionName, _piratesFactionName);

        if (_isDebugActive)
            LogFactions();

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
            GetFactionInfo(faction).GetAllies().Remove(allyName);
    }

    public bool IsOtherFactionMyEnemy(string otherFaction, string selfFaction)
    {
        return GetFactionInfo(selfFaction).GetEnemies().Contains(otherFaction);
    }

    public bool IsOtherFactionMyAlly(string otherFaction, string selfFaction)
    {
        return GetFactionInfo(selfFaction).GetAllies().Contains(otherFaction);
    }

    public void RemoveFaction(string factionName)
    {
        if (DoesFactionExist(factionName))
        {
            foreach (FactionInfo faction in _factionsList)
            {
                if (faction.GetName() != factionName)
                {
                    if (faction.GetAllies().Contains(factionName))
                        faction.GetAllies().Remove(factionName);

                    if (faction.GetEnemies().Contains(factionName))
                        faction.GetEnemies().Remove(factionName);
                }
            }

            _factionsList.Remove(GetFactionInfo(factionName));
        }
    }


    //Debugging
    private void ListenForDebugCommands()
    {
        if (_logFactionsCmd)
        {
            _logFactionsCmd = false;
            LogFactions();
        }

        if (_addFactionCmd)
        {
            _addFactionCmd = false;
            AddFaction(_factionNameDebug);
            LogCommandRecieved("Add Faction");
        }

        if (_removeFactionCmd)
        {
            _removeFactionCmd = false;
            //Remove Faction
            LogCommandRecieved("Remove Faction");
            RemoveFaction(_factionNameDebug);
        }

        if (_addAllyCmd)
        {
            _addAllyCmd = false;
            AddAllyToFaction(_factionAllyNameDebug, _factionNameDebug);
            LogCommandRecieved("Add Ally");
        }

        if (_removeAllyCmd)
        {
            _removeAllyCmd = false;
            RemoveAllyFromFaction(_factionAllyNameDebug, _factionNameDebug);
            LogCommandRecieved("Remove Ally");
        }

        if (_AddEnemyCmd)
        {
            _AddEnemyCmd = false;
            AddEnemyToFaction(_factionEnemyNameDebug, _factionNameDebug);
            LogCommandRecieved("Add Enemy");
        }

        if (_removeEnemyCmd)
        {
            _removeEnemyCmd = false;
            RemoveEnemyFromFaction(_factionEnemyNameDebug, _factionNameDebug);
            LogCommandRecieved("Remove Enemy");
        }
        
        if (_isFactionAllyOfOtherCmd)
        {
            _isFactionAllyOfOtherCmd = false;
            STKDebugLogger.LogStatement(_isDebugActive, $"Is {_otherFactionNameDebug} ally of {_factionNameDebug}: " +
                                     $"{IsOtherFactionMyAlly(_otherFactionNameDebug, _factionNameDebug)}");
        }

        if (_isFactionEnemyOfOtherCmd)
        {
            _isFactionEnemyOfOtherCmd = false;
            STKDebugLogger.LogStatement(_isDebugActive, $"Is {_otherFactionNameDebug} enemy of {_factionNameDebug}: " +
                                     $"{IsOtherFactionMyEnemy(_otherFactionNameDebug, _factionNameDebug)}");
        }

    }


    public void LogFactions()
    {
        
        string factionLog = "====== Factions: ======\n";

        foreach (FactionInfo faction in _factionsList)
        {
            factionLog += "-------------------\n";
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

    private void LogCommandRecieved(string commandDescription)
    {
        STKDebugLogger.LogStatement(_isDebugActive, $"{commandDescription} command recieved");
    }

}
