using UnityEngine;

public class Wisp : MonoBehaviour
{
    [Header("WispData")]
    public Rigidbody2D rb2d;
    public float speed;
    public Transform actualWaypoint;
    public KillTheRunner runner;
    public SpawnerContinuous spawner;
    public Animator animator;

    [Header("WispLife")]
    public float lifePoint;

    [Header("Scirpt")]
    public EventManager eventManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        spawner = GameObject.Find("SpawnerManager").GetComponent<SpawnerContinuous>();
        spawner.enemiesSpawned.Add(gameObject);

        if (actualWaypoint == null)
        {
            NewWaypoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(actualWaypoint != null)
        {
            if (Vector2.Distance(transform.position, actualWaypoint.position) <= 0.1f)
            {
                NewWaypoint();
            }
            else
            {
                rb2d.linearVelocity = (actualWaypoint.position - transform.position).normalized * speed;
            }
        }

        if(lifePoint <= 0)
        {
            eventManager.VictoryEvent();
            Destroy(transform.parent.gameObject);
        }
    }

    public void NewWaypoint()
    {
        Vector2 randomPos = new Vector2(Random.Range(-runner.sizeSpawn.x / 2, runner.sizeSpawn.x / 2), Random.Range(-runner.sizeSpawn.y / 2, runner.sizeSpawn.y / 2));
        actualWaypoint.position = randomPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            animator.SetTrigger("Hurt");
            lifePoint -= collision.GetComponent<Damage>().damage;
            Destroy(collision.gameObject);
        }
    }
}
