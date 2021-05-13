using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCUI : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_ANDROID
    gameObject.SetActive(false);
#endif
#if UNITY_STANDALONE
        gameObject.SetActive(true);
#endif
    }

}