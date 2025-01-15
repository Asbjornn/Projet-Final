using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PannelShop : MonoBehaviour
{
    [Header("List item")]
    public List<Item> items;
    public List<Item> itemInShop;

    [Header("Pannel")]
    public GameObject pannelPrefab;
    public List<GameObject> createdPannel;

    [Header("Inventory and stats")]
    public PlayerInventory playerInventory;
    public PlayerStats playerStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //itemInShop = items;
        InitialiseShop();
        RerollShop();
    }

    public void InitialiseShop()
    {
        for(int i = 0 ; i < transform.childCount ; i++)
        {
            GameObject newPannel = Instantiate(pannelPrefab, transform.GetChild(i).transform.position, Quaternion.identity, transform.GetChild(i));
            createdPannel.Add(newPannel);
        }
    }

    public void UpdateShop()
    {
        foreach (GameObject pannel in createdPannel)
        {
            int randomID = Random.Range(0, itemInShop.Count);
            ItemUI itemUI = pannel.GetComponent<ItemUI>();
            itemUI.choosenItem = itemInShop[randomID];
            itemUI.InitialiseItem();
            itemInShop.RemoveAt(randomID);
        }
    }

    public void RerollShop()
    {
        itemInShop = new List<Item>(items);
        UpdateShop();
    }

    public void BuyItem(Item item)
    {
        if(playerInventory.monsterFragments - item.price >= 0)
        {
            playerInventory.monsterFragments -= item.price;
            playerInventory.UpdateUIMonsterFragment();
            playerStats.UpdateStat(item.statName, item.givenStat);
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
