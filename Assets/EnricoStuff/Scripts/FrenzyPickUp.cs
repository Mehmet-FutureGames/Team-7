using UnityEngine;

public class FrenzyPickUp : MonoBehaviour
{
    public float frenzyGain;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerFrenzy>().AddFrenzyPickUp(frenzyGain);
            gameObject.SetActive(false);
        }
    }
}
