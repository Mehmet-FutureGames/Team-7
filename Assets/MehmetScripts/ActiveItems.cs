using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItems : MonoBehaviour
{


    public int cost;
    public int itemIndex;
    public string itemName;

    public ItemCanvas itemCanvas;

    protected virtual void Start()
    {
        itemCanvas = FindObjectOfType<ItemCanvas>();

    }

    public virtual void PerformAction()
    {

    }

}
