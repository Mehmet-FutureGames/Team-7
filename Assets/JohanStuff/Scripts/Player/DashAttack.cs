using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    MovePlayer movePlayerScript;
    bool isActive;
    CapsuleCollider capsule;

    void Start()
    {
        capsule = gameObject.GetComponent<CapsuleCollider>();
        capsule.enabled = false;
        movePlayerScript = GetComponentInParent<MovePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movePlayerScript.isMoving && !isActive)
        {
            capsule.enabled = true;
            isActive = true;
        }
        else if(!movePlayerScript.isMoving && isActive)
        {
            capsule.enabled = false;
            isActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<Enemy>().TakeDamage(10);
            
        }
    }
}
