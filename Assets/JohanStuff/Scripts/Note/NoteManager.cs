using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    easy,
    normal,
    hard
}

public class NoteManager : MonoBehaviour
{
    public Vector3 StartScale;
    [Range(0f, 1)]
    public float downScaleMultiplier;

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
    [SerializeField] AudioScriptableObject audioPreset;
    [SerializeField] AudioScriptableObject shopSongPreset;
    [SerializeField] CalibrationSaver calibrationSaver;
    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        //LoadPresetData();
        
    }

    private void Start()
    {
        SetDifficulty();
    }

    void LoadPresetData(AudioScriptableObject preset)
    {
        beatTempo = preset.BPM;
        noteStartDelay = calibrationSaver.delay + preset.noteStartDelay;
        camera.GetComponent<AudioSource>().volume = preset.volume;
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
    private void OnLevelWasLoaded(int level)
    {
        camera.GetComponent<AudioSource>().Stop();

        if(level == 2)
        {
            LoadPresetData(shopSongPreset);
        }
        else
        {
            LoadPresetData(audioPreset);
        }
        camera.GetComponent<AudioSource>().Play();
    }
}
