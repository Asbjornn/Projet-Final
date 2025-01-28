using UnityEngine;

public class WeaponContainers : MonoBehaviour
{
    public float rayon;
    public GameObject weaponPrefab;
    public Item startItem;
    public PlayerInventory playerInventory;
    public InventoryUI inventoryUI;

    private void Start()
    {
        GameObject firstWeapon = Instantiate(weaponPrefab, transform);
        for (int i = 0; i < startItem.itemStats.Count; i++)
        {
            firstWeapon.GetComponent<Shoot>().InitializeWeaponData(startItem.itemStats[i].statName.ToString(), startItem.itemStats[i].stat, startItem);
        }
        playerInventory.inventoryWeaponList.Add(firstWeapon);
        inventoryUI.UpdateWeaponUI(startItem);
        IntervalObject();
    }

    public void IntervalObject()
    {
        //Cercle trigonom�trique (360 degr� ou 2Pi)
        //On divise la taille totale du cercle par le nombre d'objet
        //Car on cherche un interval r�gulier entre chaques enfants

        float interval = (2f * Mathf.PI) / (float)transform.childCount;

        for (int i = 0; i < transform.childCount; i++)
        {
            float angle = i * interval;
            float y = Mathf.Sin(angle) * rayon;
            float x = Mathf.Cos(angle) * rayon;
            transform.GetChild(i).localPosition = new Vector3(x, y, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rayon);
    }
}
