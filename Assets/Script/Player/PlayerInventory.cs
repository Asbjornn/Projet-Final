using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public int monsterFragments;
    public Transform weaponContainer;
    public WeaponContainers weaponContainersScript;
    public StatGameEnd statGameScript;

    [HideInInspector]
    public int actualWeaponInInventory;

    public List<GameObject> inventoryWeaponList;
    public int inventoryMaxSize;

    [Header("UI")]
    public TextMeshProUGUI monsterFragmentsAmountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actualWeaponInInventory = 0;
        monsterFragments = 0;
        UpdateUIMonsterFragment();
    }

    public void AddFragment(int amount)
    {
        monsterFragments += amount;
        UpdateUIMonsterFragment();
    }
    
    public void WalkOnRessource(int amount)
    {
        monsterFragments += amount;
        statGameScript.fragmentsCollected += amount;
        UpdateUIMonsterFragment();
    }

    public void UpdateUIMonsterFragment()
    {
        monsterFragmentsAmountText.text = $"{monsterFragments} fragments";
    }

    public void BuyWithMonsterFragment(int amount)
    {
        monsterFragments -= amount;
        UpdateUIMonsterFragment();
    }

    public void RemoveWeapon(int id)
    {
        Destroy(inventoryWeaponList[id]);
        inventoryWeaponList.RemoveAt(id);
        weaponContainersScript.IntervalObject();
    }
}
