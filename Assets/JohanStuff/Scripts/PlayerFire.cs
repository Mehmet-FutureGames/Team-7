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
        gameObject.SetActive(false); // using Set active(false) since it's part of the objectpooler.
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
            other.GetComponentInParent<Enemy>().TakeFireDamage(5, false);
        }
    }
}
