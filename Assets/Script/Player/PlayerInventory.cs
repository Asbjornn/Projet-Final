using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int monsterFragments;

    [Header("UI")]
    public TextMeshProUGUI monsterFragmentsAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monsterFragments = 0;
        UpdateUIMonsterFragment();
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
}
