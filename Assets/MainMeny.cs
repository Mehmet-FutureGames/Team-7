using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMeny : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("hello world");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }



    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
