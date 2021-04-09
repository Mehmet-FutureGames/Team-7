using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    easy,
    normal,
    hard
}

public class NoteHandler : MonoBehaviour
{

    

    [SerializeField] GameObject notePrefab;
    private TempoHandler tempo;
    
    float timer = 0;
    [SerializeField] Transform hitArea;
    public Difficulty difficulty;
    private float multiplier;
    private void Start()
    {
        tempo = FindObjectOfType<TempoHandler>();
        
        SetDifficulty();
    }
    void FixedUpdate()
    {
        if (PressAnyKey.hasStarted)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= (60/tempo.beatTempo))
            {
                GameObject prefab = Instantiate(notePrefab, transform);
                timer -= (60/tempo.beatTempo);
            }
        }

    }

    void SetDifficulty()
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
