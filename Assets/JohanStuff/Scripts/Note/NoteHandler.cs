using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NoteHandler : MonoBehaviour
{

    private float startDelay;

    public GameObject notePrefab;
    
    float timer = 0;
    [SerializeField] Transform hitArea;

    NoteManager noteManager;

    ObjectPooler notePooler;

    bool delayDone;
    bool delayStarted;
    private void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        notePrefab = noteManager.notePrefab;
        noteManager.SetDifficulty();
        notePooler = ObjectPooler.Instance;
        delayDone = false;
        startDelay = noteManager.noteStartDelay;
    }
    void FixedUpdate()
    {
        if (PressAnyKey.hasStarted)
        {
            if(delayDone == false && delayStarted == false)
            {
                StartCoroutine(StartDelay());
                delayStarted = true;
            }
            else if (delayDone)
            {
                timer += Time.fixedDeltaTime;
                if (timer >= (60 / noteManager.beatTempo) * noteManager.difficultyMultiplier)
                {
                    Instantiate(notePrefab, transform.position, Quaternion.identity);
                    timer -= (60 / noteManager.beatTempo) * noteManager.difficultyMultiplier;
                }
            }
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        delayDone = true;
    }

    private void OnLevelWasLoaded(int level)
    {
        timer = 0;
        delayDone = false;
        delayStarted = false;
    }


}
