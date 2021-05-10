using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItems : MonoBehaviour
{


    public int itemIndex;

    public ItemCanvas itemCanvas;
    protected bool cooldownReady;
    public int cooldown = 5;
    protected int cooldownCount;

    protected virtual void Start()
    {
        itemCanvas = FindObjectOfType<ItemCanvas>();
        cooldownReady = true;
        cooldown = cooldown * 10;
        cooldownCount = cooldown;
    }

    public virtual void PerformAction()
    {

    }


}
