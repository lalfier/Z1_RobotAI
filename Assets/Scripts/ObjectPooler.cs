using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Tooltip("Parent to store boxes in")]
    public Transform boxesParent;
    [Tooltip("List of items to pool")]
    public List<ObjectPoolItem> itemsToPool;

    private List<GameObject> pooledObjects; // Pooled objects in scene

    private void Start()
    {
        // On start pool number of objects as buffer
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool, boxesParent);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    /// <summary>
    /// Get pooled object that is not active or spawn another one.
    /// </summary>
    /// <param name="objectTag">Tag of object that we want to get.</param>
    /// <returns>Returns pooled object with appropriate tag.</returns>
    public GameObject GetPooledObject(string objectTag)
    {
        // Pool object form list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag.Equals(objectTag))
            {
                return pooledObjects[i];
            }
        }

        // Generate new pool object if needed
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag.Equals(objectTag))
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool, boxesParent);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Get number of active pooled objects in parent.
    /// </summary>
    /// <returns>Returns number of active pooled objects in parent.</returns>
    public int NumberOfActiveObjects()
    {
        int activeCount = 0;
        foreach (Transform box in boxesParent)
        {
            if (box.gameObject.activeSelf)
            {
                activeCount++;
            }
        }
        return activeCount;
    }
}

[Serializable]
public class ObjectPoolItem
{
    [Tooltip("Prefab to pool")]
    public GameObject objectToPool;
    [Tooltip("Start amount of objects to generate")]
    public int amountToPool;
    [Tooltip("Expand number of objects as needed")]
    public bool shouldExpand;
}
