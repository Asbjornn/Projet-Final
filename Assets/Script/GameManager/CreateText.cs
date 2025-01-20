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
        System.IO.File.WriteAllText(fileName, "=== Début de la partie ===\n");
    }

    public void CreateFileText()
    {
        Debug.Log("Fichier créer");
        string data = $"Fin de la Vague {spawner.waveID + 1} \n" +
            $"Nombre de fragments : {playerInventory.monsterFragments} \n" +
            $"Player Stats : \n" +
            $"Vie Max : {playerStats.maxHealth} \n" +
            $"Vie Actuelle : {playerStats.currentHealth} \n" +
            $"Dégâts Brut : {playerStats.damageBrut} \n" +
            $"Dégâts % : {playerStats.damagePercentage} \n" +
            $"Vitesse : {playerStats.movementSpeed} \n" +
            $"Portée : {playerStats.range} \n" +
            $"Vitesse Attaque : {playerStats.attackSpeed} \n" +
            $"Armure : {playerStats.armor}\n\n";

        if (System.IO.File.Exists(fileName))
        {
            System.IO.File.AppendAllText(fileName, data);
            Debug.Log("Données ajoutées au fichier existant");
        }
        else
        {
            print("Le Fichier txt n'a pas été crée ou mal configurer, les données ne se sauvegarderons pas");
        }

    }
}
