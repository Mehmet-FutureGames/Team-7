using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboHandler : MonoBehaviour
{
    NotePublisher publisher;
    MovePlayer movePlayer;
    // Start is called before the first frame update

    [HideInInspector]
    public int Combo { get; private set; }
    [SerializeField] Text displayCombo;
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
        
    }
    private void OnDisable()
    {
        publisher.noteHit -= HitNote;
        publisher.noteNotHit -= MissedNote;
    }

    public void SetCombo(int combo)
    {
        Combo = combo;
        displayCombo.text = "Combo: " + Combo.ToString();
    }

    public void AddToCombo(int combo)
    {
        Combo += combo;
        displayCombo.text = "Combo: " + Combo.ToString();
    }

    void MissedNote()
    {
        Combo = 0;
        hitNote = false;
        displayCombo.text = "Combo: " + Combo.ToString();
    }
    void HitNote()
    {
        hitNote = true;
    }

}
