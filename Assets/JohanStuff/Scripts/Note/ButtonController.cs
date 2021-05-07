using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    Color defaultColor;
    public KeyCode keyToPress;
    RaycastHit hit;
    private int rayDist = 3;
    NotePublisher notePublisher;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
        notePublisher = FindObjectOfType<NotePublisher>();
    }

    // Update is called once per frame
    void Update()
    {
        NewMethod();
        //transform.localScale = new Vector3(transform.localScale.x, 3 + AudioReader.bandBuffer[0] * 5, transform.localScale.z) ;
    }

    private void NewMethod()
    {
        if (Input.GetKeyDown(keyToPress) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (NoteMiss.Instance.TriggerCount == 0)
            {
                if (Physics.Raycast(transform.position, Vector3.left, out hit, rayDist))
                {
                    if (hit.collider.gameObject.CompareTag("Note"))
                    {
                        hit.collider.gameObject.GetComponent<NoteObject>().deActivated = true;
                        hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    }
                }
            }
            GetComponent<MeshRenderer>().material.color = Color.white;
        }

        if (Input.GetKeyUp(keyToPress) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Mouse1))
        {
            GetComponent<MeshRenderer>().material.color = defaultColor;

        }
    }
}
