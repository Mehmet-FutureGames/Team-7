using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboHandler : MonoBehaviour
{
    NotePublisher publisher;
    EnemyPublisher enemyPublisher;
    MovePlayer movePlayer;
    float coinMult;
    float frenzyMult;

    [HideInInspector]
    public int Combo { get; private set; }
    [SerializeField] Text displayCombo;
    private bool hitNote;

    private void Awake()
    {
        enemyPublisher = FindObjectOfType<EnemyPublisher>();
        publisher = FindObjectOfType<NotePublisher>();
        movePlayer = FindObjectOfType<MovePlayer>();
    }

    private void OnEnable()
    {
        enemyPublisher.enemyTakeDamage += AddToCombo;
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

    public void AddToCombo()
    {
        Combo += 1;
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
