using UnityEngine;
using TMPro;

public class KillEnemies : MonoBehaviour
{
    public SpawnerContinuous spawnerScript;
    public WaveManager waveManager;
    public EventManager eventManager;
    public float enemiesForWin;
    public float currentNumberOfEnemies;
    public TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemiesForWin = Mathf.Round(waveManager.waves[spawnerScript.waveID].waveInterval / 45);
        currentNumberOfEnemies = enemiesForWin;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentNumberOfEnemies <= 0)
        {
            eventManager.VictoryEvent();
        }
    }
}
