using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

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
    public GameObject cross;
    public WaveManager waveManager;

    [Header("List")]
    public List<GameObject> enemies;
    public List<GameObject> enemiesSpawned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveID = 0;
        textNumberWave.text = $"Vague {waveID + 1}";
        waveTimer = waveManager.waves[waveID].waveTime;
        spawnInterval = waveManager.waves[waveID].waveInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            CheckIfNull();

            if (spawnInterval < 0)
            {
                if(waveID % 3 == 0 || waveID % 4 == 0 || waveID % 5 == 0 && waveID !=0)
                {
                    StartCoroutine(SpawnEnemy(0));
                }
                else
                {
                    StartCoroutine(SpawnEnemy(1));
                }
                
                spawnInterval = waveManager.waves[waveID].waveInterval;
            }
            else
            {
                spawnInterval -= Time.deltaTime;
            }
        }
        else
        {
            if(!waveManager.waveEnd)
            {
                waveManager.endWaveEvent.Invoke();
                waveManager.waveEnd = true;
            }
        }
    }

    public IEnumerator SpawnEnemy(int number)
    {
        Vector2 randomPos = new Vector2(Random.Range(-sizeSpawn.x / 2, sizeSpawn.x / 2), Random.Range(-sizeSpawn.y / 2, sizeSpawn.y / 2));
        GameObject newCross = Instantiate(cross, randomPos, Quaternion.identity);
        yield return new WaitForSeconds(0.83f);
        int randomID = Random.Range(0, enemies.Count - number);
        GameObject newEnemy = Instantiate(enemies[randomID], newCross.transform.position, Quaternion.identity);
        enemiesSpawned.Add(newEnemy);
        Destroy(newCross);
    }

    /*public void SpawnEnemies(int number)
    {
        int randomID = Random.Range(0, enemies.Count - number);
        Vector2 randomPos = new Vector2(Random.Range(-sizeSpawn.x/2, sizeSpawn.x/2), Random.Range(-sizeSpawn.y / 2, sizeSpawn.y / 2));
        GameObject newEnemy = Instantiate(enemies[randomID], randomPos, Quaternion.identity);
        enemiesSpawned.Add(newEnemy);
    }*/

    public void WaveEnd()
    {
        titleUIPannel.text = $"Vague {waveID + 1} finie";

        //waveUI.SetActive(true);

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
        textNumberWave.text = $"Vague {waveID + 1}";
        player.position = new Vector3(0,0,0);
        waveManager.waveEnd = false;
        waveTimer = waveManager.waves[waveID].waveTime;
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
