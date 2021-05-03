using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoteObject : MonoBehaviour
{
   
    private NotePublisher publisher;
    public bool canBePressed;
    public bool perfectHit;
    public bool deActivated;

    NoteManager noteManager;
    float timer = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        noteManager = FindObjectOfType<NoteManager>();
        publisher = FindObjectOfType<NotePublisher>();
        GetNoteList.NoteList.Add(this);
    }
    void Start()
    {
        
    }
    private void OnEnable()
    {
        transform.localScale = noteManager.StartScale;
        timer = 0;
        deActivated = false;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }

#if UNITY_STANDALONE
    void Update()
    {
        if (canBePressed && deActivated == false)
        {
            DesktopInput();
        }
    }
#endif
#if UNITY_ANDROID
    void Update()
    {
        if (canBePressed && deActivated == false)
        {
            AndroidInput();
        }
    }
#endif
    private void AndroidInput()
    {
        if (OverGUICheck.Instance.IsPointerOverUIObject())
        {

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                publisher.NoteHit();
                gameObject.SetActive(false);
                canBePressed = false;
                NoteMiss.Instance.TriggerCountZero();
            }
        }
    }

    public void ButtonAttack()
    {
            publisher.NoteHitAttack();
            gameObject.SetActive(false);
            canBePressed = false;
            NoteMiss.Instance.TriggerCountZero();
    }
    public void ButtonBlock()
    {
        publisher.NoteHitBlock();
        gameObject.SetActive(false);
        canBePressed = false;
        NoteMiss.Instance.TriggerCountZero();
    }
    private void DesktopInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            publisher.NoteHit();
            gameObject.SetActive(false);
            canBePressed = false;
            NoteMiss.Instance.TriggerCountZero();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            publisher.NoteHit();
            gameObject.SetActive(false);
            canBePressed = false;
            NoteMiss.Instance.TriggerCountZero();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            publisher.NoteHitBlock();
            gameObject.SetActive(false);
            canBePressed = false;
            NoteMiss.Instance.TriggerCountZero();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            publisher.NoteHitAttack();
            gameObject.SetActive(false);
            canBePressed = false;
            NoteMiss.Instance.TriggerCountZero();
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        transform.localScale = Vector3.Lerp(noteManager.StartScale, Vector3.zero, noteManager.downScaleMultiplier * timer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }
    private void OnDisable()
    {
        canBePressed = false;
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Activator"))
        {
            publisher.NoteNotHit();
            canBePressed = false;
            gameObject.SetActive(false);
        }
    }
    
}
