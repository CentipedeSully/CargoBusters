using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SullysToolkit;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{
    //Declarations
    [SerializeField] private List<string> _damageableTagsList;
    [SerializeField] private InputReader _inputReaderReference;
    [SerializeField] private ShipFactory _shipFactoryReference;
    [SerializeField] private WeaponFactory _weaponFactoryReference;
    [SerializeField] private UiManager _UiManagerReference;
    [SerializeField] private PlayerManager _playerManagerReference;
    [SerializeField] private ShipOccupancyManager _shipOccupierReference;
    [SerializeField] private CameraController _cameraControllerReference;
    [SerializeField] private Transform _projectileContainer;
    [SerializeField] private Transform _asteroidContainer;
    [SerializeField] private Transform _shipContainer;



    //Monobehaviours
    //...




    //Utils
    protected override void InitializeAwakeUtils()
    {
        //Allows for Laser beams to hit projectiles. Projectile colliders are triggers. 
        Physics2D.queriesHitTriggers = true;

        InitializeReferences();
    }

    private void InitializeReferences()
    {

    }

    private List<T> GetReferencesFromContainer<T>(Transform container) 
    {
        if (container == null)
            return null;
        else if (container.childCount < 1)
            return new List<T>();

        List<T> componentsInTransform = new List<T>();

        for (int i = 0; i < container.childCount; i++)
        {
            T componentReference = container.GetChild(i).GetComponent<T>();
            if (componentReference != null)
                componentsInTransform.Add(componentReference);
        }

        return componentsInTransform;
    }

    private List<GameObject> GetGameObjectsFromComponentsList<T>(List<T> componentList) where T: Component
    {
        List<GameObject> returnObjectsList = new List<GameObject>();

        foreach (T component in componentList)
        {
            if (component != null)
                returnObjectsList.Add(component.gameObject);
        }

        return returnObjectsList;
    }
    


    //Getters & Setters
    public UiManager GetUiManager()
    {
        return _UiManagerReference;
    }

    public ShipOccupancyManager GetShipOccupancyManager()
    {
        return _shipOccupierReference;
    }

    public CameraController GetCameraController()
    {
        return _cameraControllerReference;
    }

    public InputReader GetInputReader()
    {
        return _inputReaderReference;
    }

    public PlayerManager GetPlayerManager()
    {
        return _playerManagerReference;
    }

    public Transform GetShipContainer()
    {
        return _shipContainer;
    }

    public Transform GetAsteroidContainer()
    {
        return _asteroidContainer;
    }

    public List<string> GetDamageableTagsList()
    {
        return _damageableTagsList;
    }

    public ShipFactory GetShipFactory()
    {
        return _shipFactoryReference;
    }

    public WeaponFactory GetWeaponsFactory()
    {
        return _weaponFactoryReference;
    }

    public Transform GetProjectileContainer()
    {
        return _projectileContainer;
    }

    public List<AbstractShip> GetAllShipsInScene()
    {
        return GetReferencesFromContainer<AbstractShip>(_shipContainer);
    }

    public List<ProjectileBehavior> GetAllProjectilesInScene()
    {
        return GetReferencesFromContainer<ProjectileBehavior>(_projectileContainer);
    }

    public GameObject FindShipWithID(int instanceID)
    {
        foreach (AbstractShip shipReference in GetAllShipsInScene())
        {
            IDamageable damageableReference = shipReference.GetComponent<IDamageable>();
            if (damageableReference?.GetInstanceID() == instanceID)
                return shipReference.gameObject;
        }

        return null;
    }

    public GameObject FindProjectileWithID(int instanceID)
    {
        Debug.Log($"Found Projectiles In Scene: {GetAllProjectilesInScene().Count}");

        foreach (IProjectile projectileRef in GetAllProjectilesInScene())
        {
            Debug.Log($"Projectile ID: {projectileRef.GetInstanceID()}");
            if (projectileRef.GetInstanceID() == instanceID)
                return projectileRef.GetGameObject();
        }

        return null;
    }

    public GameObject FindDamageableObjectWithID(int instanceID)
    {
        GameObject foundObject;

        //Check if the ID is among the ships
        foundObject = GameManager.Instance.FindShipWithID(instanceID);

        if (foundObject != null)
            return foundObject;


        //Check if the ID is among the Projectiles
        foundObject = GameManager.Instance.FindProjectileWithID(instanceID);

        if (foundObject != null)
            return foundObject;

        //object not found. return null
        Debug.LogWarning($"Game Manager cant find object w/ID {instanceID} among neither the ships nor projectiles. returning Null");
        return foundObject;
    }

    public List<GameObject> GetAllThings()
    {
        List<GameObject> everything = new List<GameObject>();
        everything.AddRange(GetGameObjectsFromComponentsList(GetAllShipsInScene()));
        everything.AddRange(GetGameObjectsFromComponentsList(GetAllProjectilesInScene()));

        return everything;
    }

    public GameObject FindObjectWithID(int instanceID)
    {
        GameObject foundObject;

        //Check if the ID is among the ships
        foundObject = GameManager.Instance.FindShipWithID(instanceID);

        if (foundObject != null)
            return foundObject;


        //Check if the ID is among the Projectiles
        foundObject = GameManager.Instance.FindProjectileWithID(instanceID);

        if (foundObject != null)
            return foundObject;

        //object not found. return null
        Debug.LogWarning($"Game Manager cant find object w/ID {instanceID} among neither the ships nor projectiles. returning Null");
        return foundObject;
    }

    
}
