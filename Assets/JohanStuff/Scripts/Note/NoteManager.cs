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

    [Space]
    public float beatTempo;
    [Tooltip("The delay before the notes starts spawning")]
    public float noteStartDelay;

    [Space]
    [HideInInspector]
    public float difficultyMultiplier;
    [Space]
    public GameObject notePrefab;

    

    private void Start()
    {
        SetDifficulty();
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
