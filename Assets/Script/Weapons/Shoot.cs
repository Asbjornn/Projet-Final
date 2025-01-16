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

    void Start()
    {
        playerStats = GameObject.Find("Stats").GetComponent<PlayerStats>();
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

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(shootPoint.position, findNearestEnemy.miniDistance);
    }*/
}