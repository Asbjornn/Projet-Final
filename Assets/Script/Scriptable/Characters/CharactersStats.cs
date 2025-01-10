using UnityEngine;

[CreateAssetMenu(fileName = "CharactersStats", menuName = "Scriptable Characters/CharactersStats")]
public class CharactersStats : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;
    public int degatPourcentage;
    public int degatBrut;
    public int vitesseAttaque;
    public int portee;
    public int armure;
    public int movementSpeed;
}
