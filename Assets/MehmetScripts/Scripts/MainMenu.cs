using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public static bool hasGoneToSettings = false;
    int levelSelected;
    LevelManager manager;
    public static AudioMixer mixer;

    public static AsyncOperation scene;

    [SerializeField] Slider masterVolume;
    [SerializeField] Slider SFXVolume;
    [SerializeField] Slider musicVolume;

     float masterVolumeFloat;
     float SFXVolumeFloat;
     float musicVolumeFloat;

    private void Start()
    {
        hasGoneToSettings = false;
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
        if (masterVolume != null)       
            masterVolume.value = PlayerPrefs.GetFloat("masterVolume");
        if (SFXVolume != null)
            SFXVolume.value = PlayerPrefs.GetFloat("SFXvolume");
        if (musicVolume != null)
            musicVolume.value = PlayerPrefs.GetFloat("MusicVol");

        if (mixer == null)
        {
            mixer = Resources.Load<AudioMixer>("MainMixer");
        }
        else
        {
            mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("masterVolume"));
            mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXvolume"));
            mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        }

    }

    public void ChangeVolume(int volumeChanged)
    {
        masterVolumeFloat = masterVolume.value;
        SFXVolumeFloat = SFXVolume.value;
        musicVolumeFloat = musicVolume.value;
        switch (volumeChanged)
        {
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
        if(GetComponentInChildren<CharacterStats>().hasBeenBought && PlayerStatsMenu.hasStartedFirstTime && !PlayerStatsMenu.hasUpgraded)
        {
            StartCoroutine(SceneFader.FadeOut(PlayGameMethod));
        }
    }

    private void PlayGameMethod()
    {
        if (!PlayerStatsMenu.hasStartedFirstTime && !PlayerStatsMenu.hasUpgraded)
        {
            SceneManager.LoadScene("TutorialPC");
            PlayerStatsMenu.hasStartedFirstTime = true;
            PlayerPrefs.SetInt("hasStartedFirstTime", PlayerStatsMenu.hasStartedFirstTime ? 1 : 0);
        }
        else if (GetComponentInChildren<CharacterStats>().hasBeenBought && PlayerStatsMenu.hasStartedFirstTime && !PlayerStatsMenu.hasUpgraded)
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
            if (PauseMenu.player != null)
            {
                hasGoneToSettings = true;
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

    }
    public void CalibrationMenu()
    {
        if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex))
        {
            if (PauseMenu.player != null)
            {
                Scene activeScene = SceneManager.GetSceneByName("SettingsUI");
                Debug.Log(activeScene.name);
                scene = SceneManager.LoadSceneAsync("MetronomeTestScene", LoadSceneMode.Additive);
                scene.allowSceneActivation = true;
                foreach (GameObject g in activeScene.GetRootGameObjects())
                {
                    g.SetActive(false);
                }
            }
            else
            {
                SceneManager.LoadScene("MetronomeTestScene");
            }
        }
    }
    public void BackButtonForCaliScene()
    {
        if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex))
        {
            if (PauseMenu.player != null)
            {
                hasGoneToSettings = true;
                SceneManager.UnloadSceneAsync("MetronomeTestScene");
                foreach (GameObject g in SceneManager.GetSceneByName("SettingsUI").GetRootGameObjects())
                {
                    g.SetActive(true);
                }
            }
            else
            {
                SceneManager.LoadScene("SettingsUI");
            }
        }
    }
    public void ChangeGraphics(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
        Debug.Log(QualitySettings.GetQualityLevel());
    }
}
