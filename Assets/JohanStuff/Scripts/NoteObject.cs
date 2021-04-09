using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private Publisher publisher;
    public bool canBePressed;

    NoteManager noteManager;

    // Start is called before the first frame update
    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        gameObject.transform.localScale = noteManager.StartScale;
        publisher = FindObjectOfType<Publisher>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canBePressed)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                publisher.NoteHit();
                Destroy(this.gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        transform.localScale = transform.localScale * (Time.fixedDeltaTime + 1) * noteManager.downScaleMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Activator"))
        {
            Debug.Log("Entered area");
            canBePressed = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Activator"))
        {
            publisher.NoteNotHit();
            Debug.Log("Exited area");
            canBePressed = false;
            Destroy(this.gameObject);
        }
    }
    
}
