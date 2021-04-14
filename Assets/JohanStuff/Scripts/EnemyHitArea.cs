using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitArea : MonoBehaviour
{
    Enemy character;
    private void Awake()
    {
        character = FindObjectOfType<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.playerIsInAttackArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.playerIsInAttackArea = false;
        }
    }
    private void OnDisable()
    {
        character.playerIsInAttackArea = false;
    }
}
