using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    private void Awake()
    {
        noteManager = FindObjectOfType<NoteManager>();
        notePrefab = noteManager.notePrefab;
    }
    private void Start()
    {
        
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
                    Instantiate(notePrefab, transform.position, Quaternion.identity, ObjectPooler.Instance.transform);
                    timer -= (60 / noteManager.beatTempo) * noteManager.difficultyMultiplier;
                }
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        Vector3 delayOffset = new Vector3(noteManager.noteStartDelay, 0, 0);
        Instantiate(notePrefab, transform.position, Quaternion.identity, ObjectPooler.Instance.transform);
        Instantiate(notePrefab, transform.position +Vector3.right * 2 - delayOffset, Quaternion.identity, ObjectPooler.Instance.transform);
        Instantiate(notePrefab, transform.position + Vector3.right * 4 - delayOffset, Quaternion.identity, ObjectPooler.Instance.transform);
        Instantiate(notePrefab, transform.position + Vector3.right * 6 - delayOffset, Quaternion.identity, ObjectPooler.Instance.transform);
        Debug.Log("!!!");
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
        if (level == SceneManager.GetSceneByName("Shop").buildIndex || level == SceneManager.GetSceneByName("CoinShop").buildIndex)
        {
            StartCoroutine(Wait());
        }
        
    }


}
