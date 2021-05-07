using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransisitionToNextLevel : MonoBehaviour
{
    LevelManager manager;
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPos;
    Scene currentScene;
    // Start is called before the first frame update
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        player = FindObjectOfType<Player>().gameObject;
        manager = FindObjectOfType<LevelManager>();
        GetComponent<BoxCollider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NextLevel();
        }
    }
    private void NextLevel()
    {
        SceneManager.LoadScene(currentScene.buildIndex + 1);
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
