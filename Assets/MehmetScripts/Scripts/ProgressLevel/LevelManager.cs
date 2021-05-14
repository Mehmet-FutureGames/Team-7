using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public int currentLevel = 0;

    public static int levelsCompletedThisRun;

    public int levelsCompletedOverall;

    public static Scene currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
        levelsCompletedThisRun++;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetSceneByName("SettingsUI").buildIndex == level)
        {
            Debug.Log("You went into settings");
        }
        else
        {
            currentScene = SceneManager.GetActiveScene();
        }
        if(SceneManager.GetSceneByName("MainMenu").buildIndex == level)
        {
            levelsCompletedThisRun = 0;
            Destroy(gameObject);
        }
        //Sets the current scene to the current level
        //To be changed (maybe)
        currentLevel = level;
        if(level == SceneManager.GetSceneByName("EmilSTestScene").buildIndex)
        {
            --levelsCompletedThisRun;
        }
    }
}
