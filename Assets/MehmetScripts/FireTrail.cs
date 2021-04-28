using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrail : ActiveItems
{
    bool timerDone;
    MovePlayer movePlayer;
    float timer;
    bool hasStartedEffect;
    NotePublisher notePublisher;
    private void Start()
    {
        notePublisher = FindObjectOfType<NotePublisher>();
        UseItem();
        movePlayer = GetComponent<MovePlayer>();
        notePublisher.noteHit += SetTimer;
    }
    public override void UseItem()
    {
        Debug.Log("CoolTreail");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<UseItem>().OnPickUpItem(itemIndex, this);
            Destroy(gameObject);
        }
    }

    public override void PerformAction()
    {
        //Spawn FireTrail at player position
        hasStartedEffect = true;
        Debug.Log("FIRETRAIL");
        StartCoroutine(Timer());
    }
    
    void Update()
    {
        if(hasStartedEffect)
            SpawnFire();
    }

    void SetTimer()
    {
        timer = 0.01f;
    }

    private void SpawnFire()
    {
        timer -= Time.deltaTime;
        if ((timer <= 0 && timerDone == false) && movePlayer.isMoving)
        {
            timer = 0.1f / (movePlayer.MovementValue * 2 + 1);
            ObjectPooler.Instance.SpawnFormPool("FireTrail", transform.position, transform.rotation);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10f);
        timerDone = true;
        Destroy(this);
        yield return null;
    }

}
