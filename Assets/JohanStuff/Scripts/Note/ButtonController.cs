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

    private void Update()
    {
        NewMethod();
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
                        hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color32(29, 27, 117, 255);
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
