using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatsPerMinuteFinder : MonoBehaviour
{
    float timer = 0;
    public int counter;
    public float bpm;
    public float totalTime;
    public float avgTimeBetweenTaps;
    float period;
    float time;

    bool start = false;

    private Action pressed;

    public float timeSinceStart;

    public List<float> beats = new List<float>();
    private void Start()
    {
        pressed += Press;
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (start == true)
        {
            timeSinceStart += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (start == false) 
            {
                StartCoroutine(Stats());
            }
            start = true;
            
            counter++;
            if (pressed != null)
            {
                pressed();
            }

        }
        
    }

    IEnumerator Stats()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < beats.Count; i++)
            {
                time += beats[i];
            }
            totalTime = time;
            time = 0;
            avgTimeBetweenTaps = totalTime / beats.Count;
            bpm = 60 / avgTimeBetweenTaps;
            
        }
    }

    void Press()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (timer < 2f)
            {
                period = timer;
                beats.Add(period);
            }
            timer = 0;

        }
    }
}

