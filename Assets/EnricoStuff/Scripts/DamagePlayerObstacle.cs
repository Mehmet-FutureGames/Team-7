using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerObstacle : MonoBehaviour
{
    public float damagePerBeat;
    NotePublisher notePublisher;
    MovePlayer movePlayer;
    private PlayerHealth health;
    private bool playerContact;

    private void OnEnable()
    {
        notePublisher = FindObjectOfType<NotePublisher>();
        movePlayer = FindObjectOfType<MovePlayer>();
        //movePlayer.playerRegMove += EventUpdate;
        notePublisher.noteNotHit += EventUpdate;
        notePublisher.noteHitBlock += EventUpdate;
        notePublisher.noteHitAttack += EventUpdate;
        movePlayer.playerRegMove += EventUpdate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (health == null)
            {
              health = other.GetComponent<PlayerHealth>();
              health.TakeDamage(damagePerBeat);
            }
                

            else
                health.TakeDamage(damagePerBeat);
            playerContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerContact = false;
        }
    }

    private void EventUpdate()
    {
        if(playerContact == true)
        {
            health.TakeDamage(damagePerBeat);
        }

    }

    private void OnDisable()
    {
        playerContact = false;
    }

}
