using UnityEngine;
using UnityEngine.AI;
public class Character : MonoBehaviour
{

    public StateMachine movementSM;
    public MoveRandomState moving;




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
    float health;

    private MovePattern movePattern;
    [HideInInspector]
    public int moveCounter = 0;
    int attackCounter = 0;


    Transform player;

    [SerializeField] EnemyStats stats;

    NotePublisher notePublisher;

    
    #region Methods
    public void Shoot()
    {

    }

    public void Move()
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

    public void ActivateHitBox()
    {

    }

    public void DeactivateHitBox()
    {

    }
    #endregion
    
    #region MonoBehaviour Callbacks

    private void Start()
    {
        movementSM = new StateMachine();

        moving = new MoveRandomState(this, movementSM);

        

        enemyName = stats.enemyName;
        movementSpeed = stats.movementSpeed;
        moveDistance = stats.moveDistance;
        attackDamage = stats.attackDamage;
        notesToMove = stats.notesToMove;
        detectionRange = stats.detectionRange;
        health = stats.health;
        parent = GetComponent<Transform>();

        agentObj = Instantiate(stats.enemyModel, parent);

        agent = GetComponentInChildren<NavMeshAgent>();

        player = FindObjectOfType<MovePlayer>().transform;

        Debug.Log("Fear not " + enemyName + " is here");
        movementSM.Initialize(moving);
    }

    private void EventUpdate()
    {
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

