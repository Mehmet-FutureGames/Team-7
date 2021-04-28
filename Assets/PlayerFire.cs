using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private void Start()
    {
        Invoke("DisableAfterSeconds", 10f);
    }

    void DisableAfterSeconds()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
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
