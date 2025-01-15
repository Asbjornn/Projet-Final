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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseItem();
    }

    public void InitialiseItem()
    {
        image.sprite = choosenItem.sprite;
        nameItem.text = choosenItem.name;
        statsItem.text = choosenItem.itemEffect;
        descriptionItem.text = choosenItem.description;
        priceItem.text = $"{choosenItem.price}";
    }
}
