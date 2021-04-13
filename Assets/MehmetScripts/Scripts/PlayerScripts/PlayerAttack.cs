using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerAttack : MonoBehaviour
{
    float damage;

    Player playerStats;

    void Start()
    {
        playerStats = GetComponentInParent<Player>();

        Invoke("DamageReference", 0.1f);
    }

    private void OnTriggerEnter(Collider other)
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
        Debug.Log(damage);
    }
}
