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

    float health;

    NavMeshAgent agent;
    Transform parent;

    Transform player;

    [SerializeField] EnemyStats stats;

    NotePublisher notePublisher;
    // Start is called before the first frame update
    void Start()
    {
        enemyName = stats.enemyName;

        movementSpeed = stats.movementSpeed;
        attackDamage = stats.attackDamage;
        health = stats.health;

        parent = GetComponent<Transform>();

        Instantiate(stats.enemyModel, parent);

        agent = GetComponentInChildren<NavMeshAgent>();

        notePublisher = FindObjectOfType<NotePublisher>();

        player = FindObjectOfType<MovePlayer>().transform;

        notePublisher.noteHit += EnemyMove;
        notePublisher.noteNotHit += EnemyMove;

        Debug.Log("Fear not " + enemyName + " is here");
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void EnemyMove()
    {
        Vector3 dir = (player.position - agent.transform.position).normalized;
        Debug.Log(dir);
        float walkDistance = 1.5f;
        agent.SetDestination(agent.transform.position + dir * walkDistance);
        
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
        
    }
    private void EnemyAttack()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
