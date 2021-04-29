using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text text;

    private float timer;
    int fps;
    void Start()
    {
        //Application.targetFrameRate = 200;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.unscaledTime > timer)
        {
            fps = (int)(1f / Time.unscaledDeltaTime);
            text.text = "FPS: " + fps.ToString();
            timer = Time.unscaledTime + 0.05f;
        }
    }
}
