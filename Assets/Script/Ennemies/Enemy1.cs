using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform player;
    public float speed;
    public int damage;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = (player.position - transform.position).normalized * speed;
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
