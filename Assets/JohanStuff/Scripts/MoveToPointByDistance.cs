using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPointByDistance
{
    Vector3 previous;
    public Vector3 Move(Vector3 current, Vector3 target, float deltaSpeed, ref float velocity)
    {
        float distance = (current - target).magnitude;
        Vector3 pos = Vector3.MoveTowards(current, target, distance * deltaSpeed);
        velocity = (pos - previous).magnitude / Time.deltaTime;
        previous = pos;
        return pos;
    }
}
