using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceTracker : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isDebugActive = true;
    private Dictionary<int, AbstractShip> _aliveShips; //keys are instance IDs
    private Dictionary<int, ProjectileBehavior> _aliveProjectiles; //keys are instance IDs
    [SerializeField] private bool _logShipsCmd;
    [SerializeField] private bool _logProjectilesCmd;

    public delegate void InstanceTrackerEvent(int ID);
    public event InstanceTrackerEvent OnShipSpawned;
    public event InstanceTrackerEvent OnShipDied;
    public event InstanceTrackerEvent OnProjectileSpawned;
    public event InstanceTrackerEvent OnProjectileDied;




    //Monobehaviuors
    private void Awake()
    {
        _aliveShips = new Dictionary<int, AbstractShip>();
        _aliveProjectiles = new Dictionary<int, ProjectileBehavior>();
    }

    private void Update()
    {
        if (_isDebugActive)
            ListenForDebugCommands();
    }





    //Internal Utils




    //Getters Setters & Commands
    public AbstractShip GetShipWithID(int instanceID)
    {
        if (_aliveShips.ContainsKey(instanceID))
            return _aliveShips[instanceID];
        else return null;
    }

    public ProjectileBehavior GetProjectileWithID(int instanceID)
    {
        if (_aliveProjectiles.ContainsKey(instanceID))
            return _aliveProjectiles[instanceID];
        else return null;
    }

    public void AddShip(AbstractShip newShip)
    {
        if (newShip != null)
        {
            int newShipID = newShip.GetInstanceID();

            if (_aliveShips.ContainsKey(newShipID) == false)
            {
                _aliveShips.Add(newShipID, newShip);
                LogStatement($"New ship added to tracker: {newShip.GetName()}, ID: {newShipID}");
                OnShipSpawned?.Invoke(newShipID);
            }
                
            else LogWarning($"Attempted to add ship instance {newShipID}({newShip.GetName()}) to the Instance Tracker when it's already been added");
        }
    }

    public void RemoveShip(int ID)
    {
        if (_aliveShips.ContainsKey(ID))
        {
            OnShipDied?.Invoke(ID);
            _aliveShips.Remove(ID);
            LogStatement($"Removed ship with ID {ID} from tracker");
        }
            
        else LogWarning($"Attempted to remove ship instance {ID} but it isnt being tracked by the instance tracker");
    }

    public void AddProjectile(ProjectileBehavior newProjectile)
    {
        if (newProjectile != null)
        {
            int newProjectileID = newProjectile.GetInstanceID();

            if (_aliveProjectiles.ContainsKey(newProjectileID) == false)
            {
                _aliveProjectiles.Add(newProjectileID, newProjectile);
                OnProjectileSpawned?.Invoke(newProjectileID);
            }
                
            else LogWarning($"Attempted to add projectile instance {newProjectileID}({newProjectile.gameObject.name}) to the Instance Tracker when it's already been added");
        }
    }

    public void RemoveProjectile(int ID)
    {
        if (_aliveProjectiles.ContainsKey(ID))
        {
            OnProjectileDied?.Invoke(ID);
            _aliveProjectiles.Remove(ID);
        }
            
        else LogWarning($"Attempted to remove projectile instance {ID} but it isnt being tracked by the instance tracker");
    }



    //Debugging Uitls
    private void ListenForDebugCommands()
    {
        if (_logShipsCmd)
        {
            _logShipsCmd = false;
            LogShips();
        }

        if (_logProjectilesCmd)
        {
            _logProjectilesCmd = false;
            LogProjectiles();
        }
    }

    private void LogStatement(string statement)
    {
        SullysToolkit.STKDebugLogger.LogStatement(_isDebugActive, statement);
    }

    private void LogWarning(string warning)
    {
        SullysToolkit.STKDebugLogger.LogWarning(warning);
    }

    private void LogShips()
    {
        string shipLog = "========= Ship Log =========\n";
        foreach (KeyValuePair<int,AbstractShip> entry in _aliveShips)
        {
            shipLog += "--------------------------\n";
            shipLog += $"ID: {entry.Key},\nShipName: {entry.Value.GetName()}\n";
        }



        shipLog += "========= Ship Log End =========";
        LogStatement(shipLog);
    }

    private void LogProjectiles()
    {
        string projectileLog = "========= Projectiles Log =========\n";
        foreach (KeyValuePair<int, ProjectileBehavior> entry in _aliveProjectiles)
        {
            projectileLog += "--------------------------\n";
            projectileLog += $"ID: {entry.Key},\nOwnerID: {entry.Value.GetOwnerID()}\n";
        }



        projectileLog += "========= Projectiles Log End =========";
        LogStatement(projectileLog);
    }

    private void LogAsteroids()
    {

    }




}
