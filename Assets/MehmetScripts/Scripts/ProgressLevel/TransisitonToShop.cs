using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TransisitonToShop : MonoBehaviour
{
    LevelManager manager;
    Scene level;
    bool hasProceeded = false;
    bool hasReachedTheEnd;
    private void Awake()
    {
        hasProceeded = false;
        level = SceneManager.GetActiveScene();
        if(level == SceneManager.GetSceneByName("Level_3"))
        {
            hasReachedTheEnd = true;
            GetComponentInChildren<TextMeshProUGUI>().text = "The End";
        }
        manager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hasReachedTheEnd)
            {
                SceneFader.fadeImage.color = new Color(0,0,0,0);
                StartCoroutine(SceneFader.FadeOut(LoadFinalScene));
            }
            else if (!hasProceeded)
            {
                SceneFader.fadeImage.color = new Color(1, 1, 1, 0);
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

    private void LoadFinalScene()
    {
        SceneManager.LoadScene("ThankYou");
    }
}


