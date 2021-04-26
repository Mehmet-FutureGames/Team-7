using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidAttackButton : MonoBehaviour
{

#if UNITY_STANDALONE
    void Start()
    {
        gameObject.SetActive(false);
    }
#endif

#if UNITY_ANDROID
    void Start()
    {
        gameObject.SetActive(true);
    }
#endif
}
