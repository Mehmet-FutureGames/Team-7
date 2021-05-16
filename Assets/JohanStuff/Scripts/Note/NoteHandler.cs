using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NoteHandler : MonoBehaviour
{
    public Action beat;
    private float startDelay;

    public GameObject notePrefab;
    
    float timer = 0;
    [SerializeField] Transform hitArea;

    NoteManager noteManager;

    ObjectPooler notePooler;
    int counter;
    bool delayDone;
    bool delayStarted;
    bool isInShop;
    private void Awake()
    {
        noteManager = FindObjectOfType<NoteManager>();
        notePrefab = noteManager.notePrefab;
    }
    public void NoteHandlerInitialize()
    {
        startDelay = noteManager.noteStartDelay;
        noteManager.SetDifficulty();
        notePooler = ObjectPooler.Instance;
        delayDone = false;
    }
    void FixedUpdate()
    {
        if (!NoteManager.IsInShop)
        {
            if (PressAnyKey.hasStarted)
            {
                if (delayDone == false && delayStarted == false)
                {
                    StartCoroutine(StartDelay());
                    delayStarted = true;
                }
                else if (delayDone)
                {
                    timer += Time.fixedDeltaTime;
                    if (timer >= (60 / noteManager.beatTempo) * noteManager.difficultyMultiplier)
                    {
                        Instantiate(notePrefab, transform.position, Quaternion.identity, ObjectPooler.Instance.transform); // spawns beatNotes to the beat.
                        timer -= (60 / noteManager.beatTempo) * noteManager.difficultyMultiplier;
                        if (beat != null)
                        {
                            beat();
                        }
                    }
                }
            }
        }

    }

    public IEnumerator Wait()
    {
        // Instantiates beatNotes to be able to move sooner when the game starts.
        yield return new WaitForSeconds(0.0001f);
        Vector3 delayOffset = new Vector3(noteManager.noteStartDelay, 0, 0);
        Instantiate(notePrefab, transform.position, Quaternion.identity, ObjectPooler.Instance.transform);
        Instantiate(notePrefab, transform.position + Vector3.right * 2 - delayOffset, Quaternion.identity, ObjectPooler.Instance.transform);
        Instantiate(notePrefab, transform.position + Vector3.right * 4 - delayOffset, Quaternion.identity, ObjectPooler.Instance.transform);
        Instantiate(notePrefab, transform.position + Vector3.right * 6 - delayOffset, Quaternion.identity, ObjectPooler.Instance.transform);
    }

        IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(startDelay); // delays the spawning of beatNotes so it syncs better with the music. this is the calibration delay.
            delayDone = true;
        }

    private void OnLevelWasLoaded(int level)
    {
        timer = 0;
        delayDone = false;
        delayStarted = false;

    }
}
