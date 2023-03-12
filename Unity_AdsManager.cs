using UnityEngine;
using UnityEngine.Advertisements;

public class Unity_AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static Unity_AdsManager Instance;
    
    private string banner_Android = "Banner_Android", interstitial_Android = "Interstitial_Android", rewarded_Android = "Rewarded_Android";

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    string _gameId;
    [SerializeField] bool _testMode = true;
    int calledindex;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (Advertisement.isInitialized)
        {
            Debug.Log("Advertisement is Initialized");
            //LoadRewardedAd();
        }
        else
        {
            InitializeAds();
        }
    }
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        //LoadBannerAd();
        LoadInerstitialAd();       
        LoadRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadInerstitialAd()
    {
        Advertisement.Load(interstitial_Android, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowInterstitialAd()
    {
        LoadInerstitialAd();
        Advertisement.Show(interstitial_Android, this);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(rewarded_Android, this);
    }

    public void ShowRewardedAd()
    {
        LoadRewardedAd();
        Advertisement.Show(rewarded_Android, this);
    }

    //This function show ads directly if loaded and called.
    public void OnUnityAdsAdLoaded(string placementId)
    {
        //Debug.Log("OnUnityAdsAdLoaded");
        //Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
        LoadInerstitialAd();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");
        //Time.timeScale = 0;
        //Advertisement.Banner.Hide();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete " + showCompletionState);
        //if (placementId.Equals("Rewarded_Android") && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        //{
        //    Debug.Log("rewared Player");
        //}
        //Time.timeScale = 1;
        //Advertisement.Banner.Show("Banner_Android");

        //if (placementId.Equals("video") && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        //{
        //    if (calledindex == 1)
        //        UiManager.instance.retryafterad();
        //    if (calledindex == 2)
        //        FindObjectOfType<PlayerController>().afterdeath();
        //    if (calledindex == 3)
        //        FindObjectOfType<PlayerController>().afterwin();
        //}
        //Time.timeScale = 1;
    }



    public void LoadBannerAd()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(banner_Android,
            new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            }
            );
    }

    void OnBannerLoaded()
    {
        Advertisement.Banner.Show(banner_Android);
    }

    void OnBannerError(string message)
    {

    }

}
