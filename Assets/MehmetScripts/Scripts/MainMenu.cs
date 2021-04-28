using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void ChangeCharacterScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGame()
    {
        if (GetComponentInChildren<CharacterStats>().hasBeenBought)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            StartCoroutine(GetComponent<PlayerStatsMenu>().cantbuyChar());
        }
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
