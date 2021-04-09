using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatsScroller : MonoBehaviour
{

    private float beatTempo;

    private TempoHandler tempo;

    // Start is called before the first frame update
    void Start()
    {
        tempo = FindObjectOfType<TempoHandler>();
        beatTempo = tempo.beatTempo / 60f;
        transform.position = FindObjectOfType<ButtonController>().transform.position - new Vector3(7, 0, 0);
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
