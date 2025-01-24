using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isMenu;
    public bool isPause;

    public GameObject menuPannel;
    public GameObject pausePannel;

    private void Start()
    {
        Time.timeScale = 1.0f;
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            isMenu = true;
            menuPannel.SetActive(true);
        }
        else
        {
            isMenu = false;
            menuPannel.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
        }

        pausePannel.SetActive(isPause);

        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void SetPause(bool isPaused)
    {
        isPause = isPaused;
    }

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

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }

    public void CloseOptions()
    {
        if (isMenu)
        {
            menuPannel.SetActive(true);
        }
        else
        {
            pausePannel.SetActive(true);
        }
    }
}
