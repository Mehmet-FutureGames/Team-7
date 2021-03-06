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
        StartCoroutine(WaitForCount());
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
        if (!NoteManager.IsInShop)
        {
            StartCoroutine(WaitForCount());
        }
    }
    IEnumerator WaitForCount()
    {
        yield return new WaitForSeconds(0.01f);
        TriggerCount = 0;
    }

}
