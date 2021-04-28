using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    //item 0 = Firetrail
    //item 1 = Attack Projectile
    //item 2 = Stun Lock

    public bool[] item;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnPickUpItem(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnUseItem();
        }
    }

    public void OnPickUpItem(int itemIndex) 
    {
        for (int i = 0; i < item.Length; i++)
        {
            item[i] = false;
        }
        item[itemIndex] = true;
        gameObject.AddComponent<FireTrail>();
    }

    void OnUseItem()
    {
        for (int i = 0; i < item.Length; i++)
        {
            switch (item[i])
            {
                case true:
                    Debug.Log(item[i]);
                    Destroy(GetComponent<FireTrail>());
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
