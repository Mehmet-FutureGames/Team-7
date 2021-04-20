using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    NotePublisher notePublisher;
    MovePlayer movePlayer;
    void Start()
    {
        movePlayer = FindObjectOfType<MovePlayer>();
        notePublisher = FindObjectOfType<NotePublisher>();
        movePlayer.playerRegMove += Target;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Target()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Enemy enemy;
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            enemy = hit.collider.gameObject.transform.parent.GetComponent<Enemy>();
            enemy.moveDistance = 0;
        }
        


    }
}
