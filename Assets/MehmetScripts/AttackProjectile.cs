using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : ActiveItems
{
    NotePublisher notePublisher;
    int projectileCount = 10;
    private void Start()
    {
        UseItem();
    }
    public override void UseItem()
    {
        Debug.Log("CoolTreail");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<UseItem>().OnPickUpItem(itemIndex, this);
            Destroy(gameObject);
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
