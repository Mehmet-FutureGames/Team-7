using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text text;

    private float timer;
    public float minVal;
    int fps;
    void Start()
    {
        
        Application.targetFrameRate = 100;
        text = GetComponent<Text>();
        StartCoroutine(UpdateMinVal());
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

    IEnumerator UpdateMinVal()
    {
        minVal = 1000;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (minVal > fps)
            {
                minVal = fps;
            }
        }
    }
}
