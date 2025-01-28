using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public FindNearestEnemy findNearestEnemy;
    public PlayerStats playerStats;

    public GameObject bullet;
    public Transform shootPoint;

    [Header("WeaponData")]
    public Item actualWeapon;
    public SpriteRenderer spriteRenderer;
    public float damageBullet;
    public float range;
    public float reloadTime;
    public float damageMultiplier;

    public bool reload;

    void Start()
    {
        playerStats = GameObject.Find("Stats").GetComponent<PlayerStats>();

        if(actualWeapon != null )
        {
            for (int i = 0; i < actualWeapon.itemStats.Count; i++)
            {
                InitializeWeaponData(actualWeapon.itemStats[i].statName.ToString(), actualWeapon.itemStats[i].stat, actualWeapon);
            }
        }
    }

    void Update()
    {
        if(findNearestEnemy.nearestTarget != null && !reload)
        {
            StartCoroutine(ShootBullet());
        }
    }

    public IEnumerator ShootBullet()
    {
        reload = true;
        GameObject bul = Instantiate(bullet, shootPoint.position, transform.rotation);
        bul.GetComponent<Damage>().damage = damageBullet + (playerStats.damageBrut * damageMultiplier) + (playerStats.damagePercentage / 100);
        yield return new WaitForSeconds(Mathf.Clamp(reloadTime + (playerStats.attackSpeed / 100), actualWeapon.minReloadTime, 50));
        if (findNearestEnemy.nearestTarget != null)
        {
            reload = false;
            StartCoroutine(ShootBullet());
        }
        else
        {
            reload = false;
            yield break;
        }
    }

    public void InitializeWeaponData(string statName, float statValue, Item weapon)
    {
        actualWeapon = weapon;
        spriteRenderer.sprite = actualWeapon.sprite;

        switch(statName)
        {
            case "damageBullet":
                damageBullet = statValue;
                break;
            case "range":
                range = statValue;
                break;
            case "reloadTime":
                reloadTime = statValue; 
                break;
            case "damageMultiplier":
                damageMultiplier = statValue;
                break;
            default:
                break;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(shootPoint.position, findNearestEnemy.miniDistance);
    }*/
}