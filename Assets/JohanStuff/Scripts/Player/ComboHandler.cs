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
    float timer;
    [SerializeField] Slider slider;
    public int Combo { get; private set; }
    [SerializeField] Text displayCombo;
    private bool hitNote;
    [Range(0.01f, 2)]
    public float comboDepletionMult;

    private void Update()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime * comboDepletionMult, slider.minValue, slider.maxValue);
        slider.value = timer;
        if (timer <= slider.minValue)
        {
            SetCombo(0);
        }
    }

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
    private void OnDisable()
    {
        publisher.noteHit -= HitNote;
        publisher.noteNotHit -= MissedNote;
    }

    public void SetCombo(int combo)
    {
        if(combo <= 0)
        {
            slider.gameObject.SetActive(false);
        }
        Combo = combo;
        displayCombo.text = "Combo: " + Combo.ToString();
    }

    public void AddToCombo()
    {
        slider.gameObject.SetActive(true);
        timer = slider.maxValue;
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
