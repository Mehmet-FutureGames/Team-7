using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
 
    PlayerAttack playerAttackRange;

    string playerName;

    public float health;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        playerName = stats.playerName;

        damage = stats.attackDamage;
        health = stats.health;

        playerAttackRange = GetComponentInChildren<PlayerAttack>();

        playerAttackRange.gameObject.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(AttackingActivated());
        }
    }

    IEnumerator AttackingActivated()
    {
        playerAttackRange.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        playerAttackRange.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().material.color = Color.green;

    }
}
