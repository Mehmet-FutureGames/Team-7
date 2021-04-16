using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameObject TextPanel;

    PressAnyKey musicStart;
    private void Start()
    {
        StartCoroutine(ShowAndStopShowingText());

        musicStart = GetComponent<PressAnyKey>();

        TextPanel = GameObject.Find("UIPanel");
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(0);
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
