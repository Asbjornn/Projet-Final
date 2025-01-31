using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealthEnemy;
    public int fragmentOnDeath;
    public Animator animator;
    public GameObject fragment;
    public Enemy1 enemy1;
    public EnemyShoot enemyShoot;
    public List<Collider2D> col;

    [HideInInspector]
    public SpawnerContinuous spawner;
    [HideInInspector]
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GameObject.Find("AudioHitEnemy").GetComponent<AudioSource>();
        spawner = GameObject.Find("SpawnerManager").GetComponent<SpawnerContinuous>();
        currentHealthEnemy = maxHealth + spawner.enemyHpByWaves[spawner.waveID];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartCoroutineDie()
    {
        StartCoroutine(EnemyDie(fragmentOnDeath));
    }

    public IEnumerator EnemyDie(int amount)
    {
        spawner.RemoveEnemy(gameObject);
        foreach(Collider2D colll in col)
        {
            colll.enabled = false;
        }

        if (enemy1 != null)
        {
            enemy1.isStopped = true;
            enemy1.rb.linearVelocity = Vector2.zero;
        }
        else if (enemyShoot != null)
        {
            enemyShoot.enabled = false;
            enemyShoot.rb.linearVelocity = Vector2.zero;
        }
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(0.7f);
        for(int i = 0; i < amount; i++)
        {
            Instantiate(fragment, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        currentHealthEnemy -= amount;
        animator.SetTrigger("Hurt");
        if (currentHealthEnemy <= 0)
        {
            StartCoroutine(EnemyDie(fragmentOnDeath));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();
            float amount = collision.gameObject.GetComponent<Damage>().damage;
            TakeDamage(amount);

        }
    }
}
