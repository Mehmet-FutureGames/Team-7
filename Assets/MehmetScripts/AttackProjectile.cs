using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : ActiveItems
{
    NotePublisher notePublisher;
    int projectileCount = 10;
    protected override void Start()
    {
        base.Start();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Player"))
        {
            //Display item

            /*
            other.GetComponent<UseItem>().OnPickUpItem(itemIndex, this);
            Destroy(gameObject);
            */
        }
    }
    public override void PerformAction()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            ObjectPooler.Instance.SpawnFormPool("PlayerProjectile", transform.position);
        }
        Debug.Log("ATTACKPROJECTILE");
        Destroy(this);
    }
}
