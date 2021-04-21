using System;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    EnemyPublisher enemyPublisher;
    public Action enemyDefeated;


    #region States
    public StateMachine movementSM;

    public State moveState;
    public State combatPhase1;
    public State combatPhase2;
    public State combatPhase3;
    public State combatPhase4;
    public State combatPhase5;
    public State combatPhase6;
    #endregion

    public bool playerIsInAttackArea;
    [HideInInspector]
    public GameObject area;


    [HideInInspector]
    public bool isRanged;
    string enemyName;
    [HideInInspector]
    public GameObject agentObj;
    [HideInInspector]
    public NavMeshAgent agent;
    Transform parent;
    private GameObject coin;
    private Vector3 attackAreaScale;

    MovePlayer movePlayer;
    float movementSpeed;
    [HideInInspector]
    public float moveDistance;
    [HideInInspector]
    public float defaultMoveDistance;
    [HideInInspector]
    public int notesToMove;
    public float detectionRange;

    

    float attackDamage;
    [HideInInspector]
    public float attackRange;
    [SerializeField] float health;
    [HideInInspector]
    public MovePattern movePattern;
    [HideInInspector]
    public EnemyType enemyType;
    [HideInInspector]
    public int moveCounter = 0;
    int attackCounter = 0;

    [HideInInspector]
    public float distanceToPlayer;

    [HideInInspector]
    public Transform player;

    [SerializeField] EnemyStats stats;

    NotePublisher notePublisher;

    GameObject floatingText;

    WaveManager manager;


    #region Methods
    public void EnemyAttack()
    {
        if (playerIsInAttackArea)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (floatingText)
        {
            enemyPublisher.OnEnemyTakeDamage(); // Sends event upon taking damage. Subscribers: ComboHandler
            ShowFloatingText(damage);
        }
        Dead();
    }


    private void ShowFloatingText(float damage)
    {        
        var text = Instantiate(floatingText, agentObj.transform.position, Quaternion.identity, agentObj.transform);
        text.GetComponent<TextMesh>().text = damage.ToString();
    }



    private void Dead()
    {
        if (health < 0)
        {
            if (enemyDefeated != null)
            {
                enemyDefeated();
            }
            enabled = false;
            agentObj.GetComponent<Collider>().enabled = false;
            Invoke("DisableGameObject", 1.5f);
        }
    }

    private void DisableGameObject()
    {
        InstantiateCoin();
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }

    private void SetStats()
    {
        enemyName = stats.enemyName;
        movementSpeed = stats.movementSpeed;
        moveDistance = stats.moveDistance;
        attackDamage = stats.attackDamage;
        notesToMove = stats.notesToMove;
        detectionRange = stats.detectionRange;
        health = stats.health;
        attackRange = stats.attackRange;
        attackAreaScale = stats.attackAreaScale;
        isRanged = stats.isRanged;
        defaultMoveDistance = moveDistance;
    }
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        movementSM = new StateMachine();
        InitializeEnemyType.Instance.Initialize(this, movementSM);
        SetStats();


        

        parent = GetComponent<Transform>();

        agentObj = Instantiate(stats.enemyModel, parent);
        area = Instantiate(stats.attackAreaShape, agentObj.transform.position, Quaternion.identity, agentObj.transform);
        floatingText = stats.floatingText;
        area.SetActive(false);
        area.transform.localScale = stats.attackAreaScale;
        //gameObject.GetComponentInChildren<EnemyHitArea>().transform.localScale = stats.attackAreaScale;
        agent = GetComponentInChildren<NavMeshAgent>();

        player = FindObjectOfType<MovePlayer>().transform;

        Debug.Log("Fear not " + enemyName + " is here");
        InitializeState(moveState);
    }

    void InitializeState(State state)
    {
        movementSM.Initialize(state);
    }

    private void EventUpdate()
    {
        distanceToPlayer = (agentObj.transform.position - player.position).magnitude;
        
        movementSM.CurrentState.NoteEventUpdate();


    }

    private void Awake()
    {
        

    }
    private void OnEnable()
    {
        manager = FindObjectOfType<WaveManager>();
        manager.Subscribe(this);
        enemyPublisher = FindObjectOfType<EnemyPublisher>();
        movePlayer = FindObjectOfType<MovePlayer>();
        movePattern = stats.movePattern;
        enemyType = stats.enemyType;
        notePublisher = FindObjectOfType<NotePublisher>();
        movePlayer.playerRegMove += EventUpdate;
        notePublisher.noteNotHit += EventUpdate;
        notePublisher.noteHitBlock += EventUpdate;
        
    }

    private void OnDisable()
    {
        
        movePlayer.playerRegMove -= EventUpdate;
        notePublisher.noteNotHit -= EventUpdate;
        notePublisher.noteHitBlock -= EventUpdate;
        manager.UnSubscribe(this);
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
    void InstantiateCoin()
    {
        var coinPrefab = Resources.Load("Coin") as GameObject;
        var coin = GameObject.Instantiate(coinPrefab, agentObj.transform.position, transform.rotation);
    }
    #endregion

}

