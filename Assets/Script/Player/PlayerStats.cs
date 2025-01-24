using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public CharactersStats currentCharacter;
    public Animator animator;

    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float damagePercentage;
    public float damageBrut;
    public float attackSpeed;
    public float range;
    public float armor;
    public float movementSpeed;

    [Space(5)]
    [Header("----------------------------------------------------------------")]
    [Space(5)]
    [Header("Stats UI")]
    public TextMeshProUGUI maxHealthUI;
    //public TextMeshProUGUI currentHealthUI;
    public TextMeshProUGUI damagePercentageUI;
    public TextMeshProUGUI damageBrutUI;
    public TextMeshProUGUI attackSpeedUI;
    public TextMeshProUGUI rangeUI;
    public TextMeshProUGUI armorUI;
    public TextMeshProUGUI movementSpeedUI;

    [Space(5)]
    [Header("----------------------------------------------------------------")]
    [Space(5)]
    [Header("Scipt for stat")]
    public Shoot shootScript;
    public PlayerHealth health;

    public void Update()
    {
        UpdateStatsUI();
    }

    public void InitialiseStats()
    {
        maxHealth = currentCharacter.maxHealth;
        currentHealth = currentCharacter.currentHealth;
        damagePercentage = currentCharacter.damagePercentage;
        damageBrut = currentCharacter.damageBrut;
        attackSpeed = currentCharacter.attackSpeed;
        range = currentCharacter.range;
        armor = currentCharacter.armor;   
        movementSpeed = currentCharacter.movementSpeed;

        currentHealth = maxHealth;
    }

    public void UpdateStatsUI()
    {
        maxHealthUI.text = $"Max Health {maxHealth}";
        damagePercentageUI.text = $"Damage {damagePercentage} %";
        damageBrutUI.text = $"Damage Brut {damageBrut}";
        attackSpeedUI.text = $"Attack Speed {attackSpeed * -1} %";
        rangeUI.text = $"Range {range}";
        armorUI.text = $"Armor {armor}";
        movementSpeedUI.text = $"Move Speed {movementSpeed}";
    }

    public void TakeDamage(float amount)
    {
        //faire que l'ennemi te fais au minimum 10% de ton armure
        animator.SetTrigger("Hurt");
        currentHealth -= Mathf.Max(amount / 10, amount - armor);
    }

    public void UpdateStat(string statName, float value)
    {
        switch(statName)
        {
            case "maxHealth":
                //maxHealth += value;
                if(value > 0)
                {
                    health.AddHealth(value);
                }
                else if(health.healthSlider.value - value > maxHealth)
                {
                    health.healthSlider.value = maxHealth;
                }
                else if(health.healthSlider.value - value <= 0)
                {
                    maxHealth = 1;
                    health.healthSlider.value = maxHealth;
                }
                break;
            case "damagePercentage":
                damagePercentage += value;
                break;
            case "damageBrut":
                damageBrut += value;
                break;
            case "attackSpeed":
                attackSpeed -= value;
                break;
            case "range":
                range += value;
                break;
            case "armor":
                armor += value;
                break;
            case "movementSpeed":
                movementSpeed += value;
                break;
            default:
                break;
        }
    }
}
