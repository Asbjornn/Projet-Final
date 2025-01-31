using TMPro;
using UnityEngine;

public class StatGameEnd : MonoBehaviour
{
    [Header("Scripts")]
    public SpawnerContinuous spawnerScript;
    public WaveManager waveScipt;
    public InventoryUI inventoryUI;
    public FragmentManager fragmentManager;

    [Header("UI")]
    public TextMeshProUGUI wave;
    public TextMeshProUGUI ennemiesBeated;
    public TextMeshProUGUI itemBuyed;
    public TextMeshProUGUI fragmentsCollctedUI;

    [Header("Stats")]
    public int enemiesNumber;
    public int fragmentsCollected;

    private void Start()
    {
        GameEndedStat();
    }

    public void GameEndedStat()
    {
        wave.text = $"Fin à la vague {spawnerScript.waveID + 1}";
        itemBuyed.text = $"Nombre d'objets achetés : {inventoryUI.itemsIninvetory.Count}";
        ennemiesBeated.text = $"Ennemies battues : {enemiesNumber}";
        fragmentsCollctedUI.text = $"Fragments collectés : {fragmentsCollected}";
    }
}
