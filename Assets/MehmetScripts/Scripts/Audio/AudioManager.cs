using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject enemySound;
    public static Dictionary<string,AudioSource> sources = new Dictionary<string,AudioSource>();
    public static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    AudioClip[] clipsSaved;

    void Start()
    {
        //Clears the dictionaries just incase there are elements 
        //can cause crashes without
        audioClips.Clear();
        sources.Clear();
        sources.Add("PlayerSound", FindObjectOfType<Player>().GetComponent<AudioSource>());
        sources.Add("VFXSound", GetComponent<AudioSource>());
        sources.Add("EnemySound", enemySound.GetComponent<AudioSource>());
        clipsSaved = Resources.LoadAll<AudioClip>("usefull");
        for (int i = 0; i < clipsSaved.Length; i++)
        {
            audioClips.Add(clipsSaved[i].name, clipsSaved[i]); 
        }
    }
    /// <summary>
    /// This will play the audio string you input, at the specified at the audio source
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="audio"></param>
    public static void PlaySound(string clip, string audio)
    {
        sources[audio].clip = audioClips[clip];
        sources[audio].Play();
        
    }
    /// <summary>
    /// This will play the audio string you input, at the specified audiosource and at the specified delay
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="audio"></param>
    /// <param name="delay"></param>
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
    /// <summary>
    /// This stops the audio
    /// </summary>
    /// <param name="audio"></param>
    public static void StopSound(string audio)
    {
        sources[audio].Stop();
    }
    /// <summary>
    /// This will play the hovered name
    /// </summary>
    /// <param name="clip"></param>
    public void PlayHoverSound(string clip) 
    {
        sources["VFXSound"].clip = audioClips[clip];
        sources["VFXSound"].Play();
    }
    /// <summary>
    /// This checks if the audio is playing
    /// </summary>
    /// <param name="audio"></param>
    /// <returns>1 if playing, 0 if not</returns>
    public static bool IsPlaying(string audio)
    {
        return sources[audio].isPlaying;
    }
}
