using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCanvas : MonoBehaviour
{
    Camera mainCamrea;
    void Start()
    {
        mainCamrea = Camera.main;
        transform.LookAt(transform.position + mainCamrea.transform.rotation * Vector3.back, mainCamrea.transform.rotation * Vector3.down);
    }

    private void OnEnable()
    {
        
    }
}
