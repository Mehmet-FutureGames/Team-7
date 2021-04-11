using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootFollow : MonoBehaviour
{
    public Transform player;

    Vector3 velocity = Vector3.zero;

    public float speedModifier;

    void Update()
    {
        float distance = (player.position - transform.position).magnitude;
        if(distance < 30)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, speedModifier * Time.deltaTime);
        }
        
    }
}
