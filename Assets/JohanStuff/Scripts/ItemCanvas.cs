using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemCanvas : MonoBehaviour
{
    Camera mainCamrea;
    public TextMeshProUGUI text;
    public static ItemCanvas Instance;
    public static bool isInBuyArea;
    [SerializeField]GameObject panel;
    public TextMeshProUGUI descriptionText;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        mainCamrea = Camera.main;
        transform.LookAt(transform.position - mainCamrea.transform.rotation * Vector3.back, mainCamrea.transform.rotation * Vector3.up);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (isInBuyArea && !panel.activeSelf)
        {
            panel.SetActive(true);
        }
        else if(!isInBuyArea && panel.activeSelf)
        {
            panel.SetActive(false);
        }

    }
}
