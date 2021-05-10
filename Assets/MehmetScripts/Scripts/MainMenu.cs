using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    int levelSelected;
    LevelManager manager;
    static AudioMixer mixer;

    [SerializeField] Slider masterVolume;
    [SerializeField] Slider SFXVolume;
    [SerializeField] Slider MusicVolume;

    private void Start()
    {
        mixer = Resources.Load<AudioMixer>("test");
        //SavedVolume();
        manager = FindObjectOfType<LevelManager>();
    }

    public static void SavedVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        float SFX = PlayerPrefs.GetFloat("SFXVolume");
        float music = PlayerPrefs.GetFloat("MusicVolume");
        mixer.SetFloat("MasterVol", masterVolume);
        mixer.SetFloat("SFXVol", SFX);
        mixer.SetFloat("MusicVol", music);
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
        StartCoroutine(SceneFader.FadeOut(PlayGameMethod));
        
    }

    private void PlayGameMethod()
    {
        if (!PlayerStatsMenu.hasStartedFirstTime)
        {
            SceneManager.LoadScene("TutorialPC");
            PlayerStatsMenu.hasStartedFirstTime = true;
            PlayerPrefs.SetInt("hasStartedFirstTime", PlayerStatsMenu.hasStartedFirstTime ? 1 : 0);
        }
        else if (GetComponentInChildren<CharacterStats>().hasBeenBought && PlayerStatsMenu.hasStartedFirstTime)
        {
            SceneManager.LoadScene("Shop");
        }
        else
        {
            StartCoroutine(GetComponent<PlayerStatsMenu>().cantbuyChar());
        }
        FindObjectOfType<MusicSingleton>().DestroyThis();
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
    public void ChangeGraphics(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
        Debug.Log(QualitySettings.GetQualityLevel());
    }
}
