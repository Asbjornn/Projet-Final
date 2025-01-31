using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats stats;
    public Slider healthSlider;
    public TextMeshProUGUI textLife;
    public UnityEvent eventDie;

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
            Die();
        }

        if(stats.currentHealth > stats.maxHealth)
        {
            stats.currentHealth = stats.maxHealth;
        }
    }

    public void UpdateHealth()
    {
        textLife.text = $"{stats.currentHealth} PV";
        healthSlider.value = stats.currentHealth;
    }

    public void AddHealth(float amount)
    {
        print("Modification des hp de " +  amount); 
        stats.maxHealth += amount;
        healthSlider.maxValue = stats.maxHealth;
        stats.currentHealth += amount;
    }

    public void Die()
    {
        eventDie.Invoke();
    }
}
