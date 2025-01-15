using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item choosenItem;
    public Image image;
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI statsItem;
    public TextMeshProUGUI descriptionItem;
    public TextMeshProUGUI priceItem;

    private void Start()
    {
        InitialiseItem();
    }

    public void InitialiseItem()
    {
        image.sprite = choosenItem.sprite;
        nameItem.text = choosenItem.nameItem;
        statsItem.text = choosenItem.itemEffect;
        descriptionItem.text = choosenItem.description;
        priceItem.text = $"{choosenItem.price}";
    }
}
