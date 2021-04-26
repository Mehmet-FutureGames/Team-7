using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNoteList : MonoBehaviour
{
    public static List<NoteObject> NoteList = new List<NoteObject>();
    NotePublisher notePublisher;
    private void Start()
    {
        notePublisher = FindObjectOfType<NotePublisher>();
    }

    public void PressButton()
    {
        for (int i = 0; i < NoteList.Count; i++)
        {
            NoteList[i].ButtonAttack();
        }
    }
}
