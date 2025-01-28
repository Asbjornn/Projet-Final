using UnityEngine;
using UnityEngine.Audio;

public class PlayRandomSound : MonoBehaviour
{
    public Audio clipOver;
    public Audio clipSelect;
    public AudioSource audioSource;

    public void PlaySoundOver()
    {
        int randomID = Random.Range(0, clipOver.clip.Count);
        audioSource.clip = clipOver.clip[randomID];
        audioSource.Play();
    }

    public void PlaySoundSelect()
    {
        int randomID = Random.Range(0, clipSelect.clip.Count);
        audioSource.clip = clipSelect.clip[randomID];
        audioSource.Play();
    }
}
