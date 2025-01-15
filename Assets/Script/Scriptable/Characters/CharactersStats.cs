using UnityEngine;

[CreateAssetMenu(fileName = "CharactersStats", menuName = "Scriptable Characters/CharactersStats")]
public class CharactersStats : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;
    public float damagePercentage;
    public float damageBrut;
    public float attackSpeed;
    public float range;
    public float armor;
    public float movementSpeed;
}
