using UnityEngine;
using UnityEngine.UI;

public class ReturnItem : MonoBehaviour
{
    public ItemUI itemUI;
    public Button button;
    public PannelShop pannelShop;
    public PlayerInventory playerInventory;
    public bool itemPurchased = false;

    public void Start()
    {
        pannelShop = GameObject.Find("PannelShop").GetComponent<PannelShop>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if ((playerInventory.monsterFragments - itemUI.choosenItem.price) >= 0 && !itemPurchased)
        {
            if(playerInventory.inventoryWeaponList.Count >= playerInventory.inventoryMaxSize && itemUI.choosenItem.itemStats[0].itemType.ToString() == "weapon")
            {
                button.interactable = false;
            }
            else
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
}
