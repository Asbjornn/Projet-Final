using UnityEngine;
using System.Collections.Generic;

public class SpawnerContinuous : MonoBehaviour
{
    public Vector3 sizeSpawn;

    public float spawnInterval;
    public float waveTimer;
    public int waveID;

    public List<GameObject> enemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        int randomID = Random.Range(0, enemies.Count);
        Vector2 randomPos = new Vector2(Random.Range(-sizeSpawn.x/2, sizeSpawn.x/2), Random.Range(-sizeSpawn.y / 2, sizeSpawn.y / 2));
        Instantiate(enemies[randomID], randomPos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, sizeSpawn);
    }
}
