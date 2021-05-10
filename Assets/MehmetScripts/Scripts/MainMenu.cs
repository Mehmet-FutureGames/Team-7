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
    public static AudioMixer mixer;

    [SerializeField] Slider masterVolume;
    [SerializeField] Slider SFXVolume;
    [SerializeField] Slider musicVolume;
     Scene currentScene;

     float masterVolumeFloat;
     float SFXVolumeFloat;
     float musicVolumeFloat;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        mixer = Resources.Load<AudioMixer>("MainMixer");
        manager = FindObjectOfType<LevelManager>();
        LoadVolume();
    }

    public void SavedVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeFloat);
        PlayerPrefs.SetFloat("SFXvolume", SFXVolumeFloat);
        PlayerPrefs.SetFloat("MusicVol", musicVolumeFloat);
        mixer.SetFloat("MasterVol", masterVolumeFloat);
        mixer.SetFloat("SFXVol", SFXVolumeFloat);
        mixer.SetFloat("MusicVol", musicVolumeFloat);
    }
    public void LoadVolume()
    {
        masterVolumeFloat = PlayerPrefs.GetFloat("masterVolume");
        SFXVolumeFloat = PlayerPrefs.GetFloat("SFXvolume");
        musicVolumeFloat = PlayerPrefs.GetFloat("MusicVol");

        if(mixer == null)
        {
            mixer = Resources.Load<AudioMixer>("MainMixer");
        }
        else
        {
            mixer.SetFloat("MasterVol", masterVolumeFloat);
            mixer.SetFloat("SFXVol", SFXVolumeFloat);
            mixer.SetFloat("MusicVol", musicVolumeFloat);
        }

        if (currentScene.buildIndex == SceneManager.GetSceneByName("SettingsUI").buildIndex)
        {
            masterVolume.value = PlayerPrefs.GetFloat("masterVolume"); 
            SFXVolume.value = PlayerPrefs.GetFloat("SFXvolume");
            musicVolume.value = PlayerPrefs.GetFloat("MusicVol");
        }
    }

    public void ChangeVolume(int volumeChanged)
    {
        masterVolumeFloat = masterVolume.value;
        SFXVolumeFloat = SFXVolume.value;
        musicVolumeFloat = musicVolume.value;
        switch (volumeChanged)
        {
            default:
                mixer.SetFloat("MasterVol", masterVolumeFloat);
                break;
            case 0:
                mixer.SetFloat("MasterVol", masterVolumeFloat);
                break;
            case 1:
                mixer.SetFloat("SFXVol", SFXVolumeFloat);
                break;
            case 2:
                mixer.SetFloat("MusicVol", musicVolumeFloat);
                break;
        }
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
        if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex))
        {
            PauseMenu.player.gameObject.SetActive(true);
            PauseMenu.player.ActivateAll();
            foreach (GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                g.SetActive(true);
            }
            SceneManager.UnloadSceneAsync("SettingsUI");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
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
