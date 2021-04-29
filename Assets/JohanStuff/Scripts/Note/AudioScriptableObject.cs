using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Preset", menuName = "ScriptableObjects/Audio/Music Preset")]
public class AudioScriptableObject : ScriptableObject
{

    // Will be used to store audio file information
    public string songName;
    public float BPM;
    public AudioClip audioClip;
    [Range(0,1)]
    public float volume;
    public float noteStartDelay;
}
