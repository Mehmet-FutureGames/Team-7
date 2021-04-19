using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerStats stats;

    [SerializeField] LayerMask enemyLayer;
 
    PlayerAttack playerAttackRange;

    PlayerDashAttack playerDashRange;

    NotePublisher notePublisher;

    int distanceToClick;

    string playerName;

    [HideInInspector]
    public GameObject playerDamageText;
    [HideInInspector]
    public float health;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float dashDamage;
    [HideInInspector]
    public float dashAttackDuration;
    [HideInInspector]
    public float meleeAttackDuration;

    public bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(References());        
    }

    public void AttackActivated()
    {
        //Shoots a ray and stores the information in the raycastHit variable.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 test = ray.origin;
        Vector3 test2 = ray.direction;
        RaycastHit hit;
        if(Physics.Raycast(test, test2, out hit, Mathf.Infinity, enemyLayer))
        {            
                float distance = (transform.position - hit.transform.position).magnitude;
                if (distance < distanceToClick)
                {
                    Debug.Log(hit.collider.tag);
                    {
                        var enemyPos = hit.transform.position;
                        transform.LookAt(new Vector3(enemyPos.x, 1, enemyPos.z));
                        StartCoroutine(AttackingActivated());
                    }
                }            
        //Checks if the player is moving and the melee range attack isn't activate.
        }
        else if (!playerAttackRange.isActiveAndEnabled)
        {
            StartCoroutine(DashAttack());
        }

    }

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
        yield return new WaitForSeconds(dashAttackDuration);
        playerDashRange.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    IEnumerator References()
    {

        //References to all the things needed.
        playerName = stats.playerName;
        playerDamageText = stats.playerDamageText;

        //Stats
        damage = stats.attackDamage;
        dashDamage = stats.dashDamage;
        health = stats.health;

        dashAttackDuration = stats.dashAttackDuration;
        meleeAttackDuration = stats.meleeAttackDuration;

        distanceToClick = stats.distanceToClick;

        //Check if moved or not.
        notePublisher = FindObjectOfType<NotePublisher>();

        //Subscribe to noteHit.
        notePublisher.noteHit += AttackActivated;

        playerAttackRange = GetComponentInChildren<PlayerAttack>();

        playerDashRange = GetComponentInChildren<PlayerDashAttack>();
        playerAttackRange.gameObject.transform.localScale *= distanceToClick;
        yield return new WaitForSeconds(1);
        playerAttackRange.gameObject.SetActive(false);
        playerDashRange.gameObject.SetActive(false);
    }
}
