using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransisitonToShop : MonoBehaviour
{
    LevelManager manager;
    int level;
    bool hasProceeded = false;
    private void Awake()
    {
        hasProceeded = false;
        level = SceneManager.GetActiveScene().buildIndex;
        manager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!hasProceeded)
            {
                other.GetComponent<MovePlayer>().mousePos = other.GetComponent<Transform>().position;
                StartCoroutine(SceneFader.FadeOut(LoadScene));
                LevelManager.levelsCompletedThisRun++;
                //LoadScene();
            }
        }
    }

    private void LoadScene()
    {
        hasProceeded = true;
        PlayerPrefs.SetInt("levelCompleted", manager.levelsCompletedOverall);
        SceneManager.LoadScene("CoinShop");
    }
}


