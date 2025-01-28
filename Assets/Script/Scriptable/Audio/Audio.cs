using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Audio", menuName = "Scriptable Objects/Audio")]
public class Audio : ScriptableObject
{
    public List<AudioClip> clip;
}
