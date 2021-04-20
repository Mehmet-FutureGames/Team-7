using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfEnemy : MonoBehaviour
{
    [SerializeField] int enemyType;

    public int ReturnEnemyType()
    {
        return enemyType;
    }
}
