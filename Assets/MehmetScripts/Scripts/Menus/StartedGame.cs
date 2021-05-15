using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartedGame : MonoBehaviour
{
    bool hasStartedFirstTimeMenu = false;
    public static bool gameStartedFirstTime = false;
    // Start is called before the first frame update
    void Start()
    {
        gameStartedFirstTime = false;
        hasStartedFirstTimeMenu = PlayerPrefs.GetInt("hasStartedFirstTimeMenu") == 1;
        if (!hasStartedFirstTimeMenu)
        {
            LoadLevel();
            FindObjectOfType<MusicSingleton>().DestroyThis();
        }
    }
    private void LoadLevel()
    {
        hasStartedFirstTimeMenu = true;
        gameStartedFirstTime = true;
        PlayerPrefs.SetInt("hasStartedFirstTimeMenu", hasStartedFirstTimeMenu ? 1 : 0);
        SceneManager.LoadScene("MetronomeTestScene", LoadSceneMode.Single);
    }
}
