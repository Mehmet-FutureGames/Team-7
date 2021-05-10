using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    

    public float healthRecovered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.PlaySound("OptionSelect", "VFXSound");
            other.gameObject.GetComponent<PlayerHealth>().RecoverHealth(healthRecovered);
            gameObject.SetActive(false);
        }
    }
}
