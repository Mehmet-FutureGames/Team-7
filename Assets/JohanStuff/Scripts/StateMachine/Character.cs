using UnityEngine;
using UnityEngine.AI;
public class Character : MonoBehaviour
{

    public StateMachine movementSM;
    public MoveState moving;
    public AttackState attack;
    public ChargeAttackState chargeAttack;


    string enemyName;
    [HideInInspector]
    public GameObject agentObj;
    [HideInInspector]
    public NavMeshAgent agent;
    Transform parent;

    float movementSpeed;
    [HideInInspector]
    public float moveDistance;
    [HideInInspector]
    public int notesToMove;
    float detectionRange;

    

    float attackDamage;
    [HideInInspector]
    public float attackRange;
    float health;
    [HideInInspector]
    public MovePattern movePattern;
    [HideInInspector]
    public int moveCounter = 0;
    int attackCounter = 0;

    [HideInInspector]
    public float distanceToPlayer;

    [HideInInspector]
    public Transform player;

    [SerializeField] EnemyStats stats;

    NotePublisher notePublisher;


    #region Methods
    private void EnemyAttack()
    {

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("This " + enemyName + " has " + health + " HP");
        Dead();
    }

    private void Dead()
    {
        if (health < 0)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        movementSM = new StateMachine();

        moving = new MoveState(this, movementSM);
        chargeAttack = new ChargeAttackState(this, movementSM);
        attack = new AttackState(this, movementSM);
        

        enemyName = stats.enemyName;
        movementSpeed = stats.movementSpeed;
        moveDistance = stats.moveDistance;
        attackDamage = stats.attackDamage;
        notesToMove = stats.notesToMove;
        detectionRange = stats.detectionRange;
        health = stats.health;
        attackRange = stats.attackRange;
        parent = GetComponent<Transform>();

        agentObj = Instantiate(stats.enemyModel, parent);

        agent = GetComponentInChildren<NavMeshAgent>();

        player = FindObjectOfType<MovePlayer>().transform;

        Debug.Log("Fear not " + enemyName + " is here");
        movementSM.Initialize(moving);
    }

    private void EventUpdate()
    {
        distanceToPlayer = (agentObj.transform.position - player.position).magnitude;
        Debug.Log(distanceToPlayer);
        movementSM.CurrentState.NoteEventUpdate();

    }

    private void OnEnable()
    {
        movePattern = stats.movePattern;
        notePublisher = FindObjectOfType<NotePublisher>();
        notePublisher.noteHit += EventUpdate;
        notePublisher.noteNotHit += EventUpdate;
    }

    private void Update()
    {
        movementSM.CurrentState.HandleInput();

        movementSM.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();
    }

    #endregion



}

