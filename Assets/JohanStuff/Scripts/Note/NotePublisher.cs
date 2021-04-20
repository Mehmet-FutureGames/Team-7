using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePublisher : MonoBehaviour
{

    public Action noteHit;
    public Action noteNotHit;
    public Action noteHitBlock;
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
    public void NoteHitBlock()
    {
        if(noteHitBlock != null)
        {
            noteHitBlock();
        }
    }
}
