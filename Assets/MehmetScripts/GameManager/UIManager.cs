using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject TextPanel;
    private void Start()
    {
        StartCoroutine(ShowAndStopShowingText());
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
        Time.timeScale = 1;
    }

}
