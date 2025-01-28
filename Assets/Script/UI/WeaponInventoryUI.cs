using UnityEngine;

public class WeaponInventoryUI : MonoBehaviour
{
    public PlayerInventory inventory;

    public void CheckForSale()
    {
        for(int i = 0; i <= transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<WeaponInteractionUI>().wantToSale == true)
            {
                inventory.RemoveWeapon(i);
                Destroy(transform.GetChild(i).gameObject);
                break;
            }
        }
    }
}
