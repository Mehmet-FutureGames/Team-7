using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchPanels : MonoBehaviour
{
    [SerializeField] List<GameObject> panels = new List<GameObject>();
    private void Start()
    {
        Time.timeScale = 1;
    }

    public void GoForwardPanel(int panel)
    {
        panels[panel + 1].SetActive(true);
        if (panel <= 0)
        {
            panels[panel].SetActive(false);
        }
        panels[panel].SetActive(false);
    }
    public void GoBackwardsPanel(int panel)
    {
        panels[panel - 1].SetActive(true);
        if (panel <= 0)
        {
            panels[panel].SetActive(false);
        }
        panels[panel].SetActive(false);
    }
    public void StartFromTutorial()
    {
        StartCoroutine(SceneFader.FadeOut(LoadShop));
    }
    private void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
