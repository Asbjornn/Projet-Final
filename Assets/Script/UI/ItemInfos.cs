using UnityEngine;
using TMPro;

public class ItemInfos : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textEffect;

    public void GetItemInfos(Item item)
    {
        textName.text = item.name;
        textEffect.text = item.itemEffect;
    }

}
