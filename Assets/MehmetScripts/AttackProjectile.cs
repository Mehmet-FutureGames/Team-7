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
