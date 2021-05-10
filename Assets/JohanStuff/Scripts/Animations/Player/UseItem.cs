using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    private bool hasItem;
    public ActiveItems activeItems1;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnUseItem();
        }
    }

    public void OnPickUpItem(ActiveItems activeItems) 
    {

        if(gameObject.GetComponent<ActiveItems>() == null)
        {
            gameObject.AddComponent(activeItems.GetType());
            hasItem = true;
        }
        else
        {
            Destroy(GetComponent<ActiveItems>());
            gameObject.AddComponent(activeItems.GetType());
        }
        
    }

    public void OnUseItem()
    {
        if (hasItem)
        {
            GetComponent<ActiveItems>().PerformAction();
        }
        
    }
}
