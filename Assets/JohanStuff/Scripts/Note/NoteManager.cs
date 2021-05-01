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
    private float volume;
    private AudioClip clip;
    [SerializeField] AudioScriptableObject audioPreset;
    [SerializeField] CalibrationSaver calibrationSaver;
    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        LoadPresetData();
        camera.GetComponent<AudioSource>().clip = clip;
    }

    private void Start()
    {
        SetDifficulty();
    }

    void LoadPresetData()
    {
        beatTempo = audioPreset.BPM;
        if(calibrationSaver.delay != 0f)
        {
            noteStartDelay = calibrationSaver.delay;
        }
        else
        {
            noteStartDelay = audioPreset.noteStartDelay;
        }
        volume = audioPreset.volume;
        clip = audioPreset.audioClip;
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
}
