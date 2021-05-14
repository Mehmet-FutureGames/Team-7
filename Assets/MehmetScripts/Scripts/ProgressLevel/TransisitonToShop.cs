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
        level = SceneManager.GetActiveScene().buildIndex;
        if (level == SceneManager.GetSceneByName("EmilSTestScene").buildIndex)
        {
            LevelManager.levelsCompletedThisRun--;
        }
        GetComponent<Animator>().enabled = true;
        manager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!hasProceeded)
            {
                StartCoroutine(SceneFader.FadeOut(LoadScene));
                //LoadScene();
            }
        }
    }

    private void LoadScene()
    {
        LevelManager.levelsCompletedThisRun++;
        manager.levelsCompletedOverall++;
        PlayerPrefs.SetInt("levelCompleted", manager.levelsCompletedOverall);
        SceneManager.LoadScene("CoinShop");
        hasProceeded = true;
    }
}


