using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    Vector3 moveToPos;

    Publisher publisher;
    public float moveDistance;
    public float moveSpeed;
    private void Awake()
    {
        moveToPos = transform.position + new Vector3(0, 1, 0);
        publisher = FindObjectOfType<Publisher>();
        publisher.noteHit += MovePos;
        publisher.noteNotHit += MovePos;
    }
    private void Update()
    {
        float distance = (transform.position - moveToPos).magnitude;
        transform.position = Vector3.MoveTowards(transform.position, moveToPos , distance * moveSpeed * Time.deltaTime);
    }
    void MovePos()
    {
        moveToPos = new Vector3(transform.position.x + Random.Range(-moveDistance, moveDistance), transform.position.y, transform.position.z + Random.Range(-moveDistance, moveDistance));
    }
}
