using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class Spawner : MonoBehaviour
{
    [Tooltip("How fast boxes will be spawned")]
    public float spawnDelay = 2f;
    [Tooltip("Limit spawning if there is max boxes on scene")]
    public int maxBoxesOnScene = 10;

    private float nextSpawnTime = 0;    // Time for next spawn
    private BoxCollider2D spawningCollider; // Area that spawns boxes on random point inside collider.
    private ObjectPooler objectPooler; // Object pooling

    private void Awake()
    {
        spawningCollider = GetComponent<BoxCollider2D>();
        objectPooler = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        if (ShouldSpawn())
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        nextSpawnTime = Time.time + spawnDelay; // Get next spawn time

        // Random index
        int index = GetRandomBoxIndex();
        // Random position inside collider
        Vector3 pos = GetRandomBoxPosition();

        // Spawn box under box parent object
        GameObject box = objectPooler.GetPooledObject(objectPooler.itemsToPool[index].objectToPool.tag);
        if (box != null)
        {
            box.transform.position = pos;
            box.transform.rotation = Quaternion.identity;
            box.SetActive(true);
        }
    }

    private int GetRandomBoxIndex()
    {
        // Return random index from 0 to 2 (can be blue or red box)
        return Random.Range(0, objectPooler.itemsToPool.Count);
    }

    private Vector3 GetRandomBoxPosition()
    {
        // Get random x value inside colliders width
        float x = Random.Range(spawningCollider.bounds.min.x, spawningCollider.bounds.max.x);

        // y and z value can be same as Spawner game object
        return new Vector3(x, transform.position.y, transform.position.z);
    }

    private bool ShouldSpawn()
    {
        // Can spawn new box only when time is up and there is less than max boxes under parent
        if(Time.time >= nextSpawnTime)
        {
            return objectPooler.NumberOfActiveObjects() < maxBoxesOnScene;
        }
        return false;
    }
}
