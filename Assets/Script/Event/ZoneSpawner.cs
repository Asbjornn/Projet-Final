using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{   
    public Vector2 sizeSpawn;
    public GameObject zonePrefab;

    public EventManager eventManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();

        Vector2 randomPos = new Vector2(Random.Range(-sizeSpawn.x / 2, sizeSpawn.x / 2), Random.Range(-sizeSpawn.y / 2, sizeSpawn.y / 2));
        Instantiate(zonePrefab, randomPos, Quaternion.identity, transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, sizeSpawn);
    }
}
