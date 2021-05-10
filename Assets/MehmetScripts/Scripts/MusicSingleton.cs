using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{
    private static MusicSingleton _instance;

    public static MusicSingleton Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
        

    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
    private void OnLevelWasLoaded(int level)
    {
        if(level == 1 || level == 0)
        {
            
        }
        else
        {
            
        }
        
    }
    
}
