using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameObject TextPanel;

    [SerializeField] TextMeshProUGUI waveText;

    NotePublisher notePublisher;

    WaveManager manager;

    bool skip = false;

    PressAnyKey musicStart;
    private void Start()
    {
        StartCoroutine(ShowAndStopShowingText());

        musicStart = GetComponent<PressAnyKey>();

        manager = FindObjectOfType<WaveManager>();

        notePublisher = FindObjectOfType<NotePublisher>();

        notePublisher.noteHit += UpdateWaveLevel;
        notePublisher.noteNotHit += UpdateWaveLevel;

        TextPanel = GameObject.Find("UIPanel");
        UpdateWaveLevel();
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

    private void UpdateWaveLevel()
    {
        waveText.text = "Wave: " + manager.waveLevel + "/" + manager.waveMaximum;
    }

    private void SkipText()
    {
        if (!skip)
        {
            StopAllCoroutines();
            Time.timeScale = 0;
            TextPanel.SetActive(false);
            musicStart.audio.Play();
            musicStart.StartGame();
            Time.timeScale = 1;
            skip = true;
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
