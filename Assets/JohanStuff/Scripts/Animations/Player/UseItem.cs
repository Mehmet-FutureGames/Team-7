using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    [HideInInspector] public Image charge;
    [HideInInspector] public Image chargeRing;
    public ActiveItems activeItems1;
    public KeyCode keyCodeChange;
    private bool hasItem;
    public bool HasItem
    {
        get { return hasItem; }
        set
        {
            hasItem = value;
            if (hasItem)
            {
                GetComponent<ActiveItems>().GetChargeBar();
                chargeRing.enabled = true;
                charge.enabled = true;
            }
        }
    }
    private void Start()
    {
#if UNITY_STANDALONE
        charge = GameObject.Find("ChargeBarPC").GetComponent<Image>();
        chargeRing = GameObject.Find("ChargeBarRingPC").GetComponent<Image>();
        charge.enabled = false;
#endif
#if UNITY_ANDROID
 chargeRing = GameObject.Find("ChargeBar").GetComponent<Image>();
#endif

        chargeRing.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCodeChange))
        {
            OnUseItem();
        }
    }

    public void OnPickUpItem(ActiveItems activeItems) 
    {

        if(gameObject.GetComponent<ActiveItems>() == null)
        {
            gameObject.AddComponent(activeItems.GetType());
            HasItem = true;
        }
        else
        {
            Destroy(GetComponent<ActiveItems>());
            gameObject.AddComponent(activeItems.GetType());
            chargeRing.fillAmount = 1;
        }
        
    }

    public void OnUseItem()
    {
        if (HasItem)
        {
            GetComponent<ActiveItems>().GetChargeBar();
            GetComponent<ActiveItems>().PerformAction();
        }
        
    }
}
