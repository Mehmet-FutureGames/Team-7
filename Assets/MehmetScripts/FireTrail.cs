using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrail : ActiveItems
{
    NotePublisher notePublisher;
    private void Start()
    {
        UseItem();
    }
    public override void UseItem()
    {
       Debug.Log("CoolTreail");        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<UseItem>().OnPickUpItem(itemIndex);
        }
    }
}
