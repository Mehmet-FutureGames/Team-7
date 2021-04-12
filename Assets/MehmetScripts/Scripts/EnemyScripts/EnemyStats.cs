using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovePattern
{
    SimpleMove,
    FastMove
}

[CreateAssetMenu(fileName = "New EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public MovePattern movePattern;
    public GameObject enemyModel;
    public string enemyName;
    [Space]
    public float movementSpeed;
    public float moveDistance;
    public float attackDamage;
    public float health;
}
