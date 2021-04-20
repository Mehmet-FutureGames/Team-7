using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameObject TextPanel;

    bool skip = false;

    PressAnyKey musicStart;
    private void Start()
    {
        StartCoroutine(ShowAndStopShowingText());

        musicStart = GetComponent<PressAnyKey>();

        TextPanel = GameObject.Find("UIPanel");

        skip = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkipText();
        }
    }
    public void RetryButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void SkipText()
    {
        if (skip)
        {
            StopAllCoroutines();
            Time.timeScale = 0;
            TextPanel.SetActive(false);
            musicStart.audio.Play();
            musicStart.StartGame();
            Time.timeScale = 1;
        }
    }
    IEnumerator ShowAndStopShowingText()
    {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(5f);
            TextPanel.SetActive(false);
            musicStart.audio.Play();
            musicStart.StartGame();
            Time.timeScale = 1;        
    }

}
