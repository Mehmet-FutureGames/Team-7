using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public PlayerStats stats;

    [SerializeField] LayerMask enemyLayer;

    MovePlayer movePlayer;

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

    [SerializeField] bool doesntReachTarget = false;
    #endregion

    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        /*if(PlayerPrefs.GetInt("selectedCharacter") == 0)
        {
            stats = Resources.Load("PlayerObjects/NewCoolGuy") as PlayerStats;
        }
        else if(PlayerPrefs.GetInt("selectedCharacter") == 1)
        {
            stats = Resources.Load("PlayerObjects/UncoolStats42") as PlayerStats;
        }
        else if(PlayerPrefs.GetInt("selectedCharacter") == 2)
        {
            stats = Resources.Load("PlayerObjects/BigTankyBoi") as PlayerStats;
        }*/
        StartCoroutine(References());
    }
    #region Attacks
    public void AttackActivated()
    {
        //Shoots a ray and stores the information in the raycastHit variable.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 originRay = ray.origin;
        Vector3 directonRay = ray.direction;
        RaycastHit hit;

        
        if(Physics.Raycast(originRay, directonRay, out hit, Mathf.Infinity, enemyLayer))
        {       
            float distance = (transform.position - hit.transform.position).magnitude;
            if (distance < distanceToClick)
            {
                {
                    var enemyPos = hit.collider.gameObject.transform.position;
                    transform.LookAt(new Vector3(enemyPos.x, 1, enemyPos.z));
                    doesntReachTarget = false;
                    StartCoroutine(AttackingActivated());
                }
            }
            else
            {
            doesntReachTarget = true;
            }
        //Checks if the player is moving and the melee range attack isn't activate.
        }
        if (Physics.Raycast(transform.position, (movePlayer.mousePos- transform.position).normalized, out hit, (movePlayer.mousePos - transform.position).magnitude, enemyLayer))
        {
            if (!playerAttackRange.isActiveAndEnabled && playerFrenzy.CurrentFrenzy >= dashAttackFrenzyCost)
            {
                Invoke("DashAttack", 0.01f);
            }
        }

    }
    #endregion

    #region Attacks
    IEnumerator AttackingActivated()
    {
        PlayerAnm.Instance.AttackTrigger();
        playerAttackRange.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material.color = Color.grey;
        yield return new WaitForSeconds(meleeAttackDuration);
        playerAttackRange.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    private void  DashAttack()
    {
        if (movePlayer.isMoving)
        {
            PlayerAnm.Instance.DashTrigger();
            playerDashRange.gameObject.SetActive(true);
            GetComponent<MeshRenderer>().material.color = Color.black;
            playerFrenzy.CurrentFrenzy -= dashAttackFrenzyCost;
        }
        else
        {
            playerDashRange.gameObject.SetActive(false);
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
    #endregion
    #region References
    IEnumerator References()
    {
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
        movePlayer.playerRegMove += AttackActivated;
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
