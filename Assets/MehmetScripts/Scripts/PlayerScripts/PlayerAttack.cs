using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerAttack : MonoBehaviour
{
    float damage;

    public float dashDamage;

    Player playerStats;

    void Start()
    {
        playerStats = GetComponentInParent<Player>();

        Invoke("DamageReference", 0.1f);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(damage);

            Debug.Log("I hit an enemy!");
        }
    }

    void DamageReference()
    {
        damage = playerStats.damage;
        dashDamage = playerStats.dashDamage;
        Debug.Log(damage);
    }
}
