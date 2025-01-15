using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public FindNearestEnemy findNearestEnemy;
    public PlayerStats playerStats;

    public GameObject bullet;
    public Transform shootPoint;
    public float reloadTime;
    public float range;
    public bool reload;

    public float damageBullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.Find("Stats").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
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
        bul.GetComponent<Damage>().damage = damageBullet + playerStats.damageBrut + playerStats.damagePercentage;
        yield return new WaitForSeconds(reloadTime + playerStats.attackSpeed);
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

    public void AddStat(string statName, float amount)
    {
        switch(statName)
        {
            case "range":
                range += amount;
                break;
            case "reloadTime":
                reloadTime += amount;
                break;
            case "damagePercentage":
                damageBullet *= amount;
                break;
            case "damageBrut":
                damageBullet += amount;
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