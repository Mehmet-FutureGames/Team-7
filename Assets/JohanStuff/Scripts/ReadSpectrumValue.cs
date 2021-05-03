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
        transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(0.2f, 2.5f + tValue, 0.1f), ref restScale, Time.deltaTime * speed);
    }

}
