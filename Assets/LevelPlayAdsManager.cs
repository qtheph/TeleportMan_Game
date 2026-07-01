using UnityEngine;
using Unity.Services.LevelPlay;
using System;
using TMPro;
public class LevelPlayAdsManager : MonoBehaviour
{
    [Header("App Key")]
    [SerializeField] private string androidAppKey;
    [SerializeField] private string iosAppKey;

    [Header("Banner Ad Unity Id")]
    [SerializeField] private string androidBannerAdUnitId;
    [SerializeField] private string iosBannerAdUnitId;
    [SerializeField] private string androidInterstitialAdUnitId;
    [SerializeField] private string iosInterstitialAdUnitId;
    [SerializeField] private string androidRewardedAdUnitId;
    [SerializeField] private string iosRewardedAdUnitId;
    private LevelPlayBannerAd bannerAd;
    private LevelPlayInterstitialAd interstitialAd;
    private LevelPlayRewardedAd rewardedAd;
    [SerializeField] TextMeshProUGUI moneyText;
    private string GetAppKey
    {
        get
        {
#if UNITY_ANDROID
            return androidAppKey;
#elif UNITY_IOS
            return iosAppKey;
#else
            return empty;
#endif
        }
    }
    private string GetBannerUnitId
    {
        get
        {
#if UNITY_ANDROID
            return androidBannerAdUnitId;
#elif UNITY_IOS
            return iosBannerAdUnitId;
#else
            return empty;
#endif
        }
    }
    private string GetInterstitialUnitId
    {
        get
        {
#if UNITY_ANDROID
            return androidInterstitialAdUnitId;
#elif UNITY_IOS
            return iosInterstitialAdUnitId;
#else
            return empty;
#endif
        }
    }
    private string GetRewardedUnitId
    {
        get
        {
#if UNITY_ANDROID
            return androidRewardedAdUnitId;
#elif UNITY_IOS
            return iosRewardedAdUnitId;
#else
            return empty;
#endif
        }
    }

    public int MoneyRewarded
    {
        get => PlayerPrefs.GetInt("Money");
        set
        {
            PlayerPrefs.SetInt("Money", value);
            PlayerPrefs.Save();
        }
    }
    private void UpdateMoneyText()
    {
        moneyText.text = MoneyRewarded.ToString();
    }
    public void Start()
    {
        //LevelPlay.ValidateIntegration();
        // Register OnInitFailed and OnInitSuccess listeners
        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
        // SDK init
        LevelPlay.Init(GetAppKey);
        UpdateMoneyText();
    }
    private void SdkInitializationCompletedEvent(LevelPlayConfiguration configuration)
    {
        CreateBanner();
        CreateInterstitial();
        CreateRewarded();
        Debug.Log("Level Play Ads Init Completed" + configuration);
    }
    private void SdkInitializationFailedEvent(LevelPlayInitError error)
    {
        Debug.Log("Level Play Ads Init Has Error" + error);
    }
    #region banner
    private void CreateBanner()
    {
        // Config banner ads
        var bannerConfig = new LevelPlayBannerAd.Config.Builder()
        .SetPosition(LevelPlayBannerPosition.BottomCenter)
        .Build();

        // Sign banner config and show banner ads following id
        bannerAd = new LevelPlayBannerAd(GetBannerUnitId, bannerConfig);

        // Register to the events 
        bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
        bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
        bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
        bannerAd.OnAdDisplayFailed += BannerOnAdDisplayFailedEvent;
        bannerAd.OnAdClicked += BannerOnAdClickedEvent;
        bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
        bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
        bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;
    }
    // Show Banner when using
    private void ShowBanner()
    {
        bannerAd.ShowAd();
    }
    // Destroy Banner when not using
    private void DestroyBanner()
    {
        bannerAd.DestroyAd();
    }
    // Implement the events
    void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Banner Ads Has Clicked");
    }
    void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error) { }
    void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo) { }
    #endregion

    #region Interstitial
    private void CreateInterstitial()
    {
        // Config interstitial ads
        interstitialAd = new LevelPlayInterstitialAd(GetInterstitialUnitId);

        // Sign banner config and show banner ads following id
        // Register to interstitial events
        interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
        interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
        interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
        interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
        interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
        interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;

        //LoadInterstitial();
    }
    // Show Banner when using
    private void LoadInterstitial()
    {
        interstitialAd.LoadAd();
        Debug.Log("InterstitialAd Has Loaded");
    }
    private void ShowInterstitial()
    {
        if (interstitialAd.IsAdReady())
        {
            interstitialAd.ShowAd();
            Debug.Log("InterstitialAd Has Showed");
        }
    }
    // Destroy Banner when not using

    // Implement the events
    void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        //Call when load failed
        //LoadInterstitial();
    }
    void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error) { }
    void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        //Call when load failed
        //LoadInterstitial();
    }
    void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo) { }
    #endregion

    #region Rewarded
    private void CreateRewarded()
    {
        // Config rewarded ads
        rewardedAd = new LevelPlayRewardedAd(GetRewardedUnitId);

        // Register to Rewarded events
        rewardedAd.OnAdLoaded += RewardedOnAdLoadedEvent;
        rewardedAd.OnAdLoadFailed += RewardedOnAdLoadFailedEvent;
        rewardedAd.OnAdDisplayed += RewardedOnAdDisplayedEvent;
        rewardedAd.OnAdDisplayFailed += RewardedOnAdDisplayFailedEvent;
        rewardedAd.OnAdRewarded += RewardedOnAdRewardedEvent;
        rewardedAd.OnAdClosed += RewardedOnAdClosedEvent;
        // Optional
        rewardedAd.OnAdClicked += RewardedOnAdClickedEvent;
        rewardedAd.OnAdInfoChanged += RewardedOnAdInfoChangedEvent;

        //Load 
        //LoadRewarded();
    }
    // Show Banner when using
    private void LoadRewarded()
    {
        rewardedAd.LoadAd();
        Debug.Log("RewardedAd Has Loaded");
    }
    public void ShowRewarded()
    {
        if (rewardedAd.IsAdReady())
        {
            rewardedAd.ShowAd();
            Debug.Log("RewardedAd Has Showed");
        }
    }
    // Destroy Banner when not using
    // Implement the events
    void RewardedOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        //Call when load failed
        //LoadRewarded();
    }
    void RewardedOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError error) { }
    void RewardedOnAdRewardedEvent(LevelPlayAdInfo adInfo, LevelPlayReward adReward)
    {
        string rewardedName = adReward.Name;
        int rewardedAmount = adReward.Amount;
        MoneyRewarded += rewardedAmount;
        Debug.Log($"Get Reward Name: {rewardedName}, Reward Amount: {rewardedAmount}");
    }
    void RewardedOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        //Call when load failed
        //LoadRewarded(); 
    }
    void RewardedOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void RewardedOnAdInfoChangedEvent(LevelPlayAdInfo adInfo) { }
    #endregion
}