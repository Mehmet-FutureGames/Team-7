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
        if (!PlayerStatsMenu.hasStartedFirstTime)
        {
            SceneManager.LoadScene("TutorialPC");
            PlayerStatsMenu.hasStartedFirstTime = true;
            PlayerPrefs.SetInt("hasStartedFirstTime", PlayerStatsMenu.hasStartedFirstTime ? 1 : 0);
        }
        else if(GetComponentInChildren<CharacterStats>().hasBeenBought && PlayerStatsMenu.hasStartedFirstTime)
        {
            SceneManager.LoadScene("Shop");
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
    public void CalibrationMenu()
    {
        SceneManager.LoadScene("MetronomeTestScene");
    }
}
