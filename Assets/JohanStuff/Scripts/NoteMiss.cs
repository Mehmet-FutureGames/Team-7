using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMiss : MonoBehaviour
{
    private int triggerCount;
    public int TriggerCount 
    { 
        get 
        {
            return triggerCount; 
        } 
        set 
        { 
            triggerCount = value; 
        } 
    }

    public static NoteMiss Instance;

    public void TriggerCountZero()
    {
        TriggerCount = 0;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerCount--;
    }
}
