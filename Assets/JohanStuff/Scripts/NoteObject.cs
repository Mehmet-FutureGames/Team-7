using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private NotePublisher publisher;
    public bool canBePressed;

    NoteManager noteManager;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        gameObject.transform.localScale = noteManager.StartScale;
        publisher = FindObjectOfType<NotePublisher>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (canBePressed)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                publisher.NoteHit();
                gameObject.SetActive(false);
                timer = 0;
                canBePressed = false;
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
            timer = 0;
        }
    }
    
}
