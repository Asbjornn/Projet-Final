using UnityEngine;
using System.Collections.Generic;

public class PannelShop : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public GameObject pannelPrefab;

    public List<GameObject> createdPannel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseShop();
        UpdateShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitialiseShop()
    {
        for(int i = 0 ; i < transform.childCount ; i++)
        {
            GameObject newPannel = Instantiate(pannelPrefab, transform.GetChild(i).transform.position, Quaternion.identity, transform.GetChild(i));
            createdPannel.Add(newPannel);
        }
    }

    public void UpdateShop()
    {
        foreach (GameObject pannel in createdPannel)
        {
            int randomID = Random.Range(0, items.Count);
            ItemUI itemUI = pannel.GetComponent<ItemUI>();
            itemUI.choosenItem = items[randomID];
        }
    }
}
