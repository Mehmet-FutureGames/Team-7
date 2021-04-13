using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
 
    PlayerAttack playerAttackRange;

    NotePublisher notePublisher;

    string playerName;

    public float health;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        playerName = stats.playerName;

        damage = stats.attackDamage;
        health = stats.health;

        notePublisher = FindObjectOfType<NotePublisher>();

        notePublisher.noteHit += AttackActivated;

        playerAttackRange = GetComponentInChildren<PlayerAttack>();

        StartCoroutine(References());
    }

    public void AttackActivated()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        Physics.Raycast(ray, out hit);
        if (hit.transform.CompareTag("Enemy"))
        {
            StartCoroutine(AttackingActivated());
        }
        else
        {
            Debug.Log("you missed!");
        }
    }

    IEnumerator AttackingActivated()
    {
        playerAttackRange.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log("Attacked");
        yield return new WaitForSeconds(0.5f);
        playerAttackRange.gameObject.SetActive(false);
        Debug.Log("Stop attacking");
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    IEnumerator References()
    {
        yield return new WaitForSeconds(1);
        playerAttackRange.gameObject.SetActive(false);
    }
}
