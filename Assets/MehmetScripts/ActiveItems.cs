using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItems : MonoBehaviour
{


    //protected int cost;
    public int itemIndex;
    //protected string itemName;

    public ItemCanvas itemCanvas;

    protected virtual void Start()
    {
        itemCanvas = FindObjectOfType<ItemCanvas>();
        //cost = GetComponent<ItemParameter>().cost;
        //itemName = GetComponent<ItemParameter>().itemName;
    }

    public virtual void PerformAction()
    {

    }

}
