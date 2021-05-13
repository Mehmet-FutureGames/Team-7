using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSingleton : MonoBehaviour
{
    Scene currentScene;

    private static MusicSingleton _instance;

    public static MusicSingleton Instance { get { return _instance; } }
    private void Awake()
    {
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
    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetSceneByName("MainMenu");
        if (level == currentScene.buildIndex)
        {
             gameObject.AddComponent<SceneFader>();            
        }
    }
    
}
