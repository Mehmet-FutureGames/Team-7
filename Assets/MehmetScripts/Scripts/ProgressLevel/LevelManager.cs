using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public int currentLevel = 0;

    public int levelsCompleted;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        levelsCompleted = PlayerPrefs.GetInt("FloorCompleted");
    }
    private void OnLevelWasLoaded(int level)
    {
        //Sets the current scene to the current level
        //To be changed (maybe)
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
}
