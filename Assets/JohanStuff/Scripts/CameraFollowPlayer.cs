using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float camFollowSpeed;
    [SerializeField] Vector3 cameraOffset;

    float distance;
    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        distance = Vector3.Distance(transform.position, player.position + cameraOffset);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position + cameraOffset , distance * camFollowSpeed * Time.deltaTime);
    }
}
