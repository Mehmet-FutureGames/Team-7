using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItems : MonoBehaviour
{
    

    public int itemIndex;

    public ItemCanvas itemCanvas;
    protected bool cooldownReady;
    public float cooldown = 5;
    protected float cooldownCount;
    protected Image charge;
    protected Image chargeRing;
    protected virtual void Start()
    {
        itemCanvas = FindObjectOfType<ItemCanvas>();
        cooldownReady = true;
        cooldown = cooldown * 100;
        cooldownCount = 0;
    }

    public virtual void PerformAction()
    {

    }
    public void GetChargeBar()
    {
        charge = GetComponent<UseItem>().charge;
        chargeRing = GetComponent<UseItem>().chargeRing;
    }
    public void OnLevelWasLoaded(int level)
    {
        cooldownReady = true;
        cooldownCount = cooldown;
        chargeRing.fillAmount = cooldownCount;
    }

}
