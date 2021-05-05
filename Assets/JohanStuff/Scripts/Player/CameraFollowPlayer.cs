using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    Transform player;

    float camFollowSpeed;
    Vector3 cameraOffset;

    private CameraManager cameraManager;

    float distance;
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        player = cameraManager.player;
        camFollowSpeed = cameraManager.camFollowSpeed;
        cameraOffset = cameraManager.cameraOffset;
    }

    
    void LateUpdate()
    {
        if (player != null)
        {
            distance = Vector3.Distance(transform.position, player.position + cameraOffset);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + cameraOffset, distance * camFollowSpeed * Time.deltaTime);
        }
    }
}
