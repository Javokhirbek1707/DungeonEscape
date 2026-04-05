using System.Collections;
using System.Collections.Generic;
using Unity.Services.LevelPlay;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    private LevelPlayRewardedAd rewardedAd;

    void Start()
    {
        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed += OnInitFailed;
        LevelPlay.Init("YOUR_APP_KEY");
    }

    void OnInitSuccess(LevelPlayConfiguration config)
    {
        Debug.Log("LevelPlay Initialized!");

        rewardedAd = new LevelPlayRewardedAd("rewarded");
        rewardedAd.OnAdLoaded += OnAdLoaded;
        rewardedAd.OnAdLoadFailed += OnAdLoadFailed;
        rewardedAd.OnAdDisplayed += OnAdDisplayed;
        rewardedAd.OnAdClosed += OnAdClosed;
        rewardedAd.OnAdRewarded += OnAdRewarded;

        rewardedAd.LoadAd();
    }

    void OnInitFailed(LevelPlayInitError error)
    {
        Debug.Log("Init Failed: " + error);
    }

    void OnAdLoaded(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Ad Loaded");
    }

    void OnAdLoadFailed(LevelPlayAdError error)
    {
        Debug.Log("Ad Load Failed: " + error);
    }

    void OnAdDisplayed(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Ad Displayed");
    }

    void OnAdClosed(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Ad Closed");
        rewardedAd.LoadAd();
    }
    void OnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        Debug.Log("Rewarded! Type: " + reward.Name + " Amount: " + reward.Amount);
        GameManager.Instance.Player.AddGems(100);
        UIManager.Instance.ShopOpen(GameManager.Instance.Player.diamonds);
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.IsAdReady())
        {
            rewardedAd.ShowAd("rewarded");
        }
        else
        {
            Debug.Log("Ad not ready");
        }
    }

    private void OnDestroy()
    {
        if (rewardedAd != null)
        {
            rewardedAd.OnAdLoaded -= OnAdLoaded;
            rewardedAd.OnAdLoadFailed -= OnAdLoadFailed;
            rewardedAd.OnAdDisplayed -= OnAdDisplayed;
            rewardedAd.OnAdClosed -= OnAdClosed;
            rewardedAd.OnAdRewarded -= OnAdRewarded;
        }
    }
}
