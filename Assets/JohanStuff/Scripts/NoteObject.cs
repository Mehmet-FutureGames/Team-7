using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public Publisher publisher;
    public bool canBePressed;

    public float delay;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        publisher = FindObjectOfType<Publisher>();
        Destroy(this.gameObject, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (canBePressed)
        {
            timer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                publisher.NoteHit();
                Destroy(this.gameObject);
            }
        }
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
        }
    }
    
}
