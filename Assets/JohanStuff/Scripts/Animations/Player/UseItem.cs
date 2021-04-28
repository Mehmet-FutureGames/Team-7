using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    //item 0 = Firetrail
    //item 1 = Attack Projectile
    //item 2 = Stun Lock

    public bool[] item;

    ActiveItems itemSelected;

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

    public void OnPickUpItem(int itemIndex, ActiveItems items) 
    {
        for (int i = 0; i < item.Length; i++)
        {
            item[i] = false;
        }
        item[itemIndex] = true;
        gameObject.AddComponent(items.GetType());
        itemSelected = items;
    }

    void OnUseItem()
    {
        for (int i = 0; i < item.Length; i++)
        {
            switch (item[i])
            {
                case true:
                    Debug.Log(item[i]);
                    Destroy(GetComponent(itemSelected.GetType()));
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
