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
    public string statName;
    public float givenStat;

    [Header("Data stat")]
    public List<ItemData> itemStats;
}

[System.Serializable]
public class ItemData
{
    public itemType itemType;
    public statName statName;
    public float stat;
}
public enum statName {maxHealth, damagePercentage, damageBrut, attackSpeed, range, armor, movementSpeed, damageBullet, reloadTime};
public enum itemType {item, weapon}
