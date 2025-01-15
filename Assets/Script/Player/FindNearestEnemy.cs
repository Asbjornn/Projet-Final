using UnityEngine;

public class FindNearestEnemy : MonoBehaviour
{
    public PlayerStats playerStats;
    public Shoot shoot;
    public SpawnerContinuous spawnerContinuous;
    public Transform nearestTarget;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;

    public float miniDistance;
    float chrono;

    // Update is called once per frame
    void Update()
    {
        if(spawnerContinuous.enemiesSpawned != null)
        {
            if(chrono > 0.5f)
            {
                nearestTarget = FindEnemy();
            }
            else
            {
                chrono += Time.deltaTime;
            }
        }

        if(nearestTarget != null)
        {
            Vector3 direction = nearestTarget.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            Debug.DrawLine(transform.position, nearestTarget.position, Color.red);
        }

        FlipSprite();
    }

    public Transform FindEnemy()
    {
        Transform target = null;
        float distance = 0;
        miniDistance = shoot.range + playerStats.range;
        

        foreach (GameObject targets in spawnerContinuous.enemiesSpawned)
        {
            if(targets != null)
            {
                distance = Vector2.Distance(transform.position, targets.transform.position);
                if(transform.rotation.z > 180)
                {
                    print("RETOURNER");
                }
            }
            if(distance < miniDistance)
            {
                miniDistance = distance;
                target = targets.transform;
            }
        }
        return target;
    }

    public void FlipSprite()
    {
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            sprite.flipY = true;
        }
        else
        {
            sprite.flipY = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, miniDistance);
    }
}
