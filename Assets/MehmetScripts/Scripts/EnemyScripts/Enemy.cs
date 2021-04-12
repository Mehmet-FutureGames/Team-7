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

    Transform parent;

    NotePublisher publisher;



    [SerializeField] EnemyStats stats;
    // Start is called before the first frame update
    void Start()
    {
        enemyName = stats.enemyName;

        movementSpeed = stats.movementSpeed;
        attackDamage = stats.attackDamage;

        parent = GetComponent<Transform>();

        Debug.Log("Fear not " + enemyName + " is here");

        Instantiate(stats.enemyModel, parent);

        agent = GetComponentInChildren<NavMeshAgent>();

        agent.speed = movementSpeed;

        publisher = FindObjectOfType<NotePublisher>();

        publisher.noteHit += EnemyMove;
        publisher.noteNotHit += EnemyMove;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void EnemyMove()
    {
            agent.SetDestination(player.transform.position);
    }
    private void EnemyAttack()
    {

    }
}
