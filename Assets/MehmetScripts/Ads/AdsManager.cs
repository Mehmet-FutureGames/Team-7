using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    UIManager respawnPlayer;

    string placement = "rewardedVideo";

    public static bool hasWatchedAd = false;
    public static bool startedWatchingAd = false;
    private void Start()
    {
        //Initialize ad for faster loading and set static bools to false.
        startedWatchingAd = false;
        hasWatchedAd = false;
        respawnPlayer = FindObjectOfType<UIManager>();
        Advertisement.AddListener(this);
        Advertisement.Initialize("4120115", true);
    }

    public void ShowAd()
    {
        startedWatchingAd = true;
        LevelManager.levelsCompletedThisRun++;
        StartCoroutine(ShowRewardedAd());
    }

    IEnumerator ShowRewardedAd()
    {
        while (!Advertisement.IsReady(placement))
            yield return null;
        Advertisement.Show(placement);
    }
    public void OnUnityAdsReady(string placementId)
    {

    }
    public void OnUnityAdsDidStart(string placementId)
    {
        
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //if player has finished watching the ad
        //Reward player
        if(showResult == ShowResult.Finished)
        {
            hasWatchedAd = true;
            respawnPlayer.RetryButton();
            UIManager.noRetryScreen.SetActive(false);
        }
    }
    public void OnUnityAdsDidError(string message)
    {
        
    }

}
