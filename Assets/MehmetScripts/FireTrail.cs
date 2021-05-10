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
    protected override void Start()
    {
        base.Start();
        notePublisher = FindObjectOfType<NotePublisher>();
        movePlayer = GetComponent<MovePlayer>();
        notePublisher.noteHit += SetTimer;
    }


    public override void PerformAction()
    {
        //Spawn FireTrail at player position
        if (cooldownReady)
        {
            timerDone = false;
            hasStartedEffect = true;
            Debug.Log("FIRETRAIL");
            cooldownReady = false;
            StartCoroutine(Timer());
        }

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
        StartCoroutine(CountCooldown());
        yield return null;
    }
    IEnumerator CountCooldown()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            cooldownCount--;
            if (cooldownCount <= 0)
            {
                cooldownReady = true;
                cooldownCount = cooldown;
                break;
            }
        }
        yield return null;

    }

}
