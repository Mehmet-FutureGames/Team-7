using UnityEngine;

public class FrenzyPickUp : MonoBehaviour
{
    public float frenzyGain;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.PlaySound("OptionSelect", "VFXSound");
            other.gameObject.GetComponent<PlayerFrenzy>().AddFrenzyPickUp(frenzyGain);
            gameObject.SetActive(false);
        }
    }
}
