using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform player;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public int damage;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = (player.position - transform.position).normalized * speed;
    }

    public void Flip()
    {
        Vector2 direction = player.position - transform.position;
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            print("je detecte collision");
            PlayerStats stats = GameObject.Find("Stats").GetComponent<PlayerStats>();
            stats.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
