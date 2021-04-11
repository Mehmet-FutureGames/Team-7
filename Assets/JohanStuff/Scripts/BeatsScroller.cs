using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatsScroller : MonoBehaviour
{

    private float beatTempo;

    private NoteManager NoteManager;
    // Start is called before the first frame update
    void Start()
    {
        NoteManager = FindObjectOfType<NoteManager>();
        beatTempo = NoteManager.beatTempo / 60f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PressAnyKey.hasStarted == true)
        {
            transform.position += new Vector3(beatTempo * Time.fixedDeltaTime, 0, 0);
        }
    }
}
