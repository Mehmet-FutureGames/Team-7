using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitArea : MonoBehaviour
{
    Character character;
    private void Awake()
    {
        character = FindObjectOfType<Character>();
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
