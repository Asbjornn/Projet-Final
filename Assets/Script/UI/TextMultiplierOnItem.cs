using TMPro;
using UnityEngine;

public class TextMultiplierOnItem : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int numberOfItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numberOfItem = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfItem > 1)
        {
            text.text = $"x {numberOfItem}";
        }
    }
}
