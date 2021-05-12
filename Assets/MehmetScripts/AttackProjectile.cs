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
        if (cooldownReady)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                ObjectPooler.Instance.SpawnFormPool("PlayerProjectile", transform.position);
            }
            Debug.Log("ATTACKPROJECTILE");
            
            StartCoroutine(CountCooldown());
        }

    }
    IEnumerator CountCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            cooldownCount++;
            float value = (cooldownCount / cooldown);
            charge.fillAmount = value;
            if (value >= 1f)
            {
                cooldownReady = true;
                cooldownCount = 0;
                break;
            }
        }
        yield return null;

    }
}
