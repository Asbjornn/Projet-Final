using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class EventManager : MonoBehaviour
{
    public List<GameObject> prefabEvents;
    public SpawnerContinuous spawnerContinuous;
    public PlayerStats playerStats;
    public PlayerHealth health;
    public PlayerInventory inventory;
    public TextMeshProUGUI textUI;
    public Animator textAnimator;
    public bool eventStarted;
    public bool claimReward;
    public bool winFragmentEvent;

    public void RandomEvent()
    {
        int randomId = Random.Range(0, prefabEvents.Count);
        Instantiate(prefabEvents[randomId], transform.position, Quaternion.identity, transform);
        textUI.text = "Évenement : " + prefabEvents[randomId].name;
        textAnimator.SetTrigger("Text");
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
                    print("BONUS HP + 50% PV MAX");
                    textUI.text = "Récompenses : Soin de PV";
                    textAnimator.SetTrigger("Text");
                    health.AddHealth(Mathf.RoundToInt(playerStats.maxHealth * 0.5f));
                    print($"Vie gagnée {Mathf.RoundToInt(playerStats.maxHealth * 0.5f)}");
                    if(playerStats.currentHealth > playerStats.maxHealth)
                    {
                        playerStats.currentHealth = playerStats.maxHealth;
                    }
                    break;
                case 1: //Bonus Fragment x2
                    print("BONUS FRAGMENT X2");
                    textUI.text = "Récompense : Fragment x2 enfin de vague";
                    textAnimator.SetTrigger("Text");
                    winFragmentEvent = true;
                    break;
                case 2: //Bonus stat random
                    print("BONUS STAT RANDOM");
                    textUI.text = "Récompense : Augmente 2 statistiques";
                    textAnimator.SetTrigger("Text");
                    RandomStat();
                    break;
                case 3: //Tue tous les mobs
                    print("BONUS TUES TT LES MOBS");
                    KillEvent();
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
                    playerStats.maxHealth += Mathf.RoundToInt(playerStats.maxHealth * 0.15f);
                    break;
                case 1:
                    print("Augmente les dgt %");
                    if(playerStats.damagePercentage == 0)
                    {
                        playerStats.damagePercentage += 5;
                    }
                    else
                    {
                        playerStats.damagePercentage += Mathf.RoundToInt(playerStats.damagePercentage * 0.15f);
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
                        playerStats.damageBrut += Mathf.RoundToInt(playerStats.damageBrut * 0.15f);
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
                        playerStats.attackSpeed += Mathf.RoundToInt(playerStats.attackSpeed * 0.15f);
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
                        playerStats.range += Mathf.RoundToInt(playerStats.range * 0.15f);
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
                        playerStats.armor += Mathf.RoundToInt(playerStats.armor * 0.15f);
                    }
                    break;
                case 6:
                    print("Augmente la vitsse");
                    playerStats.movementSpeed += Mathf.RoundToInt(playerStats.movementSpeed * 0.15f);
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

    public void DoubleFragments()
    {
        if (winFragmentEvent)
        {
            winFragmentEvent = false;

            inventory.AddFragment(Mathf.RoundToInt(inventory.monsterFragments * 0.5f));
            print($"Fragments gagnées {Mathf.RoundToInt(inventory.monsterFragments * 0.5f)}");
        }
    }

    public void KillEvent()
    {
        textUI.text = "Récompense : Tue tous les monstres";
        textAnimator.SetTrigger("Text");
        int number = 0;

        List<GameObject> enemiesToKill = new List<GameObject>(spawnerContinuous.enemiesSpawned);

        foreach (GameObject go in enemiesToKill)
        {
            if (go != null)
            {
                EnemyHealth enemyHealth = go.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.StartCoroutineDie();
                    number += go.GetComponent<EnemyHealth>().fragmentOnDeath;
                }
            }
        }
        spawnerContinuous.enemiesSpawned.Clear();
        enemiesToKill.Clear();  

        inventory.AddFragment(number);
    }

    public void ResetBoolEvent()
    {
        eventStarted = false;
        claimReward = false;
        winFragmentEvent = false;
    }
}
