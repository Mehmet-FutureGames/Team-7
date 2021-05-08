using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    public Transform player;
    public AudioClip coinsound;

    Vector3 velocity = Vector3.zero;
    [SerializeField, Range(0, 5)] float disableAfterSeconds;
    [SerializeField] float upwardForce;
    public float speedModifier;
    
    private float randomZVal;
    private float randomXVal;

    float coinValue;

    bool follow;
    Rigidbody rb;
    private void Awake()
    {
        randomXVal = Random.Range(-100f, 100f);
        randomZVal = Random.Range(-200f, 200f);


        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>().transform;
        }
        GetComponent<SphereCollider>().enabled = false;
        follow = false;
        rb.isKinematic = false;
        rb.AddForce(new Vector3(randomXVal, 1 * upwardForce, randomZVal));
        Invoke("SetFollow", disableAfterSeconds);
    }

    void FixedUpdate()
    {
        if(player != null)
        {
            float distance = (player.position - transform.position).magnitude;
            if (distance < 30 && follow == true)
            {
                transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, speedModifier * Time.fixedDeltaTime);
            }
        }
    }
    void SetFollow()
    {
        GetComponent<SphereCollider>().enabled = true;
        rb.isKinematic = true;
        follow = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!AudioManager.AudioSourcePlaying("VFXSound"))
            {
                AudioManager.PlaySound("SpaceCoinDropped", "VFXSound");
            }
            else if(AudioManager.AudioSourcePlaying("VFXSound"))
            {
                AudioManager.PlaySound("coinsDrop", "VFXSound");
            }
            other.gameObject.GetComponent<PlayerCoinHandler>().AddCoins(coinValue);
            gameObject.SetActive(false);
        }
    }

    public void SetCoinValue(float value)
    {
        coinValue = value;
    }

}
