using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Difficulty
{
    easy,
    normal,
    hard
}

public class NoteManager : MonoBehaviour
{
    Animator cameraAnim;
    NoteHandler noteHandler;
    [Space]
    public Difficulty difficulty;
    Player player;
    [HideInInspector]
    public float beatTempo;
    [HideInInspector]
    public float noteStartDelay;

    [Space]
    [HideInInspector]
    public float difficultyMultiplier;
    [Space]
    public GameObject notePrefab;
    private AudioClip clip;
    [SerializeField] AudioScriptableObject shopSongPreset;
    [SerializeField] AudioScriptableObject emilSSceneSongPreset;
    [SerializeField] AudioScriptableObject enricoSceneSongPreset;
    [SerializeField] AudioScriptableObject level_3Preset;
    [SerializeField] CalibrationSaver calibrationSaver;
    Camera camera;
    public static float currentSongMaxVolume;
    public static bool IsInShop;
    private void Awake()
    {
        camera = Camera.main;
        noteHandler = FindObjectOfType<NoteHandler>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        SetDifficulty();
    }

    public void LoadPresetData(AudioScriptableObject preset)
    {
        beatTempo = preset.BPM;
        noteStartDelay = calibrationSaver.delay + preset.noteStartDelay;
        currentSongMaxVolume = preset.volume;
        clip = preset.audioClip;
        camera.GetComponent<AudioSource>().clip = clip;
        noteHandler.NoteHandlerInitialize();
        if (!IsInShop)
        {
            noteHandler.StartCoroutine(noteHandler.Wait());
        }
    }
    public void SetDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.easy:
                difficultyMultiplier = 2;
                break;
            case Difficulty.normal:
                difficultyMultiplier = 1;
                break;
            case Difficulty.hard:
                difficultyMultiplier = 0.5f;
                break;
        }
    }
    public void StartMusic()
    {
        if (MainMenu.hasGoneToSettings)
        {
            camera.GetComponent<AudioSource>().Play();
            MainMenu.hasGoneToSettings = false;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(noteHandler == null)
        {
            noteHandler = FindObjectOfType<NoteHandler>();
        }
        camera.GetComponent<AudioSource>().Stop();
        cameraAnim = camera.GetComponent<Animator>();
        if(level == SceneManager.GetSceneByName("Shop").buildIndex || level == SceneManager.GetSceneByName("CoinShop").buildIndex)
        {
            IsInShop = true;
            LoadPresetData(shopSongPreset);
            PressAnyKey.hasStarted = true; 
            cameraAnim.enabled = false;
            camera.GetComponent<AudioSource>().Play();
            GameObject noteObj = Instantiate(noteHandler.notePrefab);
            noteObj.GetComponentInChildren<SpriteRenderer>().enabled = false;
            noteObj.GetComponent<NoteObject>();
            NoteMiss.Instance.TriggerCount = 1;
        }
        else if( level == SceneManager.GetSceneByName("EmilSTestScene").buildIndex)
        {
            IsInShop = false;
            LoadPresetData(emilSSceneSongPreset);
            cameraAnim.enabled = true;
            cameraAnim.Play("CameraStartAnim", 0, 0f);
            AudioManager.PlaySound("Fanfare_Win", "PlayerSound");
            Time.timeScale = 0;
        }
        else if (level == SceneManager.GetSceneByName("Level_2").buildIndex)
        {
            IsInShop = false;
            LoadPresetData(enricoSceneSongPreset);
            cameraAnim.enabled = true;
            cameraAnim.Play("CameraStartAnim", 0, 0f);
            AudioManager.PlaySound("Fanfare_Win", "PlayerSound");
            Time.timeScale = 0;
        }
        else if (level == SceneManager.GetSceneByName("Level_3").buildIndex)
        {
            IsInShop = false;
            LoadPresetData(level_3Preset);
            cameraAnim.enabled = true;
            cameraAnim.Play("CameraStartAnim", 0, 0f);
            AudioManager.PlaySound("Fanfare_Win", "PlayerSound");
            Time.timeScale = 0;
        }
    }
}
