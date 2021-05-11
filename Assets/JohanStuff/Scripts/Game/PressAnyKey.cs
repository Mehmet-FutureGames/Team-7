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
        hasStarted = true;
    }

    public void StartGame()
    {
        hasStarted = true;
    }
}
