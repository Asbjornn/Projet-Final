using UnityEngine;
using System.Collections;
using System.Linq;

public class Fire : MonoBehaviour
{
    public FindNearestEnemy findNearestEnemy;
    public PlayerStats playerStats;

    public GameObject fire;
    public Vector2 sizeEffect;
    public Transform shootPoint;
    public AudioSource audioSource;
    public Audio audioShoot;
    //public Animator animator;

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
        fire.SetActive(false);

        if (actualWeapon != null)
        {
            for (int i = 0; i < actualWeapon.itemStats.Count; i++)
            {
                InitializeWeaponData(actualWeapon.itemStats[i].statName.ToString(), actualWeapon.itemStats[i].stat, actualWeapon);
            }
        }
    }

    void Update()
    {
        if (findNearestEnemy.nearestTarget != null && !reload)
        {
            StartCoroutine(ShootFire());
        }
    }

    public IEnumerator ShootFire()
    {
        reload = true;
        fire.SetActive(true);

        audioSource.clip = audioShoot.clip[Random.Range(0, audioShoot.clip.Count)];
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();

        Collider2D[] col = Physics2D.OverlapBoxAll(shootPoint.transform.position, sizeEffect, transform.eulerAngles.z);

        if (col.Length > 0) // Vérifie qu'au moins un objet est touché
        {
            foreach (Collider2D coll in col)
            {
                EnemyHealth enemyHealth = coll.gameObject.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageBullet + (playerStats.damageBrut * damageMultiplier) + (playerStats.damagePercentage / 100f));
                }
            }
        }


        yield return new WaitForSeconds(reloadTime);
        
        if (findNearestEnemy.nearestTarget != null)
        {
            reload = false;
            StartCoroutine(ShootFire());
        }
        else
        {
            fire.SetActive(false);
            reload = false;
            yield break;
        }
    }

    public void InitializeWeaponData(string statName, float statValue, Item weapon)
    {
        actualWeapon = weapon;
        spriteRenderer.sprite = actualWeapon.sprite;

        switch (statName)
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

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //Gizmos.DrawWireCube(shootPoint.position, sizeEffect);

        if (shootPoint == null) return;

        // Sauvegarde la matrice actuelle
        Gizmos.matrix = Matrix4x4.TRS(shootPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z), Vector3.one);

        // Définition de la couleur et dessin du rectangle
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, sizeEffect); // Vector3.zero car on est déjà dans le repère local

        // Réinitialisation de la matrice
        Gizmos.matrix = Matrix4x4.identity;
    }
}
