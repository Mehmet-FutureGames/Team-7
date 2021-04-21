using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerAttack : MonoBehaviour
{
    public float damage;

    [HideInInspector] float dashDuration;

    [HideInInspector] float meleeDuration;

    [HideInInspector] public Player playerStats;

    void Start()
    {
        Invoke("DamageReference", 0.1f);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(damage);
        }
    }

    void DamageReference()
    {
        //Unity doesn't like to reference stuff at the same time as setting other variables
        //this helps it calm down and actually do the job.
        damage = playerStats.damage;
        dashDuration = playerStats.dashAttackDuration;
        meleeDuration = playerStats.meleeAttackDuration;
    }
    private void OnEnable()
    {
        playerStats = GetComponentInParent<Player>();
        playerStats.isAttacking = true;
    }
    private void OnDisable()
    {
        playerStats.isAttacking = false;
    }
}
