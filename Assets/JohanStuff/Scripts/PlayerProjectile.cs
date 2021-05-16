using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 10;
    public float turnSpeed;
    int randomInt;
    float randomRotY;
    Transform target;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        randomRotY = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, randomRotY, 0);
    }
    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, randomRotY, 0);
        if (Player.EnemyTransforms.Count > 0)
        {
            randomInt = Random.Range(0, Player.EnemyTransforms.Count);
            target = Player.EnemyTransforms[randomInt];
        }
        Invoke("DisableAfterSeconds", 8f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        if (Player.EnemyTransforms.Count > 0 && target != null)
        {
            turnSpeed += 0.5f;
            var targetRotation = Quaternion.LookRotation(target.position - transform.position);

            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        }
        if(target == null)
        {
            Debug.Log("Projectile has no target");
            if (Player.EnemyTransforms.Count > 0)
            {
                randomInt = Random.Range(0, Player.EnemyTransforms.Count);
                target = Player.EnemyTransforms[randomInt];
                if (target != null)
                {
                    Debug.Log(gameObject + " Projectile Found Target: " + target);
                 }
            }

        }
    }

    void DisableAfterSeconds()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(5, false);
            gameObject.SetActive(false);
        }
    }
}
