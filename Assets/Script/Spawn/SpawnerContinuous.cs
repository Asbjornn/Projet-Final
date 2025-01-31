using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

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
    public FragmentManager fragmentManager;
    public EventManager eventManager;
    public StatGameEnd statEndScript;

    [Header("List")]
    public List<GameObject> enemies;
    public List<GameObject> enemiesSpawned;
    public List<GameObject> enemiesCross;
    public List<int> enemyHpByWaves;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveID = 0;
        textNumberWave.text = $"Vague {waveID + 1}";
        waveTimer = waveManager.waves[waveID].waveTime;
        spawnInterval = waveManager.waves[waveID].waveInterval;
        eventManager.StartEvent();
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
                if(waveID % 5 == 3 || waveID % 5 == 4)
                {
                    StartCoroutine(SpawnEnemy(0));
                }
                else if(waveID % 5 == 1 || waveID % 5 == 2)
                {
                    StartCoroutine(SpawnEnemy(1));
                }
                else
                {
                    StartCoroutine(SpawnEnemy(2));
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
                if(waveID >= waveManager.waves.Count -1)
                {
                    //FIN DU GGWP
                    waveManager.endGame.Invoke();
                    waveManager.waveEnd = true;
                }
                else
                {
                    //FIN DE LA VAGUE
                    waveManager.endWaveEvent.Invoke();
                    waveManager.waveEnd = true;
                }
            }
        }
    }

    public IEnumerator SpawnEnemy(int number)
    {
        //Instantie un croix à un endroit random
        //attend la fin de son animation
        //instantie un enemy à cette même position

        Vector2 randomPos = new(Random.Range(-sizeSpawn.x / 2, sizeSpawn.x / 2), Random.Range(-sizeSpawn.y / 2, sizeSpawn.y / 2));
        GameObject newCross = Instantiate(cross, randomPos, Quaternion.identity);
        enemiesCross.Add(newCross);
        yield return new WaitForSeconds(0.83f);
        int randomID = Random.Range(0, enemies.Count - number);
        GameObject newEnemy = Instantiate(enemies[randomID], newCross.transform.position, Quaternion.identity);
        enemiesSpawned.Add(newEnemy);
        enemiesCross.Remove(newEnemy);
        Destroy(newCross);
    }

    public void WaveEnd()
    {
        //Actualise le texte pour le numéro de la wave
        //Détruis tous les ennemis restant à la fin
        //Détruis tous les fragments restant à la fin

        StopAllCoroutines();

        titleUIPannel.text = $"Vague {waveID + 1} finie";

        foreach(GameObject ene in enemiesSpawned)
        {
            Destroy( ene );
        }
        enemiesSpawned.Clear();

        foreach (GameObject ene in enemiesCross)
        {
            Destroy(ene);
        }
        enemiesCross.Clear();

        fragmentManager.CleanArena();
    }

    public void NextWave()
    {
        //imcrémente le nombre de vague
        //replace le joueur au milieu
        //reset le timer

        waveID++;
        textNumberWave.text = $"Vague {waveID + 1}";
        player.position = new Vector3(0,0,0);
        waveManager.waveEnd = false;
        waveTimer = waveManager.waves[waveID].waveTime;
    }

    public void CheckIfNull()
    {
        //vérifie si il y a un élément vide dans la liste pour le retirer

        for(int i = 0; i < enemiesSpawned.Count; i++)
        {
            if( enemiesSpawned[i] == null )
            {
                enemiesSpawned.RemoveAt(i);
            }
        }
    }

    public void RemoveEnemy(GameObject go)
    {
        enemiesSpawned.Remove(go);
        bool added = false;
        if (!added)
        {
            //print("je suis lue");
            statEndScript.enemiesNumber++;
            added = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, sizeSpawn);
    }
}
