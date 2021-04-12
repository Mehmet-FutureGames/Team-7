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
    int health;
    float moveDistance;
    GameObject agentObj;
    NavMeshAgent agent;
    Transform parent;
    private MovePattern movePattern;
    int num = 0;
    Transform player;

    [SerializeField] EnemyStats stats;

    NotePublisher notePublisher;
    // Start is called before the first frame update
    void Start()
    {
        enemyName = stats.enemyName;
        movementSpeed = stats.movementSpeed;
        moveDistance = stats.moveDistance;
        attackDamage = stats.attackDamage;

        parent = GetComponent<Transform>();

        agentObj = Instantiate(stats.enemyModel, parent);
        
        agent = GetComponentInChildren<NavMeshAgent>();

        

        player = FindObjectOfType<MovePlayer>().transform;

        Debug.Log("Fear not " + enemyName + " is here");
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void OnEnable()
    {
        movePattern = stats.movePattern;
        notePublisher = FindObjectOfType<NotePublisher>();
        SetMovePattern();
    }
    private void OnDisable()
    {
        RemoveMovePattern();
    }

    public void EnemyMove()
    {
        Vector3 dir = (player.position - agentObj.transform.position).normalized;
        float distance = (player.position - agentObj.transform.position).magnitude;
        
        if(distance < moveDistance +1 )
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(agentObj.transform.position + dir * moveDistance);
        }
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            agentObj.transform.rotation = Quaternion.LookRotation(dir);
        }
        
    }
    public void EnemyMoveEveryOther()
    {
        
        if (num == 0)
        {
            Vector3 dir = (player.position - agentObj.transform.position).normalized;
            float distance = (player.position - agentObj.transform.position).magnitude;

            if (distance < moveDistance + 1)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.SetDestination(agentObj.transform.position + dir * moveDistance);
            }
            if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                agentObj.transform.rotation = Quaternion.LookRotation(dir);
            }
            num++;
        }
        else { num = 0; }

    }

    void SetMovePattern()
    {
        switch (movePattern)
        {
            case MovePattern.SimpleMove:
                notePublisher.noteHit += EnemyMoveEveryOther;
                notePublisher.noteNotHit += EnemyMoveEveryOther;
                break;
            case MovePattern.FastMove:
                notePublisher.noteHit += EnemyMove;
                notePublisher.noteNotHit += EnemyMove;
                break;
            default:
                break;
        }
    }

    void RemoveMovePattern()
    {
        switch (movePattern)
        {
            case MovePattern.SimpleMove:
                notePublisher.noteHit -= EnemyMove;
                notePublisher.noteNotHit -= EnemyMove;
                break;
            case MovePattern.FastMove:
                notePublisher.noteHit -= EnemyMove;
                notePublisher.noteNotHit -= EnemyMove;
                break;
            default:
                break;
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
