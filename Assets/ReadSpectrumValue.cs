using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSpectrumValue : MonoBehaviour
{

    public Vector3 restScale;
    Light light;
    public float stuffval;
    public float speed;

    public float maxVal;
    public float curVal;

    public float tValue;

    public float timer;
    public float setTimer;
    private void Start()
    {
        if(GetComponent<Light>() != null)
        {
            light = GetComponent<Light>();
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            tValue = AudioStuff.spectrumValue;
            timer = setTimer;
        }
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, tValue, 0), ref restScale, Time.deltaTime * speed);
    }

}
