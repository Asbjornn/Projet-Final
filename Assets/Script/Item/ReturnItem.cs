using UnityEngine;

public class ReturnItem : MonoBehaviour
{
    public ItemUI itemUI;
    public PannelShop pannelShop;

    public void Start()
    {
        pannelShop = GameObject.Find("PannelItems").GetComponent<PannelShop>();
    }

    public Item Return()
    {
        return itemUI.choosenItem;
    }

    public void OnClick()
    {
        pannelShop.BuyItem(Return());
    }
}
