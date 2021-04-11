using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Tooltip("Insert Player object here")]
    public Transform player;

    public float camFollowSpeed;
    public Vector3 cameraOffset;
}
