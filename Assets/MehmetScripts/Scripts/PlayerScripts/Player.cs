using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    bool hasStarted = false;
    Player player;
    public static List<Transform> EnemyTransforms = new List<Transform>();

    public PlayerStats stats;

    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask enemyLayer;

    GameObject character;

    ObjectReferences playerChoose;
    PlayerModels models;
    NoteManager noteManager;
    MovePlayer movePlayer;
    RaycastHit hit;
    #region VariableSetInScriptableObject
    PlayerAttack playerAttackRange;

    PlayerDashAttack playerDashRange;

    PlayerFrenzy playerFrenzy;

    NotePublisher notePublisher;

    int distanceToClick;

    string playerName;

    [HideInInspector]
    public GameObject playerDamageText;
    [HideInInspector]
    public float maxHealth;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float dashDamage;
    [HideInInspector]
    public float dashAttackDuration;
    [HideInInspector]
    public float meleeAttackDuration;
    [HideInInspector]
    float dashAttackCooldown;
    [HideInInspector]
    int dashAttackFrenzyCost;
    #endregion

    public bool isAttacking = false;

    int selectedCharacter;

    Camera camera;

    GameObject mainCanvas;
    GameObject overlayCamera;
    GameObject managers;
    GameObject Publishers;

    [SerializeField] bool developerMode;

    public GameObject SlashParticleTrail; //for dash attack particles
    public GameObject SlashParticleTrail2;

    public static Player Instance;
    private void Awake()
    {
        player = this;
        camera = Camera.main;
        if (!developerMode)
        {
            //keeps one instance of all gameobjects
            mainCanvas = GameObject.Find("Canvas");
            overlayCamera = GameObject.Find("OverlayCam");
            managers = GameObject.Find("--MANAGERS--");
            Publishers = GameObject.Find("PUBLISHERS");
            DontDestroyOnLoad(camera);
            DontDestoryEverything(mainCanvas);
            DontDestoryEverything(overlayCamera);
            DontDestoryEverything(managers);
            DontDestoryEverything(Publishers);
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }


    }
    #region AttacksActivation
    public void DashAttackActivated()
    {
        if (playerFrenzy.CurrentFrenzy >= dashAttackFrenzyCost)
        {
            Vector3 Playeroffset = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            Vector3 mouseOffset = new Vector3(movePlayer.mousePos.x, movePlayer.mousePos.y + 2, movePlayer.mousePos.z);
            Vector3 leftOffset = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z) + transform.right;
            Vector3 rightOffset = new Vector3(transform.localPosition.x, transform.localPosition.y + 2, transform.localPosition.z) - transform.right;
            if (Physics.Raycast(Playeroffset, (mouseOffset - Playeroffset).normalized, out hit, (mouseOffset - Playeroffset).magnitude, enemyLayer))
            {
                if (!playerAttackRange.isActiveAndEnabled)
                {
                    Invoke("DashAttack", 0.01f);
                }
            }
            else if (Physics.Raycast(leftOffset, (mouseOffset - leftOffset).normalized, out hit, (mouseOffset - leftOffset).magnitude, enemyLayer))
            {
                if (!playerAttackRange.isActiveAndEnabled)
                {
                    Invoke("DashAttack", 0.01f);
                }
            }
            else if (Physics.Raycast(rightOffset, (mouseOffset - rightOffset).normalized, out hit, (mouseOffset - rightOffset).magnitude, enemyLayer))
            {
                if (!playerAttackRange.isActiveAndEnabled)
                {
                    Invoke("DashAttack", 0.01f);
                }
            }
        }


    }
#if UNITY_ANDROID
    private void Subscribe()
    {
        notePublisher.noteHitAttack += NormalAttackActivated;
    }
    public void NormalAttackActivated()
    {
        if(EnemyTransforms.Count > 0)
        {
            Transform closestEnemy = GetClosestEnemy(EnemyTransforms);
            transform.LookAt(new Vector3(closestEnemy.position.x, transform.position.y, closestEnemy.position.z));
            AttackingActivated();
        }
    }

