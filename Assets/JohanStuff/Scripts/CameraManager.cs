using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Tooltip("Insert Player object here")]
    [HideInInspector]
    public Transform player;

    public float camFollowSpeed;
    public Vector3 cameraOffset;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
}
