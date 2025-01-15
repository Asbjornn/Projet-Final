using UnityEngine;

public class MonsterFragment : MonoBehaviour
{
    public int amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
            inventory.WalkOnRessource(amount);
            Destroy(gameObject);
        }
    }
}
