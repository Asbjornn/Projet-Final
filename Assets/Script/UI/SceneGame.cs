using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
