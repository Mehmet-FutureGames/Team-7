using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public static bool hasStarted;

    [SerializeField] GameObject textField;

    public AudioSource audio;

    void Start()
    {
        hasStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
                audio.Play();
            }
        }
    }
}
