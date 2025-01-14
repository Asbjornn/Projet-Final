using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SpawnerContinuous : MonoBehaviour
{
    [Header("SpawnerData")]
    public Vector3 sizeSpawn;
    public float spawnInterval;
    public float spawnTimer;
    public float waveTimer;
    public int waveID;

    [Header("UI")]
    public GameObject waveUI;
    public TextMeshProUGUI textNumberWave;
    public TextMeshProUGUI titleUIPannel;

    [Header("Others")]
    public Transform player;

    [Header("List")]
    public List<GameObject> enemies;
    public List<GameObject> enemiesSpawned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = spawnInterval;
        waveID++;
        textNumberWave.text = $"Vague {waveID}";
    }

    // Update is called once per frame
    void Update()
    {
        if(waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            CheckIfNull();

            if (spawnTimer < 0)
            {
                SpawnEnemies(1);
                spawnTimer = spawnInterval;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
        else
        {
            WaveEnd();
        }
    }


    public void SpawnEnemies(int number)
    {
        int randomID = Random.Range(0, enemies.Count - number);
        Vector2 randomPos = new Vector2(Random.Range(-sizeSpawn.x/2, sizeSpawn.x/2), Random.Range(-sizeSpawn.y / 2, sizeSpawn.y / 2));
        GameObject newEnemy = Instantiate(enemies[randomID], randomPos, Quaternion.identity);
        enemiesSpawned.Add(newEnemy);
    }

    public void WaveEnd()
    {
        titleUIPannel.text = $"Vague {waveID} finie";

        waveUI.SetActive(true);

        foreach(GameObject ene in enemiesSpawned)
        {
            Destroy( ene );
        }
        enemiesSpawned.Clear();

        //Apparition écran entre les manches
        //Récupération gold
    }

    public void NextWave()
    {
        waveID++;
        textNumberWave.text = $"Vague {waveID}";
        player.position = new Vector3(0,0,0);
        waveTimer = 10;
    }

    public void CheckIfNull()
    {
        for(int i = 0; i < enemiesSpawned.Count; i++)
        {
            if( enemiesSpawned[i] == null )
            {
                enemiesSpawned.RemoveAt(i);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, sizeSpawn);
    }
}
