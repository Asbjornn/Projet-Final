using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public AudioSource audioSource;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenGameObject(GameObject gameObjectUI)
    {
        gameObjectUI.SetActive(true);
    }

    public void CloseGameObject(GameObject gameObjectUI)
    {
        gameObjectUI.SetActive(false);
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void QuiApplication()
    {
        Application.Quit();
    }
}
