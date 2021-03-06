using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseButton;
    public static bool GameIsPaused = false;

    public static Player player;

    public static AsyncOperation scene;

    public GameObject pauseMenuUI;

    PressAnyKey audio;

    private void Start()
    {
        pauseButton = GameObject.Find("PauseMobileButton");
        audio = FindObjectOfType<PressAnyKey>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        } 
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        audio.audio.UnPause();
        GameIsPaused = false;
#if UNITY_ANDROID
        pauseButton.SetActive(true);
#endif
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        audio.audio.Pause();
        Time.timeScale = 0f;
        GameIsPaused = true;
#if UNITY_ANDROID
        pauseButton.SetActive(false);
#endif
    }

    public static void LoadMenu()
    {
        FindObjectOfType<Player>().DestroyEverything();
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<Player>().DestroyEverything();
    } 
    public void GoToSettings()
    {
        foreach (GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            g.SetActive(false);
        }
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("SettingsUI", LoadSceneMode.Additive);
        player.DeactivateAll();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
