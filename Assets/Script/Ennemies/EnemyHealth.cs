using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Animator animator;
    public GameObject fragment;
    public SpawnerContinuous spawner;
    public int fragmentOnDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = GameObject.Find("SpawnerManager").GetComponent<SpawnerContinuous>();
        switch(spawner.waveID)
        {
            case <5 :
                maxHealth += 0;
                break;
            case <10:
                maxHealth += 2;
                break;
            case <15:
                maxHealth += 4;
                break;
            case <=20:
                maxHealth += 6;
                break;
            /*case 4:
                maxHealth += 4;
                break;
            case 5:
                maxHealth += 4;
                break;
            case 6:
                maxHealth += 5;
                break;
            case 7:
                maxHealth += 5;
                break;
            case 8:
                maxHealth += 6;
                break;
            case 9:
                maxHealth += 6;
                break;
            case 10:
                maxHealth += 7;
                break;
            case 11:
                maxHealth += 7;
                break;
            case 12:
                maxHealth += 8;
                break;
            case 13:
                maxHealth += 8;
                break;
            case 14:
                maxHealth += 9;
                break;
            case 15:
                maxHealth += 9;
                break;
            case 16:
                maxHealth += 10;
                break;
            case 17:
                maxHealth += 10;
                break;
            case 18:
                maxHealth += 11;
                break;
            case 19:
                maxHealth += 11;
                break;
            case 20:
                maxHealth += 12;
                break;*/
            default:
                break;
        }
        currentHealth = maxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            EnemyDie(fragmentOnDeath);
        }
    }

    public void EnemyDie(int amount)
    {
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
            float amount = collision.gameObject.GetComponent<Damage>().damage;
            currentHealth -= amount;
            print(gameObject.name + "prend des degts de balles");
        }
    }
}
