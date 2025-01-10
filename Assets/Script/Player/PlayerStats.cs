using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharactersStats currentCharacter;

    public int maxHealth;
    public int currentHealth;
    public int degatPourcentage;
    public int degatBrut;
    public int vitesseAttaque;
    public int portee;
    public int armure;
    public int movementSpeed;

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
}
