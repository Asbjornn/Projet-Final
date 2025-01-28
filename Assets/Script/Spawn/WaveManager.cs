using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [Header("Script")]
    public SpawnerContinuous spawner;
    public EventManager eventManager;

    [Header("List")]
    public List<GameObject> environements;
    public List<GameObject> environementNotPicked;
    public List<Waves> waves;
    public List<ListEnemies> enemiesList;
    //[HideInInspector]
    public List<ListEnemies> enemiesAlreadyThere;

    [Header("Bool")]
    public bool newEnvironement = false;
    public bool waveEnd = false;

    [Header("Events")]
    public UnityEvent endWaveEvent;
    public UnityEvent nextWaveEvent;
    public UnityEvent endGame;

    [System.Serializable]
    public class ListEnemies { public List<GameObject> enemies; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        environementNotPicked = new List<GameObject>(environements);
        enemiesAlreadyThere = new List<ListEnemies>(enemiesList);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawner.waveID % 5 == 0 && !newEnvironement)
        {
            NewEnvironement();
        }
        else if(spawner.waveID % 5 != 0)
        {
            newEnvironement = false;
        }
    }

    public void NewEnvironement()
    {
        //Retire un environnement de la nouvelle liste
        //pour choisir un al�atoirement parmis les restants

        newEnvironement = true;
        for(int id = 0; id < environements.Count; id++)
        {
            if (environements[id].activeSelf)
            {
                environements[id].SetActive(false);
            }
        }

        int randomID = Random.Range(0, environementNotPicked.Count);
        environementNotPicked[randomID].SetActive(true);
        spawner.enemies = enemiesAlreadyThere[randomID].enemies;
        enemiesAlreadyThere.RemoveAt(randomID);
        environementNotPicked.RemoveAt(randomID);
    }

    public void NextWaveEvent()
    {
        nextWaveEvent.Invoke();
    }
}
