using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    UIManager respawnPlayer;

    string placement = "rewardedVideo";

    public static bool hasWatchedAd = false;
    private void Start()
    {
        hasWatchedAd = false;
        respawnPlayer = FindObjectOfType<UIManager>();
    }

    public void ShowAd()
    {
        StartCoroutine(ShowRewardedAd());
    }

    IEnumerator ShowRewardedAd()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("4120115",true);

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
        if(showResult == ShowResult.Finished)
        {
            hasWatchedAd = true;
            respawnPlayer.RetryButton();
            UIManager.deathScreen.SetActive(false);
        }
    }
    public void OnUnityAdsDidError(string message)
    {
        
    }

}
