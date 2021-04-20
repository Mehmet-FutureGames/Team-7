using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private NotePublisher publisher;
    public bool canBePressed;
    public bool deActivated;

    NoteManager noteManager;
    float timer = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        noteManager = FindObjectOfType<NoteManager>();
    }
    void Start()
    {
        publisher = FindObjectOfType<NotePublisher>();
    }
    private void OnEnable()
    {
        transform.localScale = noteManager.StartScale;
        timer = 0;
        deActivated = false;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {        
        if (canBePressed && deActivated == false)
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
