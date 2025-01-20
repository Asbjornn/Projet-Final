using UnityEngine;

public class CreateText : MonoBehaviour
{
    public PlayerStats playerStats;
    public SpawnerContinuous spawner;
    public PlayerInventory playerInventory;
    string fileName;

    private void Start()
    {
        fileName = $"GameData_{System.DateTime.Now:dd-MM-yyyy-HH-mm}.txt";
        System.IO.File.WriteAllText(fileName, "=== D�but de la partie ===\n");
    }

    public void CreateFileText()
    {
        Debug.Log("Fichier cr�er");
        string data = $"Fin de la Vague {spawner.waveID + 1} \n" +
            $"Nombre de fragments : {playerInventory.monsterFragments} \n" +
            $"Player Stats : \n" +
            $"Vie Max : {playerStats.maxHealth} \n" +
            $"Vie Actuelle : {playerStats.currentHealth} \n" +
            $"D�g�ts Brut : {playerStats.damageBrut} \n" +
            $"D�g�ts % : {playerStats.damagePercentage} \n" +
            $"Vitesse : {playerStats.movementSpeed} \n" +
            $"Port�e : {playerStats.range} \n" +
            $"Vitesse Attaque : {playerStats.attackSpeed} \n" +
            $"Armure : {playerStats.armor}\n\n";

        if (System.IO.File.Exists(fileName))
        {
            System.IO.File.AppendAllText(fileName, data);
            Debug.Log("Donn�es ajout�es au fichier existant");
        }
        else
        {
            print("Le Fichier txt n'a pas �t� cr�e ou mal configurer, les donn�es ne se sauvegarderons pas");
        }

    }
}
