using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntAds : MonoBehaviour
{
    //string appKey = "171598225";

    // Start is called before the first frame update
    void Awake()
    {
        //IronSource.Agent.init(appKey);
    }

    #region Interstitial Ad Methods

    private void InitializeInterstitialAds()
    {
        //MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        //MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
        //MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
        //MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;

    }

    //private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    interstitialRetryAttempt = 0;
    //}

    //private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    //{
    //    // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
    //    interstitialRetryAttempt++;
    //    double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));
    //    Debug.Log("Interstitial failed to load with error code: " + errorInfo.Code);

    //    Invoke("LoadInterstitial", (float)retryDelay);
    //}

    //private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    Time.timeScale = 1;
    //    Debug.Log("Interstitial dismissed");
    //}

    //private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { 
    
    //}


    #endregion

    #region Rewarded Ad Methods

    private void InitializeRewardedAds()
    {
        // Attach callbacks
        //MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        //MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
        //MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        //MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

    }



    //private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    rewardedRetryAttempt = 0;
    //}

    //private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    //{
    //    // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
    //    rewardedRetryAttempt++;
    //    double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));
    //    Debug.Log("Rewarded ad failed to load with error code: " + errorInfo.Code);

    //    Invoke("LoadRewardedAd", (float)retryDelay);
    //}


    //private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    Debug.Log("Rewarded ad clicked");
    //}



    //private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    //{
    //    // Rewarded ad was displayed and user should receive the reward
    //    Debug.Log("Rewarded ad received reward");
    //    AdsManager.intance.checkRewardComplete = true;
    //    Debug.Log("Show_Reward_Complete");
    //}


    #endregion

    #region Banner Ad Methods

    //private void InitializeBannerAds()
    //{
    //    // Attach Callbacks
    //    MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;

    //    MaxSdk.CreateBanner(BannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
    //    MaxSdk.SetBannerExtraParameter(BannerAdUnitId, "adaptive_banner", "true");
    //    MaxSdk.SetBannerBackgroundColor(BannerAdUnitId, Color.white);

    //}



    //private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    Debug.Log("Banner ad clicked");
    //}


    #endregion


}
