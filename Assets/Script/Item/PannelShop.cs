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

    [Header("List tier")]
    public List<TierTab> probaItem;
    public List<WaveProba> wavesProba;

    [Header("Script")]
    public SpawnerContinuous spawnerContinuous;

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
        textRerollButton.text = $"REROLL : {priceReroll} fragments";
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
        // probl�me, j'ai besoin de cr�er une liste que chaque boucle utilisera pour piocher un item
        // cette liste ne peux pas �tre dans la boucle car sinon 3 sont cr��es
        // mais GetRandomItemTier doit �tre dans la boucle pour choisir un item de tier diff�rrent � chque boucle

        itemInShop = new List<Item>(items);

        foreach (GameObject pannel in createdPannel)
        {
            ItemUI itemUI = pannel.GetComponent<ItemUI>();

            itemUI.choosenItem = FilterShop(GetRandomItemTier());
            itemInShop.Remove(itemUI.choosenItem);

            pannel.transform.GetChild(4).GetComponent<Button>().interactable = true;
            pannel.transform.GetChild(4).GetComponent<ReturnItem>().itemPurchased = false;
            itemUI.InitialiseItem();
        }
    }

    public Item FilterShop(TierItem tier)
    {
        //cr�er une nouvelle liste depuis la liste globale
        //boucle dessus pour stocker seulement les tier voulu dans autre liste
        //genere un ID random de cette nouvelle liste
        //retourne cet item

        List<Item> itemByRank = new List<Item>();
        for (int i = 0; i < itemInShop.Count; i++)
        {
            if (itemInShop[i].tier == tier)
            {
                itemByRank.Add(itemInShop[i]);
            }
        }

        int id = Random.Range(0, itemByRank.Count);
        return itemByRank[id];
    }

    public TierItem GetRandomItemTier()
    {
        //Prend un nombre random entre 0 et 100
        //Prend le waveID actuel pour prendre cet id de la liste qui contient les proba de chaque tier de tomber
        //cumule les proba des tiers de l'id actuel
        //retourne ce tier

        int rndom = Random.Range(0, 100);

        TierItem randomTier;
        int cumulateValue = 0;
        print(wavesProba[spawnerContinuous.waveID].tabs.Count);
        for (int i = 0; i < wavesProba[spawnerContinuous.waveID].tabs.Count; i++)
        {
            print("je passe dans la boucle et le count de la liste = " + wavesProba[spawnerContinuous.waveID].tabs.Count);
            cumulateValue += wavesProba[spawnerContinuous.waveID].tabs[i].percent;
            print(cumulateValue + " = cumulate value");

            if (rndom < cumulateValue)
            {
                randomTier = wavesProba[spawnerContinuous.waveID].tabs[i].tier;
                print("me return le tier : " + randomTier);
                return randomTier;
            }
        }
        print("je return du bronze");
        return TierItem.bronze;
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
            Shoot sho = null;
            bool buyItem = false;

            for (int i = 0; i < item.itemStats.Count; i++)
            {
                if (item.itemStats[i].itemType.ToString() == "weapon" && !buyItem)
                {
                    buyItem = true;
                    for (int y = 0; y < weaponContainers.transform.childCount; y++)
                    {
                        if (!weaponContainers.transform.GetChild(y).gameObject.activeSelf)
                        {
                            //active l'arme
                            weaponContainers.transform.GetChild(y).gameObject.SetActive(true);
                            sho = weaponContainers.transform.GetChild(y).GetComponent<Shoot>();
                            break;
                        }
                        else if(i <= 0)
                        {
                            print("plus aucun gameObject d�sactiv�");
                        }
                    }
                }
                else
                {
                    print("l'item n'est pas un weapon");
                }

                if (item.itemStats[i].itemType.ToString() == "item")
                {
                    //initialise les stats du player
                    playerStats.UpdateStat(item.itemStats[i].statName.ToString(), item.itemStats[i].stat);

                    //enleve les fragment de l'inventaire
                    //playerInventory.BuyWithMonsterFragment(item.price);
                }
                else if (item.itemStats[i].itemType.ToString() == "weapon" /*&& playerInventory.actualWeaponInInventory <= playerInventory.inventoryWeaponMaxSpace*/)
                {
                    //initialiser les stats de l'arme
                    sho.InitializeWeaponData(item.itemStats[i].statName.ToString(), item.itemStats[i].stat, item);

                    //ajoute 1 dans l'inventaire int
                    playerInventory.actualWeaponInInventory++;
                }
            }
            //enleve les fragment de l'inventaire
            playerInventory.BuyWithMonsterFragment(item.price);

            button.interactable = false;
        }
        else
        {
            print("Pas assez d'argent");
        }

        //R�cup�re l'item du gameObject du bouton
        //Check si je peux me l'acheter
        //Retire l'argent de mon inventaire
        //Grise l'item pour que ne puis pas le racheter
    }
}

[System.Serializable]
public class TierTab
{
    public TierItem tier;
    public int percent;
}

[System.Serializable]
public class WaveProba
{
    //list de tier "tab" avec chaque proba de tomber

    public List<TierTab> tabs;
}
