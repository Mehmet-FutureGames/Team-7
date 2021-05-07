using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public int currentLevel = 0;

    public int levelsCompletedThisRun;

    public int levelsCompletedOverall;

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
        levelsCompletedThisRun++;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnLevelWasLoaded(int level)
    {
        //Sets the current scene to the current level
        //To be changed (maybe)
        currentLevel = level;
        if(level == SceneManager.GetSceneByName("EmilSTestScene").buildIndex)
        {
            levelsCompletedThisRun--;
        }
    }
}
