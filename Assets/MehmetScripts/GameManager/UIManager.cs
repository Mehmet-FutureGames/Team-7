using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    Scene currentScene;

    public static float timer;

    public static TextMeshProUGUI timerDead;
    Transform spawnPos;

    public static GameObject deadSlider;

    public static GameObject deathScreen;

    public static GameObject gameOverPanel;

    GameObject uiPanel;

    [SerializeField] TextMeshProUGUI waveText;

    NotePublisher notePublisher;

    WaveManager manager;

    bool skip = false;

    Player player;

    PressAnyKey musicStart;

    private void Awake()
    {
        timer = Time.realtimeSinceStartup;

        timerDead = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        deadSlider = GameObject.Find("TimerSlider");
        Debug.Log(deadSlider);
        deathScreen = GameObject.Find("DeathScreenPanel");
        gameOverPanel = GameObject.Find("GameOverPanel");
        uiPanel = GameObject.Find("UIPanel");
    }
    private void Start()
    {
        StartCoroutine(ShowAndStopShowingText());

        musicStart = GetComponent<PressAnyKey>();

        manager = FindObjectOfType<WaveManager>();

        notePublisher = FindObjectOfType<NotePublisher>();

        notePublisher.noteHit += UpdateWaveLevel;
        notePublisher.noteNotHit += UpdateWaveLevel;

        player = FindObjectOfType<Player>();

        deathScreen.SetActive(false);
        gameOverPanel.SetActive(false);
        UpdateWaveLevel();
    }
    private void Update()
    {
        timer = Time.realtimeSinceStartup;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkipText();
        }
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(currentScene.buildIndex);
        player.RestartCharacter(spawnPos);
        player.GetComponent<PlayerHealth>().Respawn();
    }

    private void UpdateWaveLevel()
    {
        waveText.text = "Wave: " + manager.waveLevel + "/" + manager.waveMaximum;
    }

    private void SkipText()
    {
        if (!skip)
        {
            skip = true;
            StopAllCoroutines();
            Time.timeScale = 0;
            uiPanel.SetActive(false);
            musicStart.audio.Play();
            musicStart.StartGame();
            Time.timeScale = 1;
        }
    }
    IEnumerator ShowAndStopShowingText()
    {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(5f);
            uiPanel.SetActive(false);
            musicStart.audio.Play();
            musicStart.StartGame();
            Time.timeScale = 1;        
    }
    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene();
        if(level == SceneManager.GetSceneByName("Shop").buildIndex || SceneManager.GetSceneByName("CoinShop").buildIndex == 4)
        {
            Debug.Log("You are in the shop!");
        }
        else
        {
            spawnPos = GameObject.FindGameObjectWithTag("SpawnPos").transform;
        }
    }

}
