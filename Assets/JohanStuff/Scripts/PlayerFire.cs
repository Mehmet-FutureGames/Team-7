using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private void Start()
    {
        Invoke("DisableAfterSeconds", 15f);
    }

    void DisableAfterSeconds()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        
    }
    private void OnEnable()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(5, false);
        }
    }
}
