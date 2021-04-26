using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDrop : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<NoteCurrencyHandler>().AddNoteCurrency(1);
            gameObject.SetActive(false);
        }
    }
}
