using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAttack : PlayerAttack
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(dashDamage);
        }
    }
    private void OnEnable()
    {
        playerStats = GetComponentInParent<Player>();
        playerStats.isAttacking = false;
    }
}
