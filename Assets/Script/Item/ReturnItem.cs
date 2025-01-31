using UnityEngine;
using UnityEngine.UI;

public class ReturnItem : MonoBehaviour
{
    public ItemUI itemUI;
    public Button button;
    public PannelShop pannelShop;
    public PlayerInventory playerInventory;
    public PlayerStats playerStats;
    public bool itemPurchased = false;
    public bool canBuyItem;

    public void Start()
    {
        pannelShop = GameObject.Find("PannelShop").GetComponent<PannelShop>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        playerStats = GameObject.Find("Stats").GetComponent <PlayerStats>();
    }

    private void Update()
    {
        if ((playerInventory.monsterFragments - itemUI.choosenItem.price) >= 0 && !itemPurchased)
        {
            if(playerInventory.inventoryWeaponList.Count >= playerInventory.inventoryMaxSize && itemUI.choosenItem.itemStats[0].itemType.ToString() == "weapon")
            {
                button.interactable = false;
            }
            else if (canBuyItem)
            {
                button.interactable = true;
            }
        }
        else
        {
            button.interactable = false;
        }
    }

    public Item Return()
    {
        return itemUI.choosenItem;
    }

    public void OnClick()
    {
        pannelShop.BuyItem(Return(), button);
        itemPurchased = true;
    }

    public void CheckHPTopBuy()
    {
        for (int i = 0; i < itemUI.choosenItem.itemStats.Count; i++)
        {
            if (itemUI.choosenItem.itemStats[i].statName.ToString() == "maxHealth")
            {
                if (playerStats.currentHealth + itemUI.choosenItem.itemStats[i].stat <= 0)
                {
                    canBuyItem = false;
                }
                else
                {
                    canBuyItem = true;
                }
            }
        }
    }
}
