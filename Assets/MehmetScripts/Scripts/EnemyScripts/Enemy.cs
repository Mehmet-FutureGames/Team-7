using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
     
    //SET VARIABLES FROM ENEMYSTATS, DON'T CHANGE THE VARIABLES.
    //THEY GET SET THROUGH THE STATS VARIABLE.
    string enemyName;
    GameObject agentObj;
    NavMeshAgent agent;
    Transform parent;

    float movementSpeed;
    float moveDistance;
    int notesToMove;
    float detectionRange;

    float attackDamage;
    float health;

    private MovePattern movePattern;
    int moveCounter = 0;
    int attackCounter = 0;
    

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
        notesToMove = stats.notesToMove;
        detectionRange = stats.detectionRange;
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

    #region Movement Patterns
    public void EnemyRandomMovement()
    {
        moveCounter++;
        Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        Vector3 agentToRandom = agentObj.transform.position + randomVector;
        Vector3 dir = (agentToRandom - agentObj.transform.position).normalized;
        agent.SetDestination(agentObj.transform.position + dir * moveDistance);

        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            agentObj.transform.rotation = Quaternion.LookRotation(dir);
        }
        if (moveCounter == notesToMove) { moveCounter = 0; }
    }

    public void EnemyProximityDetection()
    {
        float distance = (player.position - agentObj.transform.position).magnitude;
        if(distance < detectionRange)
        {
            EnemyMoveTowardsPlayer();
        }
        else
        {
            EnemyRandomMovement();
        }
    }

    public void EnemyMoveTowardsPlayer()
    {
        moveCounter++;
        if (moveCounter == notesToMove)
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
            
        }
        if(moveCounter == notesToMove) { moveCounter = 0; }
        
    }
    #endregion

    void SetMovePattern()
    {
        switch (movePattern)
        {
            case MovePattern.TowardsPlayer:
                notePublisher.noteHit += EnemyMoveTowardsPlayer;
                notePublisher.noteNotHit += EnemyMoveTowardsPlayer;
                break;
            case MovePattern.RandomDirection:
                notePublisher.noteHit += EnemyRandomMovement;
                notePublisher.noteNotHit += EnemyRandomMovement;
                break;
            case MovePattern.ProximityDetection:
                notePublisher.noteHit += EnemyProximityDetection;
                notePublisher.noteNotHit += EnemyProximityDetection;
                break;
            default:
                break;
        }
    }

    void RemoveMovePattern()
    {
        switch (movePattern)
        {
            case MovePattern.TowardsPlayer:
                notePublisher.noteHit -= EnemyMoveTowardsPlayer;
                notePublisher.noteNotHit -= EnemyMoveTowardsPlayer;
                break;
            case MovePattern.RandomDirection:
                notePublisher.noteHit -= EnemyRandomMovement;
                notePublisher.noteNotHit -= EnemyRandomMovement;
                break;
            case MovePattern.ProximityDetection:
                notePublisher.noteHit -= EnemyProximityDetection;
                notePublisher.noteNotHit -= EnemyProximityDetection;
                break;
            default:
                break;
        }
    }

    private void EnemyAttack()
    {

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
