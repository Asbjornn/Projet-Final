using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats stats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(stats.currentHealth <= 0)
        {
            print("Meurt");
            //Die();
        }
    }
}
