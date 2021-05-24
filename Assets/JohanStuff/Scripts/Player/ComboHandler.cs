using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ComboHandler : MonoBehaviour
{
    public static ComboHandler Instance;
    NoteHandler noteHandler;
    NotePublisher publisher;
    EnemyPublisher enemyPublisher;
    MovePlayer movePlayer;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI displayCombo;
    private bool hitNote;
    [Range(1, 20)]
    public int comboDepletionCount;
    public static float ComboMult;
    float dampVel;
    private int combo;
    bool sliderRunning;
    bool hasHit;
    float nextSliderVal;
    public int Combo 
    {
        get { return combo; }
        private set 
        { 
            combo = value;
            if(combo <= 0)
            {
                displayCombo.text = "";
            }
            else
            {
                displayCombo.text = "X " + Combo.ToString();
            }
            ComboMult = combo * 0.01f;
        }
    }



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else { Debug.Log("Warning!: " + this + " multiple instnce"); }
        noteHandler = FindObjectOfType<NoteHandler>();
        enemyPublisher = FindObjectOfType<EnemyPublisher>();
        publisher = FindObjectOfType<NotePublisher>();
        movePlayer = FindObjectOfType<MovePlayer>();
    }
    private void Start()
    {
        Combo = 0;
        sliderRunning = false;
        slider.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        enemyPublisher.enemyTakeDamage += AddToCombo;
        publisher.noteHit += HitNote;
        publisher.noteNotHit += MissedNote;
        noteHandler.beat += StartSlide;
    }
    private void OnDisable()
    {
        enemyPublisher.enemyTakeDamage -= AddToCombo;
        noteHandler.beat -= StartSlide;
        publisher.noteHit -= HitNote;
        publisher.noteNotHit -= MissedNote;
    }

    void StartSlide()
    {
        
        if (!sliderRunning && hasHit)
        {
            StartCoroutine(Slide());
        }
    }
    IEnumerator Slide() // moves the combo slider down every time a beatNote spwans. To move it to the beat instead of just moving it down with time.
    {
        sliderRunning = true;
        float startSliderVal = slider.maxValue;
        float reduction = slider.maxValue / comboDepletionCount;
        nextSliderVal = slider.value - reduction;
        while (true)
        {
            slider.value = Mathf.SmoothDamp(slider.value, nextSliderVal, ref dampVel, 0.1f);
            yield return new WaitForFixedUpdate();
            if(nextSliderVal + 0.05f >= slider.value)
            {
                break;
            }
            if (slider.value <= 0.01f)
            {
                hasHit = false;
                slider.gameObject.SetActive(false);
                SetCombo(0);
                break;
            }
        }
        sliderRunning = false;

    }
    public void SetCombo(int combo)
    {
        if(combo <= 0)
        {
            slider.gameObject.SetActive(false);
        }
        Combo = combo;
        
    }

    public void AddToCombo()
    {
        slider.gameObject.SetActive(true);
        slider.value = slider.maxValue;
        nextSliderVal = slider.value - (slider.maxValue / comboDepletionCount);
        hasHit = true;
        Combo += 1;
    }

    void MissedNote()
    {
        Combo = 0;
        hitNote = false;
    }
    void HitNote()
    {
        hitNote = true;
    }
    private void OnLevelWasLoaded(int level)
    {
        slider.gameObject.SetActive(false);
        Combo = 0;
        displayCombo.text = "";
    }

}
