using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Unity.Mathematics;
using TMPro;


public class SliderVolume : MonoBehaviour
{
    public Slider slider;
    public AudioMixer mixer;
    public string mixerName;
    public string KeyName;
    private float volumeValue;
    public TextMeshProUGUI textUI;

    private void Awake()
    {
        //Debug.Log(PlayerPrefs.GetFloat(KeyName));
        if (PlayerPrefs.HasKey(KeyName))
        {
            print("a un player pref");
            volumeValue = PlayerPrefs.GetFloat(KeyName);
            mixer.SetFloat(mixerName, volumeValue);
            slider.value = volumeValue;
        }
        else
        {
            print("n'a pas de player pref");
            PlayerPrefs.SetFloat(KeyName, 1f);
            PlayerPrefs.Save();
            slider.value = slider.maxValue;
        }
    }

    public void SetVolume(float volume)
    {
        volumeValue = math.remap(slider.minValue, slider.maxValue, 0.0001f, 1, volume);
        mixer.SetFloat(mixerName, ToDecibel(volumeValue));
        textUI.text = (slider.normalizedValue * 100).ToString() + " %";
        PlayerPrefs.SetFloat(KeyName, volume);
        PlayerPrefs.Save();
    }
    private float ToDecibel(float value)
    {
        return Mathf.Log10(value) * 20f;
    }
}
