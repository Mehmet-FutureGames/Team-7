using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPublisher : MonoBehaviour
{
    public Action enemyTakeDamage;

    public void OnEnemyTakeDamage()
    {
        if (enemyTakeDamage != null)
        {
            enemyTakeDamage();
        }
    }
}
