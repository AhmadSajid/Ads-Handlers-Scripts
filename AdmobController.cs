using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AdmobController : MonoBehaviour
{
    public static AdmobController Instance;

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedInterstitialAd rewardedInterstitialAd;

    [Header("Test Mode")]
    public bool TestMode;

    [Header("AppOpen")]
    public string androidAppOpen;
    public string iosAppOpen;

    [Header("Banner")]
    public string androidBanner;
    public string iosBanner;

    [Header("Interstitial")]
    public string androidInterstitial;
    public string iosInterstitial;

    [Header("RewardedVideo")]
    public string androidRewarded;
    public string iosRewarded;

    int calledindex;

    #region UNITY MONOBEHAVIOR METHODS
    private void Awake()
    {

        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
        deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);

    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
       // Callbacks from GoogleMobileAds are not guaranteed to be called on
       // main thread.
       // In this example we use MobileAdsEventExecutor to schedule these calls on
       // the next Update() loop.
       // Initialize the Mobile Ads SDK.

		MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Dictionary<string, AdapterStatus> map = initstatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        MonoBehaviour.print("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        MonoBehaviour.print("Adapter: " + className + " is initialized.");
						//LoadOpenAd();
						RequestBannerAd();
						RequestAndLoadInterstitialAd();
						RequestAndLoadRewardedAd();
                        break;
                }
            }
        });
       
    }
	
    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }

    #endregion

    #region BANNER ADS

    public void RequestBannerAd()
    {

        string adUnitId = "";
        // These ad units are configured to always serve test ads.
        if (!TestMode)
        {
#if UNITY_EDITOR
            adUnitId = "unused";
#elif UNITY_ANDROID
         adUnitId = androidBanner;
#elif UNITY_IPHONE
         adUnitId = iosBanner;
#else
         adUnitId = "unexpected_platform";
#endif
        }
        else
        {
#if UNITY_EDITOR
            adUnitId = "unused";
#elif UNITY_ANDROID
         adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
         adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
         adUnitId = "unexpected_platform";
#endif
        }
        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;


        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());
    }
    public void HideBanner()
    {
        bannerView.Hide();
    }
    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received.");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: " + args.LoadAdError.GetDomain());
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
    }



    #endregion
    #endregion

    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd()
    {

        string adUnitId = "";
        // These ad units are configured to always serve test ads.
        if (!TestMode)
        {
#if UNITY_EDITOR
            adUnitId = "unused";
#elif UNITY_ANDROID
         adUnitId = androidInterstitial;
#elif UNITY_IPHONE
         adUnitId = iosInterstitial;
#else
         adUnitId = "unexpected_platform";
#endif
        }
        else
        {
#if UNITY_EDITOR
            adUnitId = "unused";
#elif UNITY_ANDROID
         adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
         adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
         adUnitId = "unexpected_platform";
#endif
        }
        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        interstitialAd = new InterstitialAd(adUnitId);


        // Register for ad events.
        this.interstitialAd.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitialAd.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitialAd.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitialAd.OnAdClosed += this.HandleInterstitialClosed;

        // Load an interstitial ad
        interstitialAd.LoadAd(CreateAdRequest());
    }

    public void ShowInterstitialAd(int index)
    {
        calledindex = index;
        interstitialAd.Show();
    }

    public bool IsInitLoadInterstitial()
    {
        return interstitialAd.IsLoaded();
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }
    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("HandleInterstitialLoaded event received.");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleInterstitialFailedToLoad event received with message: " + args.LoadAdError.GetDomain());
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("HandleInterstitialClosed event received");
        RequestAndLoadInterstitialAd();

        //if (calledindex == 1)
        //    MenuHandler.Instance.playAfterAd();
        //if (calledindex == 2)
        //    MenuHandler.Instance.catgAfterAd();

    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        print("HandleInterstitialLeftApplication event received");
    }

    #endregion
    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {

        string adUnitId = "";
        // These ad units are configured to always serve test ads.
        if (!TestMode)
        {
#if UNITY_EDITOR
            adUnitId = "unused";
#elif UNITY_ANDROID
         adUnitId = androidRewarded;
#elif UNITY_IPHONE
         adUnitId = iosRewarded;
#else
         adUnitId = "unexpected_platform";
#endif
        }
        else
        {
#if UNITY_EDITOR
            adUnitId = "unused";
#elif UNITY_ANDROID
         adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
         adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
         adUnitId = "unexpected_platform";
#endif
        }

        // create new rewarded ad instance
        rewardedAd = new RewardedAd(adUnitId);

        /* // Add Event Handlers
         rewardedAd.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
         rewardedAd.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
         rewardedAd.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
         rewardedAd.OnAdFailedToShow += (sender, args) => OnAdFailedToShowEvent.Invoke();
         rewardedAd.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();
         rewardedAd.OnUserEarnedReward += (sender, args) => OnUserEarnedRewardEvent.Invoke();*/

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardedAd.OnAdLoaded += this.HandleRewardBasedVideoLoaded;

        this.rewardedAd.OnAdOpening += this.HandleRewardBasedVideoOpened;

        this.rewardedAd.OnUserEarnedReward += this.HandleRewardBasedVideoRewarded;
        this.rewardedAd.OnAdClosed += this.HandleRewardBasedVideoClosed;

        // Create empty ad request
        rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show();
        }
        else
        {
            Debug.Log("rewarded admob NAI CHALA");
        }
    }

    public void RequestAndLoadRewardedInterstitialAd()
    {

        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5354046379";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/6978759866";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), (rewardedInterstitialAd, error) =>
        {

            if (error != null)
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                });
                return;
            }

            this.rewardedInterstitialAd = rewardedInterstitialAd;
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {

            });
            // Register for ad events.
            this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                });
            };
            this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                });
                this.rewardedInterstitialAd = null;
            };
            this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                });
                this.rewardedInterstitialAd = null;
            };
        });
    }

    public void ShowRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show((reward) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {

                });
            });
        }
        else
        {

        }
    }
    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.LoadAdError.GetDomain());
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        RequestAndLoadRewardedAd();
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        //MenuHandler.Instance.DummyUnLocked();

        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
    #endregion

    #region AppOPEN ADS
    private AppOpenAd ad;

    public bool IsAdAvailable
    {
        get
        {
            return ad != null;
        }
    }
    public void LoadOpenAd()
    {

        string adUnitId = "";
        // These ad units are configured to always serve test ads.
        if (!TestMode)
        {
#if UNITY_EDITOR
            adUnitId = "unused";
#elif UNITY_ANDROID
         adUnitId = androidAppOpen;
#elif UNITY_IPHONE
         adUnitId = iosAppOpen;
#else
         adUnitId = "unexpected_platform";
#endif
        }
        else
        {
#if UNITY_EDITOR
            adUnitId = "unused";
#elif UNITY_ANDROID
         adUnitId = "ca-app-pub-3940256099942544/3419835294";
#elif UNITY_IPHONE
         adUnitId = "	ca-app-pub-3940256099942544/5662855259";
#else
         adUnitId = "unexpected_platform";
#endif
        }

        AdRequest request = new AdRequest.Builder().Build();

        // Load an app open ad for portrait orientation
        AppOpenAd.LoadAd(adUnitId, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                return;
            }

            // App open ad is loaded.
            ad = appOpenAd;
        }));
    }

    public void ShowAdIfAvailable()
    {
        if (!IsAdAvailable)
        {
            return;
        }

        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        ad.OnPaidEvent += HandlePaidEvent;

        ad.Show();
    }

    #region AppOpen callback handlers
    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;

        FindObjectOfType<splashscreen>().NextAfterAd();

    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
    {
        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        // LoadAd();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Displayed app open ad");

    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
                args.AdValue.CurrencyCode, args.AdValue.Value);
    }
    #endregion
    #endregion
}
