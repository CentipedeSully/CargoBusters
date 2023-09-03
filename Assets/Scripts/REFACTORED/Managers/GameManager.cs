using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SullysToolkit;

public class GameManager : MonoSingleton<GameManager>
{
    //Declarations
    [SerializeField] private List<string> _damageableTagsList;
    [SerializeField] private InputReader _inputReaderReference;
    [SerializeField] private WeaponFactory _weaponFactoryReference;
    [SerializeField] private Transform _projectileContainer;
    [SerializeField] private Transform _asteroidContainer;
    [SerializeField] private Transform _shipContainer;



    //Monobehaviours
    //...




    //Utils
    protected override void InitializeAdditionalFields()
    {
        Physics2D.queriesHitTriggers = true;
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        if (_inputReaderReference == null)
            _inputReaderReference = GetComponent<InputReader>();
        if (_weaponFactoryReference == null)
            _weaponFactoryReference = GetComponent<WeaponFactory>();
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

    


    //Getters & Setters
    public InputReader GetInputReader()
    {
        return _inputReaderReference;
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

    public List<IProjectile> GetAllProjectilesInScene()
    {
        return GetReferencesFromContainer<IProjectile>(_projectileContainer);
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
}
