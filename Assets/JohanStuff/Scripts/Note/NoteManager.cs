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
    [SerializeField] CalibrationSaver calibrationSaver;
    Camera camera;
    public static float currentSongMaxVolume;
    private void Awake()
    {
        camera = Camera.main;
        //LoadPresetData();
        noteHandler = FindObjectOfType<NoteHandler>();
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
            Debug.Log(camera.GetComponent<AudioSource>().isPlaying);
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
            LoadPresetData(shopSongPreset);
            noteHandler.NoteHandlerInitialize();
            PressAnyKey.hasStarted = true;
            cameraAnim.enabled = false;
            camera.GetComponent<AudioSource>().Play();
        }
        else if( level == SceneManager.GetSceneByName("EmilSTestScene").buildIndex)
        {
            LoadPresetData(emilSSceneSongPreset);
            cameraAnim.enabled = true;
            noteHandler.NoteHandlerInitialize();
            Time.timeScale = 0;
        }
        else if (level == SceneManager.GetSceneByName("Level_2").buildIndex)
        {
            LoadPresetData(enricoSceneSongPreset);
            cameraAnim.enabled = true;
            cameraAnim.Play("CameraStartAnim", 0, 0f);
            noteHandler.NoteHandlerInitialize();
            Time.timeScale = 0;
        }
        
    }
}
