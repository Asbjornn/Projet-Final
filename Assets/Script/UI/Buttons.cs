using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using System.Runtime.InteropServices;

public class Buttons : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isMenu;
    public bool isPause;

    public GameObject menuPannel;
    public GameObject pausePannel;

    [Header("Resolution")]
    //public Dropdown resolutionDropdown;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Start()
    {
        InitialiseResolution();

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
    }

    public void Pause()
    {
        isPause = !isPause;
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

    public void InitialiseResolution()
    {
#if UNITY_WEBGL
        // Appeler une fonction JS pour fixer la résolution en WebGL
        //
#else
        Resolution[] resolutions = Screen.resolutions
            .Select(res => new Resolution { width = res.width, height = res.height })
            .Distinct()
            .ToArray();

        resolutionDropdown.ClearOptions();
        
        List<string> options = new();
        HashSet<string> uniqueResolutions = new();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width}x{resolutions[i].height}";

            if (!uniqueResolutions.Contains(option))
            {
                uniqueResolutions.Add(option);
                options.Add(option);

                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = options.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
#endif
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
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

    public void PlaySound(Audio audio)
    {
        int randomID = Random.Range(0, audio.clip.Count);
        audioSource.clip = audio.clip[randomID];
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
