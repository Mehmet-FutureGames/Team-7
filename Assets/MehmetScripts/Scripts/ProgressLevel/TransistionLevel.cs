using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransistionLevel : MonoBehaviour
{
    LevelManager manager;
    private void Awake()
    {
        manager = FindObjectOfType<LevelManager>();
        GetComponent<Animator>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            manager.levelsCompleted++;
            PlayerPrefs.SetInt("FloorCompleted", manager.levelsCompleted);
            SceneManager.LoadScene("CharacterChange");
        }
    }
}

