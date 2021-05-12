using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItems : MonoBehaviour
{
    Image charge;

    public int itemIndex;

    public ItemCanvas itemCanvas;
    protected bool cooldownReady;
    public int cooldown = 5;
    protected int cooldownCount;

    protected virtual void Start()
    {
#if UNITY_STANDALONE
        charge = GameObject.Find("ChargeBarPC").GetComponent<Image>();
#endif
#if UNITY_ANDROID
 charge = GameObject.Find("Chargebar").GetComponent<Image>();
#endif
        itemCanvas = FindObjectOfType<ItemCanvas>();
        cooldownReady = true;
        cooldown = cooldown * 10;
        cooldownCount = cooldown;
    }

    public virtual void PerformAction()
    {

    }


}
