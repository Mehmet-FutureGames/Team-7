using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    NotePublisher notePublisher;
    MovePlayer movePlayer;
    public LayerMask layer;
    public static bool hasTarget;
    private Vector3 enemyPos;
    public static Vector3 stopPos;
    void Start()
    {
        movePlayer = FindObjectOfType<MovePlayer>();
        notePublisher = FindObjectOfType<NotePublisher>();
        //movePlayer.playerRegMove += Target;
        notePublisher.noteHit += Target;
        hasTarget = false;
    }
    void Target()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layer))
        {
            Enemy enemy;
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                enemy = hit.collider.gameObject.transform.parent.GetComponent<Enemy>();
                enemy.moveDistance = 0;
                enemyPos = hit.collider.transform.position;
                Vector3 enemyToPlayerDir = (transform.position - enemyPos).normalized;
                stopPos = enemyPos + enemyToPlayerDir * 2;
                hasTarget = true;
            }
        }
        else { hasTarget = false; }
    }

    
}
