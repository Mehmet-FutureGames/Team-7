using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float damage;

    Player playerStats;

    void Start()
    {
        playerStats = GetComponentInParent<Player>();

        damage = playerStats.damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(damage);

            Debug.Log("I hit an enemy!");
        }
    }
}
