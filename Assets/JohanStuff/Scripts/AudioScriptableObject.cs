using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioScriptableObject : ScriptableObject
{
    public string songName;
    public float BPM;
    public AudioClip audioClip;
    public float volume;
}
