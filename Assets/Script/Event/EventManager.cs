using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public List<GameObject> prefabEvents;
    public SpawnerContinuous spawnerContinuous;
    public PlayerStats playerStats;
    public PlayerHealth health;
    public PlayerInventory inventory;
    public bool eventStarted;
    public bool claimReward;

    public void RandomEvent()
    {
        int randomId = Random.Range(0, prefabEvents.Count);
        Instantiate(prefabEvents[randomId], transform.position, Quaternion.identity, transform);
    }

    public void VictoryEvent()
    {
        print("Evenement réussi");
        if(!claimReward)
        {
            claimReward = true; 

            int randomNumber = Random.Range(0, 4);
            switch(randomNumber)
            {
                case 0: //Bonus HP
                    print("BONUS HP + 25% PV MAX");
                    health.AddHealth(Mathf.RoundToInt(playerStats.maxHealth * 0.5f));
                    break;
                case 1: //Bonus Fragment x2
                    print("BONUS FRAGMENT X2");
                    inventory.AddFragment(Mathf.RoundToInt(inventory.monsterFragments * 0.5f));
                    break;
                case 2: //Bonus stat random
                    print("BONUS STAT RANDOM");
                    RandomStat();
                    break;
                case 3: //Tue tous les mobs
                    print("BONUS TUES TT LES MOBS");
                    int number = 0;
                    foreach(GameObject go in spawnerContinuous.enemiesSpawned)
                    {
                        Destroy(go);
                        spawnerContinuous.enemiesSpawned.Clear();
                        number++;
                    }
                    inventory.AddFragment(number);
                    break;
                default:
                    break;
            }
        }
    }

    public void RandomStat()
    {
        for (int i = 0; i < 2; i++)
        {
            print("passe dans la boucle");
            int randomId = Random.Range(0, 6);
            switch(randomId)
            {
                case 0:
                    print("Augmente les HP");
                    playerStats.maxHealth += playerStats.maxHealth * 0.15f;
                    break;
                case 1:
                    print("Augmente les dgt %");
                    if(playerStats.damagePercentage == 0)
                    {
                        playerStats.damagePercentage += 5;
                    }
                    else
                    {
                        playerStats.damagePercentage += playerStats.damagePercentage * 0.15f;
                    }
                    break;
                case 2:
                    print("Augmente les dgt");
                    if (playerStats.damageBrut == 0)
                    {
                        playerStats.damageBrut += 0.5f;
                    }
                    else
                    {
                        playerStats.damageBrut += playerStats.damageBrut * 0.15f;
                    }
                    break;
                case 3:
                    print("Augmente l'attack speed");
                    if (playerStats.attackSpeed == 0)
                    {
                        playerStats.attackSpeed += 5;
                    }
                    else
                    {
                        playerStats.attackSpeed += playerStats.attackSpeed * 0.15f;
                    }
                        break;
                case 4:
                    print("Augmente la range");
                    if (playerStats.range == 0)
                    {
                        playerStats.range += 0.5f;
                    }
                    else
                    {
                        playerStats.range += playerStats.range * 0.15f;
                    }
                    break;
                case 5:
                    print("Augmente l'armure");
                    if (playerStats.armor == 0)
                    {
                        playerStats.armor += 0.5f;
                    }
                    else
                    {
                        playerStats.armor += playerStats.armor * 0.15f;
                    }
                    break;
                case 6:
                    print("Augmente la vitsse");
                    playerStats.movementSpeed += playerStats.movementSpeed * 0.15f;
                    break;
                default:
                    break;

            }
        }
    }

    public void StartEvent()
    {
        if(spawnerContinuous.waveID % 5 == 1 || spawnerContinuous.waveID % 5 == 3)
        {
            eventStarted = true;
            RandomEvent();
        }
    }

    public void resetBoolEvent()
    {
        eventStarted = false;
        claimReward = false;
    }
}
