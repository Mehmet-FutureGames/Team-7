using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootFollow : MonoBehaviour
{
    public Transform player;

    Vector3 velocity = Vector3.zero;
    [SerializeField, Range(0, 5)] float disableAfterSeconds;
    [SerializeField] float upwardForce;
    public float speedModifier;
    
    bool follow;
    Rigidbody rb;
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().enabled = false;
        follow = false;
        rb.AddForce(Vector3.up * upwardForce);
        Invoke("SetFollow", disableAfterSeconds);
    }

    void Update()
    {
        float distance = (player.position - transform.position).magnitude;
        if(distance < 30 && follow == true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, speedModifier * Time.deltaTime);
        }
    }
    void SetFollow()
    {
        GetComponent<SphereCollider>().enabled = true;
        rb.isKinematic = true;
        follow = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
