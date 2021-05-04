using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransisitionToNextLevel : MonoBehaviour
{
    LevelManager manager;
    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene(manager.currentLevel + manager.levelsCompletedThisRun);
    }
}
