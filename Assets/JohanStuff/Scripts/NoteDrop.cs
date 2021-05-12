using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDrop : MonoBehaviour
{
    NoteHandler noteHandler;
    NotePublisher notePublisher;
    Transform player;
    short count;
    int tone;
    int[] color = new int[3];
    int colorVal;
    private void Awake()
    {
        noteHandler = FindObjectOfType<NoteHandler>();
        tone = Random.Range(0, 6);
        Debug.Log(tone);
        notePublisher = FindObjectOfType<NotePublisher>();
        player = FindObjectOfType<Player>().transform;
        noteHandler.beat += SetNoteColor;
    }
    private void SetNoteColor()
    {
        for (int i = 0; i < color.Length; i++)
        {
            color[i] = Random.Range(0, 2);
            if (color[i] < colorVal)
            {
                colorVal = color[i];
            }
        }
        GetComponentInChildren<MeshRenderer>().material.color = new Color(color[0], color[1], color[2]);
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
            PlayTone();
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
            float distToPlayer = (transform.position - player.position).magnitude;
            transform.position = Vector3.MoveTowards(transform.position, player.position, 20 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            count++;
        }
        yield return null;
    }

    void PlayTone()
    {
        switch (tone)
        {
            case 0:
                AudioManager.PlaySound("Tone1", "VFXSound");
                break;
            case 1:
                AudioManager.PlaySound("Tone2", "VFXSound");
                break;
            case 2:
                AudioManager.PlaySound("Tone3", "VFXSound");
                break;
            case 3:
                AudioManager.PlaySound("Tone4", "VFXSound");
                break;
            case 4:
                AudioManager.PlaySound("Tone5", "VFXSound");
                break;
            case 5:
                AudioManager.PlaySound("Tone6", "VFXSound");
                break;
            default:
                break;
        }
    }

}
