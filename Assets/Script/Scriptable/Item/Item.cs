using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public string nameItem;
    public string itemEffect;
    public string description;
    public int price;
    public TierItem tier;
    //public string statName;
    //public float statUI;
    public float minReloadTime;

    [Header("Data stat")]
    public List<ItemData> itemStats;
}

[System.Serializable]
public class ItemData
{
    public ItemType itemType;
    public StatName statName;
    public float stat;
}

public enum ItemType {item, weapon} 
public enum StatName {maxHealth, damagePercentage, damageBrut, attackSpeed, range, armor, movementSpeed, damageBullet, reloadTime, damageMultiplier};
public enum TierItem {bronze, argent, or, diamant, weapon}
