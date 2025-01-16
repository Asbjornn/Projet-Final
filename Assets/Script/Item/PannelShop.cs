using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class PannelShop : MonoBehaviour
{
    [Header("List item")]
    public List<Item> items;
    public List<Item> itemInShop;

    [Header("Pannel")]
    public GameObject pannelPrefab;
    public List<GameObject> createdPannel;

    [Header("Inventory, stats, weapons")]
    public PlayerInventory playerInventory;
    public PlayerStats playerStats;
    public GameObject weaponContainers;
    GameObject emptyWeapons;

    [Header("Reroll")]
    public Button rerollButton;
    public TextMeshProUGUI textRerollButton;
    public bool canReroll;
    public int priceReroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseShop();
    }

    public void InitialiseShop()
    {
        for(int i = 0 ; i < transform.childCount ; i++)
        {
            GameObject newPannel = Instantiate(pannelPrefab, transform.GetChild(i).transform.position, Quaternion.identity, transform.GetChild(i));
            createdPannel.Add(newPannel);
        }

        UpdateShop();
    }

    private void Update()
    {
        textRerollButton.text = $"REROLL : {priceReroll}";
        if((playerInventory.monsterFragments - priceReroll) >= 0)
        {
            canReroll = true;
            rerollButton.interactable = true;
        }
        else
        {
            canReroll = false;
            rerollButton.interactable = false;
        }
    }

    public void UpdateShop()
    {
        itemInShop = new List<Item>(items);

        foreach (GameObject pannel in createdPannel)
        {
            int randomID = Random.Range(0, itemInShop.Count);
            ItemUI itemUI = pannel.GetComponent<ItemUI>();
            itemUI.choosenItem = itemInShop[randomID];
            pannel.transform.GetChild(4).GetComponent<Button>().interactable = true;
            itemUI.InitialiseItem();
            itemInShop.RemoveAt(randomID);
        }
    }

    public void RerollShop()
    {
        if(canReroll)
        {
            playerInventory.BuyWithMonsterFragment(priceReroll);
            priceReroll += 2;
            UpdateShop();
        }
    }

    public void ResetPriceShop()
    {
        priceReroll = 2;
    }

    public void BuyItem(Item item, Button button)
    {
        if(playerInventory.monsterFragments - item.price >= 0)
        {
            playerInventory.BuyWithMonsterFragment(item.price);
            //playerStats.UpdateStat(item.statName, item.givenStat);

            for (int i = 0; i < item.itemStats.Count; i++)
            {
                if (item.itemStats[i].itemType.ToString() == "item")
                {
                    playerStats.UpdateStat(item.itemStats[i].statName.ToString(), item.itemStats[i].stat);
                }
                else if (item.itemStats[i].itemType.ToString() == "weapon")
                {
                    //initialiser les stats de l'arme
                    for (int y = 0; y < weaponContainers.transform.childCount; y++)
                    {
                        if(!weaponContainers.transform.GetChild(y).gameObject.activeSelf)
                        {
                            weaponContainers.transform.GetChild(y).gameObject.SetActive(true);
                            Shoot sho = weaponContainers.transform.GetChild(y).GetComponent<Shoot>();
                            sho.InitializeWeaponData(item.itemStats[i].statName.ToString(), item.itemStats[i].stat, item);
                            break;
                        }
                        else
                        {
                            print("plus de place dans l'inventaire");
                        }
                    }
                }
            }
            button.interactable = false;
        }
        else
        {
            print("Pas assez d'argent");
        }

        //Récupère l'item du gameObject du bouton
        //Check si je peux me l'acheter
        //Retire l'argent de mon inventaire
        //Grise l'item pour que ne puis pas le racheter
    }
}
