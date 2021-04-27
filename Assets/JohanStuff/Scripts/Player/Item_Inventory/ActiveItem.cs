using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : MonoBehaviour
{
    public ActiveItemSlot container;

    public void AddItem(ActiveItemObject item)
    {
        bool hasItem = false;
        if(container.item == item)
        {
            hasItem = true;
            return;
        }
        container.item = item;
    }
    void UseItem()
    {
        container.item.PerformItemAction();
    }
}
[System.Serializable]
public class ActiveItemSlot
{
    public ActiveItemObject item;
}


public abstract class ActiveItemObject : MonoBehaviour
{
    public abstract void PerformItemAction();
    public abstract void OnTriggerEnter(Collider other);

}
public class FireTrail : ActiveItemObject
{

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ActiveItem>().AddItem(this);
        }
    }

    public override void PerformItemAction()
    {
        // do firetrail stuff
    }
}
