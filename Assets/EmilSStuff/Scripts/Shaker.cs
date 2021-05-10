using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour

{
    public ShakeScript shakeScript;
    public float duration = 1f;
 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        shakeScript.Shake(duration);
        }
        
    }
}
