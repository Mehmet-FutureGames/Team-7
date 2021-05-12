using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItems : MonoBehaviour
{
    protected Image charge;

    public int itemIndex;

    public ItemCanvas itemCanvas;
    protected bool cooldownReady;
    public float cooldown = 5;
    protected float cooldownCount;

    protected virtual void Start()
    {
#if UNITY_STANDALONE
        charge = GameObject.Find("ChargeBarPC").GetComponent<Image>();
#endif
#if UNITY_ANDROID
 charge = GameObject.Find("ChargeBar").GetComponent<Image>();
#endif
        itemCanvas = FindObjectOfType<ItemCanvas>();
        cooldownReady = true;
        cooldown = cooldown * 100;
        cooldownCount = 0;
    }

    public virtual void PerformAction()
    {

    }


}
