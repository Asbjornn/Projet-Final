using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public SpawnerContinuous spawnerContinuous;
    public Slider sliderTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        sliderTime.value = spawnerContinuous.waveTimer;
    }

    public void UpdateTimer()
    {
        sliderTime.maxValue = spawnerContinuous.waveManager.waves[spawnerContinuous.waveID].waveTime;
    }
}
