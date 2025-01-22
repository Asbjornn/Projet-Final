using UnityEngine;

public class KillTheRunner : MonoBehaviour
{
    //public GameObject prefabRunner;
    public Vector2 sizeSpawn;

    public EventManager eventManager;

    private void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, sizeSpawn);
    }
}
