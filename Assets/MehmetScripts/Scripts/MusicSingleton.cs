using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSingleton : MonoBehaviour
{
    AudioSource menuSong;
    SceneFader faderScript;

    Scene currentScene;

    private static MusicSingleton _instance;

    public static MusicSingleton Instance { get { return _instance; } }
    private void Awake()
    {
        menuSong = GetComponent<AudioSource>();
        PlaySong();
        faderScript = GetComponent<SceneFader>();
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }        
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
    public void PlaySong()
    {
        menuSong.Play();
    }
    private void OnLevelWasLoaded(int level)
    {
        //Checks if scene fader exists, if it doesn't, add it to the gameobject.
        currentScene = SceneManager.GetSceneByName("MainMenu");
        if (level == currentScene.buildIndex && faderScript == null)
        {
            var fader = gameObject.AddComponent<SceneFader>();
            faderScript = fader;
        }
    }
    
}
