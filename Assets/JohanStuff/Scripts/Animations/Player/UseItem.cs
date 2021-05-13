using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public bool hasItem;
    public ActiveItems activeItems1;
    public KeyCode keyCodeChange;
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
            hasItem = true;
            activeItems1.charge.gameObject.SetActive(true);
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
