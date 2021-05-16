using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransisitionToNextLevel : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPos;
    Scene currentScene;
    public float fadeTimer;
    // Start is called before the first frame update
    void Awake()
    {        
        currentScene = SceneManager.GetActiveScene();
        player = FindObjectOfType<Player>().gameObject;
        GetComponent<BoxCollider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneFader.fadeImage.color = new Color(0, 0, 0, 0);
            StartCoroutine(SceneFader.FadeOut(NextLevel));
            other.GetComponent<MovePlayer>().mousePos = other.GetComponent<Transform>().position;
        }
    }
    private void NextLevel()
    {
        SceneManager.LoadScene(currentScene.buildIndex + LevelManager.levelsCompletedThisRun);
    }
    private void OnLevelWasLoaded(int level)
    {
        if(level == SceneManager.GetSceneByName("Shop").buildIndex || level == SceneManager.GetSceneByName("CoinShop").buildIndex)
        {
            spawnPos = GameObject.FindGameObjectWithTag("SpawnPos").transform;
            player.transform.position = spawnPos.transform.position;
            player.GetComponent<MovePlayer>().mousePos = spawnPos.transform.position;
        }
    }

}
