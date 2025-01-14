using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats stats;
    public Slider healthSlider;
    public TextMeshProUGUI textLife;

    private void Start()
    {
        healthSlider.maxValue = stats.maxHealth;
        textLife.text = $"{stats.maxHealth}";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth(); 
        if (stats.currentHealth <= 0)
        {
            print("Meurt");
            //Die();
        }
    }

    public void UpdateHealth()
    {
        textLife.text = $"{stats.currentHealth}";
        healthSlider.value = stats.currentHealth;
    }
}
