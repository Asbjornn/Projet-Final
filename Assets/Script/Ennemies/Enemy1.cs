using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform player;
    public EnemyHealth health;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public int damage;
    public bool isStopped;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        isStopped = false;
    }

    private void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        if(!isStopped)
        {
            rb.linearVelocity = (player.position - transform.position).normalized * speed;
        }
    }

    public void Flip()
    {
        Vector2 direction = player.position - transform.position;
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            print("je detecte collision");
            PlayerStats stats = GameObject.Find("Stats").GetComponent<PlayerStats>();
            stats.TakeDamage(damage);
            StartCoroutine(health.EnemyDie(health.fragmentOnDeath));
        }
    }
}
