using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitArea : MonoBehaviour
{
    Enemy character;
    private void Awake()
    {
        character = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.playerIsInAttackArea = true;
            Debug.Log("Player entered" + character.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.playerIsInAttackArea = false;
            Debug.Log("Player exit" + character.name);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(wait());
    }
    private void OnDisable()
    {
        character.playerIsInAttackArea = false;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.0001f);
        transform.position = character.player.position;
        
    }
}
