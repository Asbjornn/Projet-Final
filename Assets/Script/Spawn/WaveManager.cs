using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public SpawnerContinuous spawner;

    public List<GameObject> environements;
    public List<GameObject> environementNotPicked;
    public List<Waves> waves;

    public bool newEnvironement = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        environementNotPicked = environements;
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
        newEnvironement = true;
        int id = 0;
        for(id = 0; id < environements.Count; id++)
        {
            if (environements[id].activeSelf)
            {
                environements[id].SetActive(false);
                environementNotPicked.RemoveAt(id);
            }
        }

        int randomID = Random.Range(0, environementNotPicked.Count);
        environementNotPicked[randomID].SetActive(true);
    }
}
