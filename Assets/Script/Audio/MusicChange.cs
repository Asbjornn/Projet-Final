using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MusicChange : MonoBehaviour
{
    public List<AudioClip> audioClipList;
    public AudioSource audioSource;

    public IEnumerator ChangeMusicSmoothly(AudioClip newClip)
    {
        // Diminue le volume progressivement
        float currentVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime;  // Baisse le volume progressivement
            yield return null;
        }

        // Change le clip une fois le volume ? z?ro
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // R?augmente le volume progressivement
        while (audioSource.volume < currentVolume)
        {
            audioSource.volume += Time.deltaTime;
            yield return null;
        }
    }
}
