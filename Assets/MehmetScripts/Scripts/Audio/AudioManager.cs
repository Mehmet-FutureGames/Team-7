using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static Dictionary<string,AudioSource> sources = new Dictionary<string,AudioSource>();
    public static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    AudioClip[] clipsSaved;

    private void Start()
    {
        sources.Add("PlayerSound", FindObjectOfType<Player>().GetComponent<AudioSource>());
        sources.Add("VFXSound", GetComponent<AudioSource>());
        sources.Add("EnemySound", GetComponentInChildren<AudioSource>());
        clipsSaved = Resources.LoadAll<AudioClip>("usefull");
        for (int i = 0; i < clipsSaved.Length; i++)
        {
            audioClips.Add(clipsSaved[i].name, clipsSaved[i]);
        }
    }

    public static void PlaySound(string clip, string audio)
    {
        sources[audio].clip = audioClips[clip];
        sources[audio].Play();
    }
    public void TestMethod(string clip)
    {
        sources["VFXSound"].clip = audioClips[clip];
        sources["VFXSound"].Play();
    }
    public static bool AudioSourcePlaying(string audio)
    {
        return sources[audio].isPlaying;
    }
}
