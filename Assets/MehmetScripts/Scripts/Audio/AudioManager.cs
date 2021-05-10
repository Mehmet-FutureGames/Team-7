using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject enemySound;
    public static Dictionary<string,AudioSource> sources = new Dictionary<string,AudioSource>();
    public static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    AudioClip[] clipsSaved;

    private void Start()
    {
        sources.Add("PlayerSound", FindObjectOfType<Player>().GetComponent<AudioSource>());
        sources.Add("VFXSound", GetComponent<AudioSource>());
        sources.Add("EnemySound", enemySound.GetComponent<AudioSource>());
        Debug.Log(sources["EnemySound"]);
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
    public static void PlaySound(string clip, string audio, float delay)
    {
 
        if (delay > 0)
        {
            sources[audio].clip = audioClips[clip];
            sources[audio].PlayDelayed(delay);
        }
        sources[audio].clip = audioClips[clip];
        sources[audio].Play();
        
    }
    public static void StopSound(string audio)
    {
        sources[audio].Stop();
    }
    public void PlayHoverSound(string clip)
    {
        sources["VFXSound"].clip = audioClips[clip];
        sources["VFXSound"].Play();
    }
    public static bool AudioSourcePlaying(string audio)
    {
        return sources[audio].isPlaying;
    }
}
