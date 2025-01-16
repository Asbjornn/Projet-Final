using UnityEngine;
using System.Collections.Generic;

public class FragmentManager : MonoBehaviour
{
    public GameObject[] fragments;

    public void CleanArena()
    {
        fragments = GameObject.FindGameObjectsWithTag("Fragment");
        foreach (GameObject frag in fragments)
        {
            Destroy(frag);
        }
    }
}
