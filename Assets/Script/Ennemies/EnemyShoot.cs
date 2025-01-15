using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject fragment;
    public Transform player;
    public float speed;
    public int damageClose;
    public int damageShoot;

    public state actualState;

    [Space(10)]
    [Header("ShootParameters")]
    public float range;
    public float shootInterval;
    public GameObject weapon;
    public GameObject enemyBullet;

    public bool canChase;
    float chrono;

    public enum state {chase, attack};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) <= range)
        {
            actualState = state.attack;
        }
        else
        {
            actualState = state.chase;
        }

        switch (actualState)
        {
            case state.chase:
                canChase = true;
                animator.SetBool("Walk", true);
                break;
            case state.attack:
                canChase = false;
                animator.SetBool("Walk", false);
                Attack();
                break;
            default:
                break;
        }

        Flip(); 
    }

    public void Attack()
    {
        Vector3 direction = player.position - weapon.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0, 0, angle);

        if(chrono > shootInterval)
        {
            animator.SetTrigger("Shoot");
            GameObject newBullet = Instantiate(enemyBullet, weapon.transform.position, weapon.transform.rotation);
            chrono = 0;
        }
        else
        {
            chrono += Time.deltaTime;
        }
    }

    public void Flip()
    {
        Vector2 direction = player.position - transform.position;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if(canChase)
        {
            rb.linearVelocity = (player.position - transform.position).normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;   
        }
    }

    private void OnDestroy()
    {
        Instantiate(fragment, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("je detecte collision");
            PlayerStats stats = GameObject.Find("Stats").GetComponent<PlayerStats>();
            stats.TakeDamage(damageClose);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(weapon.transform.position, range);
    }
}
