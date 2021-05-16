using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisable : MonoBehaviour
{
    public float timeToDisable;
    
    private void OnEnable()
    {
        Invoke("DisableThis", timeToDisable);
    }

    void DisableThis()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
