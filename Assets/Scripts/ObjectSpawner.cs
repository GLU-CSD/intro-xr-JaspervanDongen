using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;       // Het object dat je wilt spawnen
    public float spawnInterval = 5f;       // Interval tussen spawns
    private float lastSpawnTime;           // Tijd van de laatste spawn

    void Update()
    {
        if (Time.time >= lastSpawnTime + spawnInterval)
        {
            SpawnObject();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnObject()
    {
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("Object to spawn is not set.");
        }
    }
}

 