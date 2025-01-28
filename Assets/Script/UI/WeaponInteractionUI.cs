using TMPro;
using UnityEngine;

public class WeaponInteractionUI : MonoBehaviour
{
    public WeaponInventoryUI inventoryUI;
    public Item item;
    public TextMeshProUGUI textUI;
    public bool wantToSale;
    private int value;

    private void Start()
    {
        inventoryUI = transform.parent.GetComponent<WeaponInventoryUI>();
        value = Mathf.RoundToInt(item.price / 1.5f);
        textUI.text = $"+ {value} fragments";
    }

    public void SellWeapon()
    {
        wantToSale = true;

        PlayerInventory inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        inventory.AddFragment(value);
        inventory.UpdateUIMonsterFragment();

        inventoryUI.CheckForSale();
    }
}
