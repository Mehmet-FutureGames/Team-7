using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePublisher : MonoBehaviour
{

    public Action noteHit;
    public Action noteNotHit;
    public Action noteHitBlock;
    public Action noteHitAttack;
    public Action buttonHitAttack;
    public static NotePublisher Instance;

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
    public void NoteHitAttack()
    {
        if (noteHitAttack != null)
        {
            noteHitAttack();
        }
    }
    public void NoteButtonHitAttack()
    {
        if (buttonHitAttack != null)
        {
            buttonHitAttack();
        }
    }
}
