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
            StartCoroutine(LoadLevelDelay());
            FindObjectOfType<MusicSingleton>().DestroyThis();
        }
    }
    IEnumerator LoadLevelDelay()
    {
        hasStartedFirstTimeMenu = true;
        gameStartedFirstTime = true;
        PlayerPrefs.SetInt("hasStartedFirstTimeMenu", hasStartedFirstTimeMenu ? 1 : 0);
        yield return new WaitForSecondsRealtime(0.1f);
        SceneManager.LoadScene("MetronomeTestScene", LoadSceneMode.Single);
    }
}
