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
        button.onClick.AddListener(PressButton);
    }

    public void PressButton()
    {
        for (int i = 0; i < NoteList.Count; i++)
        {
            if (NoteList[i].canBePressed && !NoteList[i].deActivated)
            {
                Debug.Log("What is happening");
                NoteList[i].ButtonAttack();
            }
        }
    }
}
