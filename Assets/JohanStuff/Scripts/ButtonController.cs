using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    Color defaultColor;
    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress)) 
        {
            
            GetComponent<MeshRenderer>().material.color = Color.white;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            GetComponent<MeshRenderer>().material.color = defaultColor;
           
        }
    }
}
