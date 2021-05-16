using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    bool motionBlurSetting;
    PlayerStatsMenu playerStatsMenu;
    public static bool hasGoneToSettings = false;
    int levelSelected;
    LevelManager manager;
    public static AudioMixer mixer;

    public static AsyncOperation scene;

    [SerializeField] Slider masterVolume;
    [SerializeField] Slider SFXVolume;
    [SerializeField] Slider musicVolume;

    [SerializeField] Toggle motionBlur;

     float masterVolumeFloat;
     float SFXVolumeFloat;
     float musicVolumeFloat;

    [SerializeField] GameObject saveButtonMenu;
    [SerializeField] GameObject saveButtonSettings;
    private void Awake()
    {
        mixer = Resources.Load<AudioMixer>("MainMixer");
    }

    private void Start()
    {
        LoadVolume();
        motionBlurSetting = PlayerPrefs.GetInt("MotionBlur") == 1;
        hasGoneToSettings = false;
        manager = FindObjectOfType<LevelManager>();
        playerStatsMenu = FindObjectOfType<PlayerStatsMenu>();
        LoadMotionBlurSetting();
    }

    public void SavedVolume()
    {
        PlayerPrefs.SetFloat("masterVolume",masterVolumeFloat);
        PlayerPrefs.SetFloat("SFXvolume", SFXVolumeFloat);
        PlayerPrefs.SetFloat("MusicVol", musicVolumeFloat);
        mixer.SetFloat("MasterVol", Mathf.Log10(masterVolumeFloat) * 20);
        mixer.SetFloat("SFXVol", Mathf.Log10(SFXVolumeFloat) * 20);
        mixer.SetFloat("MusicVol", Mathf.Log10(musicVolumeFloat) * 20);
    }
    public void LoadVolume()
    {
        if (masterVolume != null)
            if (PlayerPrefs.GetFloat("masterVolume") != 0)
                masterVolume.value = PlayerPrefs.GetFloat("masterVolume");
            else
                masterVolume.value = 1;
        if (SFXVolume != null)
            if (PlayerPrefs.GetFloat("SFXvolume") != 0)
                SFXVolume.value = PlayerPrefs.GetFloat("SFXvolume");
            else
                SFXVolume.value = 1;
        if (musicVolume != null)
            if (PlayerPrefs.GetFloat("MusicVol") != 0)
                musicVolume.value = PlayerPrefs.GetFloat("MusicVol");
            else
                musicVolume.value = 1;

        masterVolumeFloat = PlayerPrefs.GetFloat("masterVolume");
        SFXVolumeFloat = PlayerPrefs.GetFloat("SFXvolume");
        musicVolumeFloat = PlayerPrefs.GetFloat("MusicVol");

        if (mixer == null)
        {
            mixer = Resources.Load<AudioMixer>("MainMixer");
        }
        else
        {
            mixer.SetFloat("MasterVol", Mathf.Log10(masterVolumeFloat) * 20);
            mixer.SetFloat("SFXVol", Mathf.Log10(SFXVolumeFloat) * 20);
            mixer.SetFloat("MusicVol", Mathf.Log10(musicVolumeFloat) * 20);
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
                mixer.SetFloat("MasterVol", Mathf.Log10(masterVolumeFloat) * 20);
                break;
            case 1:
                mixer.SetFloat("SFXVol", Mathf.Log10(SFXVolumeFloat) * 20);
                break;
            case 2:
                mixer.SetFloat("MusicVol", Mathf.Log10(musicVolumeFloat) * 20);
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
        if (!playerStatsMenu.hasStartedFirstTime && !PlayerStatsMenu.hasUpgraded)
        {
            StartTutorial();
        }
        else if(GetComponentInChildren<CharacterStats>().hasBeenBought && playerStatsMenu.hasStartedFirstTime && !PlayerStatsMenu.hasUpgraded)
        {
            StartCoroutine(SceneFader.FadeOut(PlayGameMethod));
        }
    }

    private void PlayGameMethod()
    {
        if (!GetComponentInChildren<CharacterStats>().hasBeenBought)
        {
            StartCoroutine(GetComponent<PlayerStatsMenu>().CantBuyChar());
        }
        else if (GetComponentInChildren<CharacterStats>().hasBeenBought && playerStatsMenu.hasStartedFirstTime && !PlayerStatsMenu.hasUpgraded)
        {
            SceneManager.LoadScene("Shop");
        }
        FindObjectOfType<MusicSingleton>().DestroyThis();
    }
    private void StartTutorial()
    {
#if UNITY_STANDALONE
        SceneManager.LoadScene("TutorialPC");
#endif
#if UNITY_ANDROID
SceneManager.LoadScene("TutorialMobile");
#endif
        playerStatsMenu.hasStartedFirstTime = true;
        PlayerPrefs.SetInt("hasStartedFirstTime", playerStatsMenu.hasStartedFirstTime ? 1 : 0);
    }

    public void Settings()
    {
        if (!PlayerStatsMenu.hasUpgraded)
        {
            SceneManager.LoadScene("SettingsUI");
        }
    }

    public void LoadMotionBlurSetting()
    {
        motionBlurSetting = PlayerPrefs.GetInt("MotionBlur") == 1;
        Debug.Log(motionBlurSetting);
        if (motionBlur != null)
        {
            motionBlur.isOn = motionBlurSetting;
        }
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
                if (FindObjectOfType<MusicSingleton>() != null)
                {
                    FindObjectOfType<MusicSingleton>().DestroyThis();
                }
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
                if (!PlayerStatsMenu.hasUpgraded)
                {
                    SceneManager.LoadScene("MainMenu");
                }
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
    public static void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void MuteAudio(int audioSource)
    {
        switch (audioSource)
        {
            case 0:
                mixer.SetFloat("MasterVol", Mathf.Log10(0.0001f) * 20);
                masterVolume.value = 0.0001f;
                break;
            case 1:
                mixer.SetFloat("SFXVol", Mathf.Log10(0.0001f) * 20);
                SFXVolume.value = 0.0001f;
                break;
            case 2:
                mixer.SetFloat("MusicVol", Mathf.Log10(0.0001f) * 20);
                musicVolume.value = 0.0001f;
                break;
        }
    }
    public void UnMuteAudio(int audioSource)
    {
        switch (audioSource)
        {
            case 0:
                mixer.SetFloat("MasterVol", Mathf.Log10(1) * 20);
                masterVolume.value = 1;
                break;
            case 1:
                mixer.SetFloat("SFXVol", Mathf.Log10(1) * 20);
                SFXVolume.value = 1;
                break;
            case 2:
                mixer.SetFloat("MusicVol", Mathf.Log10(1) * 20);
                musicVolume.value = 1;
                break;
        }
    }
    public void SaveMotionBlurSetting()
    {
        PlayerPrefs.SetInt("MotionBlur", motionBlurSetting ? 1 : 0);
    }
    public void MotionBlurOption()
    {
        if (motionBlur.isOn)
        {
            motionBlurSetting = true;
        }
        if (!motionBlur.isOn)
        {
            motionBlurSetting = false;
        }
    }
    public void ChangeGraphics(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
    }
}
