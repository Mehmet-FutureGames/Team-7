using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    public float RotateSpeed = 0.001f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 35 + Time.time * RotateSpeed);
    }
}
