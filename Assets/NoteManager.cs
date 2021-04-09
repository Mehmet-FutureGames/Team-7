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
    [Range(0.8f, 1)]
    public float downScaleMultiplier;

    [Space]
    public Difficulty difficulty;

    [Space]
    public float beatTempo;

    private float multiplier;
    [Space]
    public GameObject notePrefab;

    public void SetDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.easy:
                multiplier = 2;
                break;
            case Difficulty.normal:
                multiplier = 1;
                break;
            case Difficulty.hard:
                multiplier = 0.5f;
                break;
        }
    }
}
