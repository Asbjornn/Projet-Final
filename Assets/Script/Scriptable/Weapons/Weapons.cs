using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Objects/Weapons")]
public class Weapons : ScriptableObject
{
    [Header("UI")]
    public Sprite sprite;
    public string weaponName;
    public string weaponEffect;
    public string weaponDescription;
    public float price;

    [Header("GameObject")]
    public float damageBullet;
    public float rangeWeapon;
    public float reloadTime;
}
