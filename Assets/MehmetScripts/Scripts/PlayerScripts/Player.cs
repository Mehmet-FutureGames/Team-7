using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
 
    PlayerAttack playerAttackRange;

    PlayerDashAttack playerDashRange;

    NotePublisher notePublisher;

    int distanceToClick;

    string playerName;

    [HideInInspector]
    public float health;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float dashDamage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(References());
    }

    public void AttackActivated()
    {
        //Shoots a ray and stores the information in the raycastHit variable.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        float distance = (transform.position - hit.transform.position).magnitude;

        if(distance < distanceToClick)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                StartCoroutine(AttackingActivated());
                Debug.Log("You hit an enemy!");
            }
        }

        //Checks if the player is moving and the melee range attack isn't activate.
        if(!playerAttackRange.isActiveAndEnabled && hit.transform.CompareTag("Enemy"))
        {
            StartCoroutine(DashAttack());
        }
    }

    IEnumerator AttackingActivated()
    {
        playerAttackRange.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material.color = Color.grey;
        Debug.Log("Attacked");
        yield return new WaitForSeconds(0.5f);
        playerAttackRange.gameObject.SetActive(false);
        Debug.Log("Stop attacking");
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    IEnumerator DashAttack()
    {
        playerDashRange.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material.color = Color.black;
        yield return new WaitForSeconds(1f);
        playerDashRange.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    IEnumerator References()
    {
        //References to all the things needed.
        playerName = stats.playerName;

        //Stats
        damage = stats.attackDamage;
        dashDamage = stats.dashDamage;
        health = stats.health;

        distanceToClick = stats.distanceToClick;

        //Check if moved or not.
        notePublisher = FindObjectOfType<NotePublisher>();

        //Subscribe to noteHit.
        notePublisher.noteHit += AttackActivated;

        playerAttackRange = GetComponentInChildren<PlayerAttack>();

        playerDashRange = GetComponentInChildren<PlayerDashAttack>();

        yield return new WaitForSeconds(1);
        playerAttackRange.gameObject.SetActive(false);
        playerDashRange.gameObject.SetActive(false);
    }
}
