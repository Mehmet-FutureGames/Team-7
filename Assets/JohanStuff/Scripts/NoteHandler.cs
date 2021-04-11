using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NoteHandler : MonoBehaviour
{

    

    GameObject notePrefab;
    
    float timer = 0;
    [SerializeField] Transform hitArea;

    NoteManager noteManager;

    NotePooler notePooler;

    private void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        notePrefab = noteManager.notePrefab;
        noteManager.SetDifficulty();
        notePooler = NotePooler.Instance;
    }
    void FixedUpdate()
    {
        if (PressAnyKey.hasStarted)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= (60/noteManager.beatTempo) * noteManager.difficultyMultiplier)
            {
                notePooler.SpawnFormPool("Note", notePooler.transform.position, Quaternion.identity, noteManager.StartScale);
                timer -= (60/noteManager.beatTempo) * noteManager.difficultyMultiplier;
            }
        }

    }


}
