using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboHandler : MonoBehaviour
{
    NotePublisher publisher;
    MovePlayer movePlayer;
    // Start is called before the first frame update

    [HideInInspector]
    public int combo = 0;
    private bool hitNote;

    private void Awake()
    {
        publisher = FindObjectOfType<NotePublisher>();
        movePlayer = FindObjectOfType<MovePlayer>();
    }

    private void OnEnable()
    {
        publisher.noteHit += HitNote;
        publisher.noteNotHit += MissedNote;
    }
    private void Update()
    {
        //if () { }
    }
    private void OnDisable()
    {
        publisher.noteHit -= HitNote;
        publisher.noteNotHit -= MissedNote;
    }

    void MissedNote()
    {
        combo = 0;
        hitNote = false;
    }
    void HitNote()
    {
        hitNote = true;
    }

}
