using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SullysToolkit
{
    public class ObjectPooler : MonoSingleton<ObjectPooler>
    {
        private static int _defaultPopulationValue = 10;

        private static List<GameObject> _pooledObjects;

        private static GameObject _objectPoolerGameObject;


        protected override void InitializeAdditionalFields()
        {
            _objectPoolerGameObject = gameObject;
            _pooledObjects = new List<GameObject>();
        }


        public static void PoolObject(GameObject existingObject)
        {
            if (existingObject.activeSelf == true)
                existingObject.SetActive(false);

            ParentPoolerToGameObject(existingObject);

            _pooledObjects.Add(existingObject);
        }

        private static void ParentPoolerToGameObject(GameObject pooledObject)
        {
            ParentObjectToNewTransform(pooledObject, _objectPoolerGameObject.transform);

            Quaternion poolerRotation = Quaternion.Euler(_objectPoolerGameObject.transform.rotation.eulerAngles);

            pooledObject.transform.SetPositionAndRotation(_objectPoolerGameObject.transform.position, poolerRotation);
            //pooledObject.transform.position = _objectPoolerGameObject.transform.position;
        }


        public static void ParentObjectToNewTransform(GameObject childObject, Transform parentTransform)
        {
            childObject.transform.SetParent(parentTransform);
        }

        public static GameObject TakePooledGameObject(GameObject requestedPrefab)
        {
            //Populate the pool with an amount of the desired objects if none currently exist in the pool
            if (DoesObjectExistInPool(requestedPrefab) == false)
                AddPopulationToPool(requestedPrefab, _defaultPopulationValue);

            //Find a matching object to return and mark its location in the list.
            GameObject recycledGameObject = null;
            int removalIndex = -1;
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (_pooledObjects[i].CompareTag(requestedPrefab.tag))
                {
                    recycledGameObject = _pooledObjects[i].gameObject;
                    removalIndex = i;
                    //Debug.Log("Selected ObjectID: " + recycledGameObject.GetInstanceID());
                    break;
                }
            }

            _pooledObjects.RemoveAt(removalIndex);
            _pooledObjects.RemoveAt(removalIndex);

            //Debug.Log("Object Instance at removed position: " + _pooledObjects[removalIndex].GetInstanceID());


            //Return the object
            if (recycledGameObject != null)
                return recycledGameObject;

            else
            {
                Debug.LogError("Failed to return requested object from ObjectPooler: (" + requestedPrefab.name + "). Failed To Populate Pooler with requested object prfab.");
                return null;
            }

        }
        private static bool DoesInstanceExistInList(int instanceID)
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (instanceID == _pooledObjects[i].GetInstanceID())
                {
                    //_pooledObjects.RemoveAt(i);
                    return true;
                }
                    
            }
            return false;
        }

        private static void RemoveInstanceFromList(int instanceID)
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (instanceID == _pooledObjects[i].GetInstanceID())
                {
                    _pooledObjects.RemoveAt(i);
                }
            }
        }

        public static bool DoesObjectExistInPool(GameObject objectInQuestion)
        {
            foreach (GameObject pooledObject in _pooledObjects)
            {
                if (objectInQuestion.tag == pooledObject.tag)
                    return true;
            }

            return false;
        }

        private static void AddPopulationToPool(GameObject prefab, int amountToAdd)
        {
            int count = 0;
            while (count < amountToAdd)
            {
                GameObject newObject = Instantiate(prefab, _objectPoolerGameObject.transform);
                newObject.SetActive(false);
                PoolObject(newObject);
                count++;
            }
        }


    }

}
