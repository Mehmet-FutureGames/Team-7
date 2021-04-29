using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemCanvas : MonoBehaviour
{
    Camera mainCamrea;
    public TextMeshProUGUI text;
    public static ItemCanvas Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        mainCamrea = Camera.main;
        transform.LookAt(transform.position - mainCamrea.transform.rotation * Vector3.back, mainCamrea.transform.rotation * Vector3.up);
    }
    void Start()
    {

    }
}
