﻿using System;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Action enemyDefeated;

    public StateMachine movementSM;
    public MoveState moveState;
    public AttackState attackState;
    public ChargeAttackState chargeAttackState;
    public IdleState idleState;
    public SecondChargeAttackState secondChargeAttackState;
    WaveManager manager;

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

    private Vector3 attackAreaScale;

    

    float movementSpeed;
    [HideInInspector]
    public float moveDistance;
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
    public int moveCounter = 0;
    int attackCounter = 0;


    public bool hasSubscribedToDeath = false;

    [HideInInspector]
    public float distanceToPlayer;

    [HideInInspector]
    public Transform player;

    [SerializeField] EnemyStats stats;

    NotePublisher notePublisher;

    GameObject floatingText;


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
            ShowFloatingText(damage);
        }
        Dead();
    }

    private void ShowFloatingText(float damage)
    {        
        var text = Instantiate(floatingText, agentObj.transform.position, Quaternion.identity, agentObj.transform);
        text.GetComponent<TextMesh>().text = "Damage: " + damage.ToString();
    }

    private void Dead()
    {
        if (health <= 0)
        {
            if (enemyDefeated != null)
            {
                enemyDefeated();
            }
            enabled = false;
            Invoke("DisableGameObject", 1.5f);

        }
    }

    private void DisableGameObject()
    {
        gameObject.SetActive(false);
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
        detectionRange = stats.detectionRange;
        isRanged = stats.isRanged;
    }
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        movementSM = new StateMachine();

        moveState = new MoveState(this, movementSM);
        chargeAttackState = new ChargeAttackState(this, movementSM);
        attackState = new AttackState(this, movementSM);
        idleState = new IdleState(this, movementSM);
        secondChargeAttackState = new SecondChargeAttackState(this, movementSM);
        SetStats();

        manager = FindObjectOfType<WaveManager>();

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
        movementSM.Initialize(moveState);
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
        movePattern = stats.movePattern;
        notePublisher = FindObjectOfType<NotePublisher>();
        notePublisher.noteHit += EventUpdate;
        notePublisher.noteNotHit += EventUpdate;
        
    }

    private void OnDisable()
    {
        manager.UnSubscribe(this);
        notePublisher.noteHit -= EventUpdate;
        notePublisher.noteNotHit -= EventUpdate;
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

