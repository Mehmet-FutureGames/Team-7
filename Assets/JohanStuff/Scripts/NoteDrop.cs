using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDrop : MonoBehaviour
{
    NotePublisher notePublisher;
    Transform player;
    short count;
    private void Awake()
    {
        notePublisher = FindObjectOfType<NotePublisher>();
        player = FindObjectOfType<Player>().transform;
    }

    private void OnEnable()
    {
        notePublisher.noteHit += MoveTowardsPlayer;
        notePublisher.noteHitAttack += MoveTowardsPlayer;
        notePublisher.noteHitBlock += MoveTowardsPlayer;
        notePublisher.buttonHitAttack += MoveTowardsPlayer;
    }
    private void OnDisable()
    {
        notePublisher.noteHit -= MoveTowardsPlayer;
        notePublisher.noteHitAttack -= MoveTowardsPlayer;
        notePublisher.noteHitBlock -= MoveTowardsPlayer;
        notePublisher.buttonHitAttack -= MoveTowardsPlayer;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<NoteCurrencyHandler>().AddNoteCurrency(1);
            gameObject.SetActive(false);
        }
    }

    public void MoveTowardsPlayer()
    {
        StartCoroutine(Move()); 
    }
    IEnumerator Move()
    {
        count = 0;
        while(count < 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 40 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            count++;
        }
        yield return null;
    }

}
