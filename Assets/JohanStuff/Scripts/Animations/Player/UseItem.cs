using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    //item 0 = Firetrail
    //item 1 = Attack Projectile
    //item 2 = Stun Lock
    public ActiveItems activeItems1;
    public bool[] item;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //OnPickUpItem(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnUseItem();
        }
    }

    public void OnPickUpItem(int itemIndex, ActiveItems activeItems) 
    {
        for (int i = 0; i < item.Length; i++)
        {
            item[i] = false;
        }
        item[itemIndex] = true;
        //activeItems1 = activeItems;
        
        if(gameObject.GetComponent<ActiveItems>() == null)
        {
            gameObject.AddComponent(activeItems.GetType());
            //activeItems1 = activeItems;
        }
        else
        {
            Destroy(GetComponent<ActiveItems>());
            gameObject.AddComponent(activeItems.GetType());
        }
        
    }

    void OnUseItem()
    {
        for (int i = 0; i < item.Length; i++)
        {
            switch (item[i])
            {
                case true:
                    Debug.Log(item[i]);
                    GetComponent<ActiveItems>().PerformAction();
                    //Destroy(GetComponent(itemSelected.GetType()));
                    item[i] = false;
                    break;
                case false:
                    Debug.Log(item[i]);
                    break;
                default:
                    break;
            }
        }
    }
}
