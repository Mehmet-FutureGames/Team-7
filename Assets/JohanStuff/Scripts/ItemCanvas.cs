using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemCanvas : MonoBehaviour
{
    Camera mainCamrea;
    public TextMeshProUGUI text;
    void Start()
    {
        mainCamrea = Camera.main;
        transform.LookAt(transform.position - mainCamrea.transform.rotation * Vector3.back, mainCamrea.transform.rotation * Vector3.up);
    }

    private void OnEnable()
    {
        
    }
}
