using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NoteHandler : MonoBehaviour
{

    

    [SerializeField] GameObject notePrefab;
    
    float timer = 0;
    [SerializeField] Transform hitArea;

    NoteManager noteManager;
    
    private void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        
        noteManager.SetDifficulty();
    }
    void FixedUpdate()
    {
        if (PressAnyKey.hasStarted)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= (60/noteManager.beatTempo))
            {
                GameObject prefab = Instantiate(notePrefab, transform);
                timer -= (60/noteManager.beatTempo);
            }
        }

    }


}
