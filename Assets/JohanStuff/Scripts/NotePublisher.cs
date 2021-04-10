using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePublisher : MonoBehaviour
{

    public Action noteHit;
    public Action noteNotHit;

    public void NoteHit()
    {
        if(noteHit != null)
        {
            noteHit();
        }
    }
    public void NoteNotHit()
    {
        if (noteNotHit != null)
        {
            noteNotHit();
        }
    }
}
