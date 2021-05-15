using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetNoteList : MonoBehaviour
{
    public static List<NoteObject> NoteList = new List<NoteObject>();
    NotePublisher notePublisher;
    private Button button;
    private void Start()
    {
        notePublisher = FindObjectOfType<NotePublisher>(); 
        button = GetComponent<Button>();
    }

    public void PressButton()
    {
        for (int i = 0; i < NoteList.Count; i++)
        {
            if ((NoteList[i].canBePressed && !NoteList[i].deActivated))
            {
                if (gameObject.CompareTag("AttackButton"))
                {
                    NoteList[i].ButtonAttack();
                }
                else if (gameObject.CompareTag("BlockButton"))
                {
                    NoteList[i].ButtonBlock();
                }
            }
        }
    }
}
