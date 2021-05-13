using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static bool hasRestartedPC;
    Scene currentScene;

    public static float timer;

    public static TextMeshProUGUI timerDead;
    public static TextMeshProUGUI sliderTimerTextPC;
    public static TextMeshProUGUI notesNeededToRevive;
    Transform spawnPos;

    public static GameObject deadSlider;

    public static GameObject deadPanel;

    public static GameObject noRetryScreen;

    public static GameObject gameOverPanel;

    public static GameObject deadPanelPC;
    public static GameObject timerOverSliderPC;

    GameObject uiPanel;

    [SerializeField] TextMeshProUGUI waveText;

    NotePublisher notePublisher;

    bool skip = false;

    [SerializeField] int notesNeeded;

    Player player;

    private void Start()
    {
        hasRestartedPC = false;
        //Keeps time incase we need a timer
        timer = Time.realtimeSinceStartup;

        //PC REFERENCES
        notesNeededToRevive = GameObject.Find("NoteCost").GetComponent<TextMeshProUGUI>();
        notesNeededToRevive.text = notesNeeded.ToString();
        deadPanelPC = GameObject.Find("DeathScreenPanelPC");
        sliderTimerTextPC = GameObject.Find("TimerTextPC").GetComponent<TextMeshProUGUI>(); 
        timerOverSliderPC = GameObject.Find("TimerSliderPC");
        deadPanelPC.SetActive(false);

        //ANDROID REFERENCES
        timerDead = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        deadSlider = GameObject.Find("TimerSlider");
        noRetryScreen = GameObject.Find("GameOverPanel");
        gameOverPanel = GameObject.Find("DeathScreenPanel");
        gameOverPanel.SetActive(false);
        noRetryScreen.SetActive(false);

        uiPanel = GameObject.Find("UIPanel");

        StartCoroutine(ShowAndStopShowingText());

        notePublisher = FindObjectOfType<NotePublisher>();

        notePublisher.noteHit += UpdateWaveLevel;
        notePublisher.noteNotHit += UpdateWaveLevel;

        player = FindObjectOfType<Player>();

        UpdateWaveLevel();
        SkipText();
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
            if (player == null || spawnPos == null)
            {
                player = FindObjectOfType<Player>();
                spawnPos = GameObject.FindGameObjectWithTag("SpawnPos").transform;
            }
            currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
            player.RestartCharacter(spawnPos);
            player.GetComponent<PlayerHealth>().Respawn();        
    }
    public void RetryButtonPC()
    {
        if (NoteCurrencyHandler.Instance.NoteCurrency >= notesNeeded)
        {
            hasRestartedPC = true;
            if (player == null || spawnPos == null)
            {
                player = FindObjectOfType<Player>();
                spawnPos = GameObject.FindGameObjectWithTag("SpawnPos").transform;
            }
            deadPanelPC.SetActive(false);
            currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
            player.RestartCharacter(spawnPos);
            player.GetComponent<PlayerHealth>().Respawn();
            NoteCurrencyHandler.Instance.NoteCurrency -= notesNeeded;
            PlayerPrefs.SetInt("NoteCurrency", NoteCurrencyHandler.Instance.NoteCurrency);
        }
    }
    IEnumerator TimeStart()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }
    private void UpdateWaveLevel()
    {
        //waveText.text = "Wave: " + manager.waveLevel + "/" + manager.waveMaximum;
    }

    private void SkipText()
    {
        if (!skip)
        {
            skip = true;
            StopAllCoroutines();
            //Time.timeScale = 0;
            uiPanel.SetActive(false);
            //musicStart.audio.Play();
            //musicStart.StartGame();
            //Time.timeScale = 1;
        }
    }
    IEnumerator ShowAndStopShowingText()
    {
            //Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(5f);
            uiPanel.SetActive(false);
            //musicStart.audio.Play();
            //musicStart.StartGame();
            //Time.timeScale = 1;        
    }
    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene();
        if(level == SceneManager.GetSceneByName("Shop").buildIndex || level == SceneManager.GetSceneByName("CoinShop").buildIndex || level == SceneManager.GetSceneByName("ThankYou").buildIndex)
        {

        }
        else
        {
            spawnPos = GameObject.FindGameObjectWithTag("SpawnPos").transform;
        }
    }

}
