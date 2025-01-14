using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public FindNearestEnemy findNearestEnemy;

    public GameObject bullet;
    public Transform shootPoint;
    public float reloadTime;
    public bool reload;
    public float range;

    public float damageBullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        bul.GetComponent<Damage>().damage = damageBullet;
        yield return new WaitForSeconds(reloadTime);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(shootPoint.position, range);
    }
}
