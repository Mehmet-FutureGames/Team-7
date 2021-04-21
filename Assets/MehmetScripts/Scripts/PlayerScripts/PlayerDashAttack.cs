using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerDashAttack : MonoBehaviour
{
    public float dashDamage;

    Player playerStats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(dashDamage);
        }
    }
    private void OnEnable()
    {
        playerStats = GetComponentInParent<Player>();
        dashDamage = playerStats.dashDamage;
        playerStats.isAttacking = false;
    }
    public void UpgradeDamage()
    {
        dashDamage = playerStats.dashDamage;
    }
}
