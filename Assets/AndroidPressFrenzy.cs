using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidPressFrenzy : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
       
    }

    public void PressButton()
    {
        FindObjectOfType<UseItem>().OnUseItem();
    }
}
