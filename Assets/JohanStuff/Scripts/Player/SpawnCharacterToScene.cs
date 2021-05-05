using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacterToScene : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    void Start()
    {
        if(FindObjectOfType<Player>() == null)
        {
            Instantiate(playerPrefab);
        }
    }
    
}
