using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    public delegate void MethodDelegate();
    public UnityEvent unityEvent;
    public IEnumerator NonReturnTimer(float time, MethodDelegate method)
    {
        yield return new WaitForSeconds(time);
        method();
    }
    public IEnumerator ReturnTimer(float time, int updates, System.Action<float> outputVal, MethodDelegate method)
    {
        float interval = time / updates;
        float timeVal = 0;
        while(timeVal <= time)
        {
            yield return new WaitForSeconds(interval);
            timeVal += interval;
            outputVal(timeVal);
        }
        method();
    }
    public IEnumerator ReturnTimer(float time, int updates, System.Action<float> outputVal)
    {
        float interval = time / updates;
        float timeVal = 0;
        while (timeVal <= time)
        {
            yield return new WaitForSeconds(interval);
            timeVal += interval;
            outputVal(timeVal);
        }
    }
}
