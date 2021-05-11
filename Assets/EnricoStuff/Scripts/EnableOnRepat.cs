using UnityEngine;

public class EnableOnRepat : MonoBehaviour
{
    public GameObject objectToEnable;
    public float enableFrequency;
    public float enableDelay;

    public void OnEnable()
    {
        InvokeRepeating("EnableObject", enableDelay, enableFrequency);
    }

    public void EnableObject()
    {
        objectToEnable.SetActive(true);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
