using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{
    private static MusicSingleton _instance;

    public static MusicSingleton Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
    }
}
