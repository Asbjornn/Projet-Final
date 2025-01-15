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
}
