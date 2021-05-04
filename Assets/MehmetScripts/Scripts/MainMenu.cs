using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    int levelSelected;
    LevelManager manager;
    private void Start()
    {
        manager = FindObjectOfType<LevelManager>(); 
    }
    public void ChangeCharacterScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeLevelUp()
    {
        levelSelected = Mathf.Clamp(++levelSelected, 0, manager.levelsCompletedOverall);
    }
    public void ChangeLevelDown()
    {
        levelSelected = Mathf.Clamp(--levelSelected, 0, manager.levelsCompletedOverall);
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
