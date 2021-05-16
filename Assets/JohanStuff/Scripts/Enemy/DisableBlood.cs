using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBlood : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DisableGameObject", 2f);
    }

    void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
