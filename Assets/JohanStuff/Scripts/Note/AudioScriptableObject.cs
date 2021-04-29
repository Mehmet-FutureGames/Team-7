using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioScriptableObject : ScriptableObject
{

    // Will be used to store audio file information
    public string songName;
    public float BPM;
    public AudioClip audioClip;
    public float volume;
}
