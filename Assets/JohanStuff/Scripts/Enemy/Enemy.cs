using System;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public AudioClip enemysound;
    public AudioClip enemy2sound;


    EnemyPublisher enemyPublisher;
    public Action enemyDefeated;
    public Animator animator;
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
    public Vector3 attackAreaScale;

    MovePlayer movePlayer;
    float movementSpeed;
    [HideInInspector]
    public float moveDistance;
    [HideInInspector]
    public float defaultMoveDistance;
    [HideInInspector]
    public int notesToMove;
    public float detectionRange;

    float noteDropChance;
    bool noteWillDrop = false;

    private int coinMinDropCount;
    private int coinMaxDropCount;
    private float coinValue;

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
        AudioSource.PlayClipAtPoint(enemysound, transform.position);

        if (playerIsInAttackArea)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    public void EnemyRangedAttack()
    {
        ObjectPooler.Instance.SpawnFormPool("EnemyBomb", area.transform.position); // for explosion animation
        AudioSource.PlayClipAtPoint(enemy2sound, transform.position);
        if (playerIsInAttackArea)
        {
            player.GetComponent<PlayerHealth>().TakeRangedDamage(attackDamage);
        }
    }
    public void TakeDamage(float damage, bool isDash)
    {
        health -= damage;
        if (floatingText)
        {
            GameObject blood = ObjectPooler.Instance.SpawnFormPool("Blood", agentObj.transform.position, transform.rotation);
            if (!isDash)
            {
                PlayerFrenzy.Instance.AddFrenzy();
                ComboHandler.Instance.AddToCombo();
            }
            else { ComboHandler.Instance.AddToCombo(); }
            
            //enemyPublisher.OnEnemyTakeDamage(); // Sends event upon taking damage. Subscribers: ComboHandler
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
        if (health <= 0)
        {
            ObjectPooler.Instance.SpawnFormPool("EnemyExplosion", agentObj.transform.position);
            if (enemyDefeated != null)
            {
                enemyDefeated();
            }
            Player.EnemyTransforms.Remove(agentObj.transform);
            SetDropNote(ComboHandler.ComboMult);
            agentObj.GetComponent<Collider>().enabled = false;
            enabled = false;
            //ObjectPooler.Instance.SpawnFormPool("EnemyExplosion", this.transform.position);
            DisableGameObject();
        }
    }

    private void DisableGameObject()
    {
        if (noteWillDrop)
        {
            // Drop Note
            SpawnNoteCurrency();
        }
        SpawnCoin(UnityEngine.Random.Range(coinMinDropCount, coinMaxDropCount + 1));
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
        noteDropChance = stats.noteDropChance;
        coinMinDropCount = stats.coinMinDropCount;
        coinMaxDropCount = stats.coinMaxDropCount;
        coinValue = stats.coinValue;
}
    void SetDropNote(float combo)
    {
        float chance = UnityEngine.Random.Range(0, 100f);
        noteDropChance = Mathf.Clamp(noteDropChance * (combo + 1), 0f, 100f);
        if(chance <= noteDropChance)
        {
            noteWillDrop = true;
        }
        else { noteWillDrop = false;}
    }
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {

        animator = agentObj.GetComponentInChildren<Animator>();
        floatingText = stats.floatingText;
        area.SetActive(false);
        area.transform.localScale = stats.attackAreaScale;
        //gameObject.GetComponentInChildren<EnemyHitArea>().transform.localScale = stats.attackAreaScale;
        agent = GetComponentInChildren<NavMeshAgent>();

        player = FindObjectOfType<MovePlayer>().transform;
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
        notePublisher.noteHitAttack += EventUpdate;

        movementSM = new StateMachine();
        InitializeEnemyType.Instance.Initialize(this, movementSM);
        SetStats();

        parent = GetComponent<Transform>();

        agentObj = Instantiate(stats.enemyModel, parent);
        Player.EnemyTransforms.Add(agentObj.transform);
        area = Instantiate(stats.attackAreaShape, agentObj.transform.position, Quaternion.identity, agentObj.transform);
    }

    private void OnDisable()
    {
        
        movePlayer.playerRegMove -= EventUpdate;
        notePublisher.noteNotHit -= EventUpdate;
        notePublisher.noteHitBlock -= EventUpdate;
        notePublisher.noteHitAttack -= EventUpdate;
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

    void SpawnNoteCurrency()
    {
        GameObject noteCurrency = ObjectPooler.Instance.SpawnFormPool("NoteCurrency", agentObj.transform.position, transform.rotation);
    }

    void SpawnCoin(int amount)
    {
        if(coinMinDropCount >= coinMaxDropCount)
        {
            amount = coinMinDropCount;
        }
        if(amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject coin = ObjectPooler.Instance.SpawnFormPool("Coin", agentObj.transform.position, transform.rotation);
                coin.GetComponent<CoinDrop>().SetCoinValue(coinValue * (ComboHandler.ComboMult + 1));
            }
        }
    }
    #endregion

}

