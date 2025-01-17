using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int monsterFragments;
    public Transform weaponContainer;

    [HideInInspector]
    public int inventoryWeaponMaxSpace;
    public int actualWeaponInInventory;

    [Header("UI")]
    public TextMeshProUGUI monsterFragmentsAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryWeaponMaxSpace = 0;
        actualWeaponInInventory = 0;
        monsterFragments = 0;
        UpdateUIMonsterFragment();

        for (int i = 0; i < weaponContainer.childCount -1; i++)
        {
            inventoryWeaponMaxSpace += 3;
        }
    }

    public void AddItem()
    {

    }
    
    public void WalkOnRessource(int amount)
    {
        monsterFragments += amount;
        UpdateUIMonsterFragment();
    }

    public void UpdateUIMonsterFragment()
    {
        monsterFragmentsAmount.text = $"{monsterFragments} fragments";
    }

    public void BuyWithMonsterFragment(int amount)
    {
        monsterFragments -= amount;
        UpdateUIMonsterFragment();
    }
}