#endif
#if UNITY_STANDALONE
    private void Subscribe()
    {
        notePublisher.noteHitAttack += NormalAttackActivated;
    }
    public void NormalAttackActivated()
    {
        //Shoots a ray and stores the information in the raycastHit variable.
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 originRay = ray.origin;
        Vector3 directonRay = ray.direction;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            AttackingActivated();
            //Checks if the player is moving and the melee range attack isn't activate.
        }
    }

#endif
    Transform GetClosestEnemy(List<Transform> enemyTransforms)
    {
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < EnemyTransforms.Count; i++)
        {
            float distance = Vector3.Distance(enemyTransforms[i].position, transform.position);
            if (distance < minDistance)
            {
                closestEnemy = enemyTransforms[i];
                minDistance = distance;
            }
        }
        return closestEnemy;
    }
    #endregion

    #region Attacks
    void AttackingActivated()
    {
        PlayerAnm.Instance.AttackTrigger();
        AudioManager.PlaySound("LowSwings", "PlayerSound", 0.1f);
    }
    public void StartAttacking()
    {
        playerAttackRange.gameObject.SetActive(true);
    }
    public void StopAttacking()
    {
        playerAttackRange.gameObject.SetActive(false);
        Enemy.TakenDamage = false;
    }
    private void DashAttack()
    {
        if (movePlayer.isMoving)
        {
            PlayerAnm.Instance.DashTrigger();
            playerDashRange.gameObject.SetActive(true);
            playerFrenzy.CurrentFrenzy -= dashAttackFrenzyCost;
            SlashParticleTrail.SetActive(true); //new
            SlashParticleTrail2.SetActive(true);
        }
        else
        {
            DisableDashAttack();
        }
    }
    public void DisableDashAttack()
    {
        playerDashRange.gameObject.SetActive(false);
    }
    #endregion

    public void RestartCharacter(Transform spawnPos)
    {
        transform.position = spawnPos.transform.position;
        GetComponent<MovePlayer>().mousePos = spawnPos.transform.position;
        GetComponent<PlayerHealth>().currentHealth = maxHealth;
        Time.timeScale = 0f;
    }
    #region References
    IEnumerator References()
    {
        yield return new WaitForSeconds(0.0001f);
        //Checks which character the player chose from the
        //main menu and adds the scriptable object to the
        //stats variable to take its stats and use them
        selectedCharacter = PlayerPrefs.GetInt("currentSelectedCharacter");

        playerChoose = GetComponent<ObjectReferences>();
        models = GetComponent<PlayerModels>();
        switch (selectedCharacter)
        {
            default:
                stats = playerChoose.stats[0];
                var playerModelDefault = stats.playerModel = models.models[selectedCharacter];
                if (!playerModelDefault.GetComponent<PlayerAnm>())
                    playerModelDefault.AddComponent<PlayerAnm>();
                break;
            case 0:
                stats = playerChoose.stats[0];
                var playerModel1 = stats.playerModel = models.models[selectedCharacter];
                if (!playerModel1.GetComponent<PlayerAnm>())
                    playerModel1.AddComponent<PlayerAnm>();
                break;
            case 1:
                stats = playerChoose.stats[1];
                var playerModel2 = stats.playerModel = models.models[selectedCharacter];
                if (!playerModel2.GetComponent<PlayerAnm>())
                    playerModel2.AddComponent<PlayerAnm>();
                break;
            case 2:
                stats = playerChoose.stats[2];
                var playerModel3 = stats.playerModel = models.models[selectedCharacter];
                if (!playerModel3.GetComponent<PlayerAnm>())
                    playerModel3.AddComponent<PlayerAnm>();
                break;
        }
        if (character == null)
        {
            character = Instantiate(stats.playerModel, transform);
            
            character.transform.localScale = new Vector3(2, 2, 2);
        }
        movePlayer = GetComponent<MovePlayer>();
        //References to all the things needed.
        playerName = stats.playerName;
        playerDamageText = stats.playerDamageText;

        //Stats
        damage = stats.attackDamage;
        dashDamage = stats.dashDamage;
        maxHealth = stats.health;

        dashAttackDuration = stats.dashAttackDuration;
        meleeAttackDuration = stats.meleeAttackDuration;

        distanceToClick = stats.distanceToClick;

        //Check if moved or not.
        notePublisher = FindObjectOfType<NotePublisher>();

        //Subscribe to noteHit.
        //notePublisher.noteHit += AttackActivated;
        movePlayer.playerRegMove += DashAttackActivated;
        Subscribe();
        playerFrenzy = GetComponent<PlayerFrenzy>();

        dashAttackFrenzyCost = stats.dashAttackFrenzyCost;

        playerAttackRange = GetComponentInChildren<PlayerAttack>();

        playerDashRange = GetComponentInChildren<PlayerDashAttack>();
        //playerAttackRange.gameObject.transform.localScale *= distanceToClick;
        yield return new WaitForSeconds(1f);
        playerAttackRange.gameObject.SetActive(false);
        playerDashRange.gameObject.SetActive(false);
        hasStarted = true;
    }
    #endregion
    #region UpgradeStats
    IEnumerator UpgradeDamageMeleeActivator(float damage)
    {
        playerAttackRange.gameObject.SetActive(true);
        playerAttackRange.damage += damage;
        yield return new WaitForSeconds(0.01f);
        playerAttackRange.gameObject.SetActive(false);
    }

    IEnumerator UpgradeDamageDashActivator(float damage)
    {
        playerDashRange.gameObject.SetActive(true);
        playerDashRange.dashDamage += damage;
        yield return new WaitForSeconds(0.01f);
        playerDashRange.gameObject.SetActive(false);
    }

    public void UpgradeDamageMelee(float damage)
    {
        StartCoroutine(UpgradeDamageMeleeActivator(damage));
    }
    public void UpgradeDamageDash(float damage)
    {
        StartCoroutine(UpgradeDamageDashActivator(damage));
    }
    #endregion

  
    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetSceneByName("ThankYou").buildIndex)
        {
            //if reached finale
            //reset everything
            DestroyEverything();
        }
        if (level == SceneManager.GetSceneByName("EmilSTestScene").buildIndex)
        {
            transform.position = new Vector3(0, 0, -42);
        }
        else if (level == SceneManager.GetSceneByName("Level_2").buildIndex)
        {
            transform.position = new Vector3(0, 0, -42);
        }
        else if (level == SceneManager.GetSceneByName("Level_3").buildIndex)
        {
            transform.position = new Vector3(0, 0, -42);
        }
        camera = Camera.main;
        //Time.timeScale = 1f;
        if (playerAttackRange != null && playerDashRange != null)
        {
            playerAttackRange.gameObject.SetActive(true);
            playerDashRange.gameObject.SetActive(true);
            playerAttackRange.gameObject.SetActive(false);
            playerDashRange.gameObject.SetActive(false);
        }
        if (!hasStarted)
        {
            StartCoroutine(References());
        }
    }
    private void DontDestoryEverything(GameObject Everything)
    {
        DontDestroyOnLoad(Everything);
    }
    public void DestroyEverything()
    {
        Time.timeScale = 1f;
        Destroy(overlayCamera);
        Destroy(mainCanvas);
        Destroy(managers);
        Destroy(Publishers);
        Destroy(gameObject);
        Destroy(camera.gameObject);
    }
    public void DeactivateAll()
    {
        overlayCamera.SetActive(false);
        mainCanvas.SetActive(false);
        managers.SetActive(false);
        Publishers.SetActive(false);
        camera.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    public void ActivateAll()
    {
        overlayCamera.SetActive(true);
        mainCanvas.SetActive(true);
        managers.SetActive(true);
        Publishers.SetActive(true);
        camera.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
}
