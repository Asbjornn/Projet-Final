using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealthEnemy;
    public Animator animator;
    public GameObject fragment;
    public SpawnerContinuous spawner;
    public int fragmentOnDeath;
    public AudioSource audioSource;
    public Enemy1 enemy1;

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
        if(currentHealthEnemy <= 0)
        {
            StartCoroutine(EnemyDie(fragmentOnDeath));
        }
    }

    public IEnumerator EnemyDie(int amount)
    {
        spawner.RemoveEnemy(gameObject);
        enemy1.rb.angularVelocity = 0;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(0.7f);
        for(int i = 0; i < amount; i++)
        {
            Instantiate(fragment, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("Hurt");
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();
            float amount = collision.gameObject.GetComponent<Damage>().damage;
            currentHealthEnemy -= amount;
            print(gameObject.name + "prend des degts de balles");
        }
    }
}
