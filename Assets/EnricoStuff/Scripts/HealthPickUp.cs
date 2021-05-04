using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public AudioClip healthsound;

    public float healthRecovered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(healthsound, transform.position);
            other.gameObject.GetComponent<PlayerHealth>().RecoverHealth(healthRecovered);
            gameObject.SetActive(false);
        }
    }
}
