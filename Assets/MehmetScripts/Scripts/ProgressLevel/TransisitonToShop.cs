using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransisitonToShop : MonoBehaviour
{
    LevelManager manager;
    private void Awake()
    {
        GetComponent<Animator>().enabled = true;
        manager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().DestroyEverything();
            manager.levelsCompletedThisRun++;
            manager.levelsCompletedOverall++;
            PlayerPrefs.SetInt("levelCompleted", manager.levelsCompletedOverall);
            SceneManager.LoadScene("Shop");
        }
    }
}


