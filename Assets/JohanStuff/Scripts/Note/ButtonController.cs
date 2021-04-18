using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    Color defaultColor;
    public KeyCode keyToPress;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.left, Color.green);
        if (Input.GetKeyDown(keyToPress)) 
        {
            if(NoteMiss.Instance.TriggerCount == 0)
            {
                if(Physics.Raycast(transform.position, Vector3.left, out hit, 1))
                {
                    if (hit.collider.gameObject.CompareTag("Note")) 
                    {
                        hit.collider.gameObject.GetComponent<NoteObject>().deActivated = true;
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    }
                }
            }
            GetComponent<MeshRenderer>().material.color = Color.white;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            GetComponent<MeshRenderer>().material.color = defaultColor;
           
        }
    }
}
