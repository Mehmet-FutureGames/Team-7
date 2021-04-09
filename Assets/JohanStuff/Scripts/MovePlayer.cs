using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Publisher publisher;
    Rigidbody rb;
    Vector3 mousePos;
    bool collided;

    Vector3 newPosition;

    void Awake()
    {
        mousePos = transform.position + new Vector3(0, 1, 0);
        rb = GetComponent<Rigidbody>();
        publisher = FindObjectOfType<Publisher>();
        publisher.noteHit += MovePlayerToMousePos;
    }
    private void Update()
    {
        
        if (!collided)
        {
            float distance = (transform.position - mousePos).magnitude;
            transform.position = Vector3.MoveTowards(transform.position, mousePos, (distance + 1) * 5 * Time.deltaTime);
        }
    }

    void MovePlayerToMousePos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            newPosition = hit.point;
        }
        mousePos = newPosition + new Vector3(0,1,0);
        collided = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        mousePos = transform.position;
        collided = true;
        Vector3 dir = other.contacts[0].point - transform.position;
        dir = -dir.normalized;
        rb.AddForce(dir * 40);
    }


    private void OnCollisionStay(Collision other)
    {
        mousePos = transform.position;
        collided = true;
        Vector3 dir = other.contacts[0].point - transform.position;
        dir = -dir.normalized;
        rb.AddForce(dir * 40);
    }

}
