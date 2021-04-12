using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Player playerStats;

    float health;

    private void Start()
    {
        playerStats = GetComponentInParent<Player>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
