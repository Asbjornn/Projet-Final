using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public CharactersStats currentCharacter;
    public Animator animator;

    [Header("Stats")]
    public int maxHealth;
    public int currentHealth;
    public int degatPourcentage;
    public int degatBrut;
    public int vitesseAttaque;
    public int portee;
    public int armure;
    public int movementSpeed;

    [Space(5)]
    [Header("----------------------------------------------------------------")]
    [Space(5)]
    [Header("Stats UI")]
    public TextMeshProUGUI maxHealthUI;
    //public TextMeshProUGUI currentHealthUI;
    public TextMeshProUGUI degatPourcentageUI;
    public TextMeshProUGUI degatBrutUI;
    public TextMeshProUGUI vitesseAttaqueUI;
    public TextMeshProUGUI porteeUI;
    public TextMeshProUGUI armureUI;
    public TextMeshProUGUI movementSpeedUI;

    public void Update()
    {
        UpdateStatsUI();
    }

    public void InitialiseStats()
    {
        maxHealth = currentCharacter.maxHealth;
        currentHealth = currentCharacter.currentHealth;
        degatPourcentage = currentCharacter.degatPourcentage;
        degatBrut = currentCharacter.degatBrut;
        vitesseAttaque = currentCharacter.vitesseAttaque;
        portee = currentCharacter.portee;
        armure = currentCharacter.armure;   
        movementSpeed = currentCharacter.movementSpeed;

        currentHealth = maxHealth;
    }

    public void UpdateStatsUI()
    {
        maxHealthUI.text = $"Max Health {maxHealth}";
        degatPourcentageUI.text = $"Damage % {degatPourcentage}";
        degatBrutUI.text = $"Damage Brut {degatBrut}";
        vitesseAttaqueUI.text = $"Attack Speed {vitesseAttaque}";
        porteeUI.text = $"Range {portee}";
        armureUI.text = $"Armor {armure}";
        movementSpeedUI.text = $"Move Speed {movementSpeed}";
    }

    public void TakeDamage(int amount)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= amount;
    }
}
