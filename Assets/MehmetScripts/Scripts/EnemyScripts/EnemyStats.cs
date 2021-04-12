using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public GameObject enemyModel;
    public string enemyName;
    [Space]
    public float movementSpeed;
    public float attackDamage;
    public float health;
}
