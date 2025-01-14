using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform player;
    public float speed;
    public int damage;

    float chrono;
    bool canMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(chrono > 0.83f)
        {
            canMove = true;
        }
        else
        {
            chrono += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            rb.linearVelocity = (player.position - transform.position).normalized * speed;
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
