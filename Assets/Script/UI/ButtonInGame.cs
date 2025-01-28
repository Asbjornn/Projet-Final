using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInGame : MonoBehaviour
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

    public void PlaySound(Audio audio)
    {
        int randomID = Random.Range(0, audio.clip.Count);
        audioSource.clip  = audio.clip[randomID];
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

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }
}
