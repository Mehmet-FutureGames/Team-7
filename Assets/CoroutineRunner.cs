using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    public static CoroutineRunner Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void SCouroutine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
