using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public EnemyHealth health;
    public Transform player;
    public float speed;
    public int damageClose;
    public int damageShoot;

    public State actualState;

    [Space(10)]
    [Header("ShootParameters")]
    public float range;
    public float shootInterval;
    public GameObject shootPoint;
    public GameObject enemyBullet;

    public bool canAttack;
    float chrono;

    public enum State {chase, attack};

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
            actualState = State.attack;
        }
        /*else
        {
            actualState = state.chase;
        }*/

        switch (actualState)
        {
            case State.chase:
                canAttack = true;
                animator.SetBool("Walk", true);
                break;
            case State.attack:
                canAttack = false;
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
        //

        Vector3 direction = player.position - shootPoint.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shootPoint.transform.rotation = Quaternion.Euler(0, 0, angle);

        if(chrono > shootInterval)
        {
            animator.SetTrigger("Shoot");
            
            Instantiate(enemyBullet, shootPoint.transform.position, shootPoint.transform.rotation);
            chrono = 0;

            if (canAttack)
            {
                actualState = State.attack;
            }
            else
            {
                actualState = State.chase;
            }
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
        if(canAttack)
        {
            rb.linearVelocity = (player.position - transform.position).normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("je detecte collision");
            PlayerStats stats = GameObject.Find("Stats").GetComponent<PlayerStats>();
            stats.TakeDamage(damageClose);
            health.EnemyDie(health.fragmentOnDeath);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(shootPoint.transform.position, range);
    }
}
