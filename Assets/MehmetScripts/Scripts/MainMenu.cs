using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        SceneManager.LoadScene("SettingsUI");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
