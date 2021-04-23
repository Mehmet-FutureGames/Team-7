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

    [Range(0, 5)]
    [SerializeField]float stopDistance;

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
                Vector3 dir = (enemyPos - transform.position).normalized;
                float length = (enemyPos - transform.position).magnitude;
                if (Physics.Raycast(transform.position, dir, out hit, length, layer))
                {
                    Vector3 hitPoint = hit.point;
                    stopPos = hitPoint + (transform.position - hit.point).normalized * stopDistance;
                }
                hasTarget = true;
            }
        }
        else { hasTarget = false; }
    }

    
}
