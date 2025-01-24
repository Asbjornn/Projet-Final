using UnityEngine;

public class Attractedtoplayer : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    Vector3 dir;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            dir = collision.transform.position;
            rb.linearVelocity = (dir - transform.position).normalized * speed;
            //print("Move");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dir = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
