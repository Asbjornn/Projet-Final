using UnityEngine;

public class MonsterFragment : MonoBehaviour
{
    public GameObject parent;
    public int amount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>();
            inventory.WalkOnRessource(amount);
            Destroy(parent);
        }
    }
}
