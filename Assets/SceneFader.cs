using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneFader : MonoBehaviour
{
    private static float fadeTimer = 1f;
    public delegate void FadeDelegate();
    public static AudioSource musicSource;
    public static Image fadeImage;
    public static float FadeTimer
    {
        get { return fadeTimer; }
        set
        {
            fadeTimer = value;
        }
    }
    private void Start()
    {
        StartCoroutine(Reference());
    }
    IEnumerator Reference()
    {
        yield return new WaitForSeconds(0.1f);
        fadeImage = GameObject.FindGameObjectWithTag("FadeImage").GetComponent<Image>();
        musicSource = Camera.main.GetComponent<AudioSource>();
    }

    public static IEnumerator FadeOut(FadeDelegate method)
    {
        FadeTimer = 10f;
        float colorfloat = FadeTimer * -1 +10;
        while (FadeTimer >= 0)
        {
            colorfloat = FadeTimer * -1 + 10;
            fadeImage.color = new Color(0,0,0,colorfloat * 0.1f);
            musicSource.volume = musicSource.volume - 0.01f;
            if(musicSource.pitch > 0)
            {
                musicSource.pitch = musicSource.pitch - 0.005f;
            }
            FadeTimer = FadeTimer - 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        musicSource.pitch = 1;
        musicSource.Stop();
        method();
    }
    public static IEnumerator FadeIn(FadeDelegate method)
    {
        FadeTimer = 0f;
 
        while (FadeTimer <= 10f)
        {

            FadeTimer = FadeTimer + 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        method();
    }
    public static IEnumerator FadeIn()
    {
        musicSource.volume = 0;
        FadeTimer = 0f;
        float colorfloat = FadeTimer + 10;
        while (FadeTimer <= 10f)
        {
            colorfloat = FadeTimer * -1 + 10;
            fadeImage.color = new Color(0, 0, 0, colorfloat * 0.1f);

            musicSource.volume = Mathf.Lerp(0,NoteManager.currentSongMaxVolume, FadeTimer * 0.1f);
            FadeTimer = FadeTimer + 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetSceneByName("CoinShop").buildIndex)
        {
            StartCoroutine(FadeIn());
        }
        else if (level == SceneManager.GetSceneByName("EmilSTestScene").buildIndex)
        {
            StartCoroutine(FadeIn());
        }
        else if (level == SceneManager.GetSceneByName("Level_2").buildIndex)
        {
            StartCoroutine(FadeIn());
        }
        


    }
}
