using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //SET VARIABLES FROM ENEMYSTATS, DON'T CHANGE THE VARIABLES.
    //THEY GET SET THROUGH THE STATS VARIABLE.
    string enemyName;
    [Space]
    float movementSpeed;
    float attackDamage;

    NavMeshAgent agent;

    Transform player;

    [SerializeField] EnemyStats stats;
    // Start is called before the first frame update
    void Start()
    {
        enemyName = stats.enemyName;

        movementSpeed = stats.movementSpeed;
        attackDamage = stats.attackDamage;

        agent = GetComponent<NavMeshAgent>();

        Debug.Log("Fear not " + enemyName + " is here");
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void EnemyMove()
    {
        agent.SetDestination(player.transform.position);
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }
    private void EnemyAttack()
    {

    }
}
