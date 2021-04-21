using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] PlayerStats stats;

    [SerializeField] LayerMask enemyLayer;

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
        if (!playerAttackRange.isActiveAndEnabled && playerFrenzy.CurrentFrenzy >= dashAttackFrenzyCost && doesntReachTarget)
        {
            StartCoroutine(DashAttack());                            
        }
    }
    #endregion

    #region Attacks
    IEnumerator AttackingActivated()
    {
        playerAttackRange.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material.color = Color.grey;
        yield return new WaitForSeconds(meleeAttackDuration);
        playerAttackRange.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    IEnumerator DashAttack()
    {
        playerDashRange.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material.color = Color.black;
        playerFrenzy.CurrentFrenzy -= dashAttackFrenzyCost;
        yield return new WaitForSeconds(dashAttackDuration);
        playerDashRange.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    #endregion
    #region References
    IEnumerator References()
    {
        //Instantiate(stats.playerModel, transform);

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
        notePublisher.noteHit += AttackActivated;

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
