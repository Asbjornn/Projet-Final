using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("Item")]
    public GameObject itemPannel;
    public List<Item> itemsIninvetory = new();
    public List<GameObject> gameObjectItemInInventory = new();
    public GameObject prefabItemUI;

    [Header("Weapon")]
    public GameObject weaponPannel;
    public GameObject prefabWeaponUI;

    private void Start()
    {

    }

    public void UpdateWeaponUI(Item item)
    {
        GameObject newUI = Instantiate(prefabWeaponUI, weaponPannel.transform);
        newUI.GetComponent<WeaponInteractionUI>().item = item;
        newUI.GetComponent<Image>().sprite = item.sprite;
    }

    public void UpdateItemUI(Item item)
    {
        if(itemsIninvetory.Contains(item))
        {
            //Récupère le gameObject en double
            //Change le texte

            for (int i = 0; i < itemsIninvetory.Count; i++)
            {
                if (itemsIninvetory[i] == item)
                {
                    gameObjectItemInInventory[i].GetComponent<TextMultiplierOnItem>().numberOfItem++;
                    break;
                }
            }
        }
        else
        {
            //Instantie le prefab
            //Change son image
            //L'ajoute aux listes
            
            GameObject newUI = Instantiate(prefabItemUI, itemPannel.transform);
            newUI.GetComponent<Image>().sprite = item.sprite;
            newUI.GetComponent<ItemInfos>().GetItemInfos(item);
            itemsIninvetory.Add(item);
            gameObjectItemInInventory.Add(newUI);
        }
    }
}
