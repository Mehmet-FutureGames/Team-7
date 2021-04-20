using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitArea : MonoBehaviour
{
    Enemy enemy;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.playerIsInAttackArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.playerIsInAttackArea = false;
        }
    }
    private void OnEnable()
    {
        StartCoroutine(wait());
    }
    private void OnDisable()
    {
        enemy.playerIsInAttackArea = false;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.0001f);
        transform.position = enemy.player.position;
        
    }
}
