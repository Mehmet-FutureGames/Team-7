using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
