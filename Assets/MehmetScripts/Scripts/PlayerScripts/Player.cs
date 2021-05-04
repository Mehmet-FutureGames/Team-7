using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static List<Transform> EnemyTransforms = new List<Transform>();

    public AudioClip attacksound;

    public PlayerStats stats;

    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask enemyLayer;

    ObjectReferences playerChoose;

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

    public GameObject SlashParticleTrail; //for dash attack particles
    public GameObject SlashParticleTrail2;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        StartCoroutine(References());
        GetComponent<TrailRenderer>().time = 60 / FindObjectOfType<NoteManager>().beatTempo;
    }
    #region AttacksActivation
    public void DashAttackActivated()
    {
        if (Physics.Raycast(transform.position, (movePlayer.mousePos - transform.position).normalized, out hit, (movePlayer.mousePos - transform.position).magnitude, enemyLayer))
        {
            if (!playerAttackRange.isActiveAndEnabled && playerFrenzy.CurrentFrenzy >= dashAttackFrenzyCost)
            {
                Invoke("DashAttack", 0.01f);
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
            if(distance < minDistance)
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
        AudioSource.PlayClipAtPoint(attacksound, transform.position);
    }
    public void StartAttacking()
    {
        playerAttackRange.gameObject.SetActive(true);
    }
    public void StopAttacking()
    {
        playerAttackRange.gameObject.SetActive(false);
    }
    private void  DashAttack()
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
#region References
    IEnumerator References()
    {
        //Checks which character the player chose from the
        //main menu and adds the scriptable object to the
        //stats variable to take its stats and use them
        selectedCharacter = PlayerPrefs.GetInt("currentSelectedCharacter");
        playerChoose = GetComponent<ObjectReferences>();
        switch (selectedCharacter)
        {
            default:
                stats = playerChoose.stats[0];
                break;
            case 0:
                stats = playerChoose.stats[0];
                break;
            case 1:
                stats = playerChoose.stats[1];
                break;
            case 2:
                stats = playerChoose.stats[2];
                break;
        }
        //Instantiate(stats.playerModel, transform);
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
        yield return new WaitForSeconds(1);
        playerAttackRange.gameObject.SetActive(false);
        playerDashRange.gameObject.SetActive(false);
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
}
