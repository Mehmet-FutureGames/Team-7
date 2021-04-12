using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //SET VARIABLES FROM ENEMYSTATS, DON'T CHANGE THE VARIABLES.
    //THEY GET SET THROUGH THE STATS VARIABLE.
    GameObject enemyModel;
    string enemyName;
    [Space]
    float movementSpeed;
    float attackDamage;


    [SerializeField] EnemyStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats.enemyModel = enemyModel;
        stats.enemyName = enemyName;

        stats.movementSpeed = movementSpeed;
        stats.attackDamage = attackDamage;
    }
    public abstract void EnemyAttack();
 
    public abstract void EnemyMove();
}
