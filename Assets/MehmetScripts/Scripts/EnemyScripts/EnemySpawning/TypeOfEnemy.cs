using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfEnemy : MonoBehaviour
{
    [SerializeField] int enemyType;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x,0,transform.position.z);
    }

    public int ReturnEnemyType()
    {
        return enemyType;
    }
}
