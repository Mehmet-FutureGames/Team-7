using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Player playerStats;

    [SerializeField]float health;

    private void Start()
    {
        //playerStats = GetComponentInParent<Player>();

        //health = playerStats.health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
