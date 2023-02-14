//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using UnityEngine.UI;

//public class AdmobNativeAdController : MonoBehaviour
//{

   
//    private UnifiedNativeAd nativeAd;
//    private bool nativeLoaded = false;
//    [SerializeField] GameObject adNativePanel;
//    [SerializeField] RawImage adIcon;
//    [SerializeField] RawImage adChoices;

//    [SerializeField] Text adHeadline;
//    [SerializeField] Text adCallToAction;
//    [SerializeField] Text adAdvertiser;

//    // Start is called before the first frame update
//    void Start()
//    {
//       // nativeLoaded = false;
//       // adNativePanel.SetActive(false);
//       // RequestNativeAd();
//    }
//    void OnEnable()
//    {
        
//        RequestNativeAd();
//    }
//    private void OnDisable()
//   {
//        //nativeAd.Destroy();
//       nativeLoaded = false;
//    }

//    #region Native Ads Methods ---------------------------------
//    public void RequestNativeAd()
//    {
//        if (!AdMob.instance.TestAds)
//        {
//            AdLoader adLoader = new AdLoader.Builder(AdMob.instance.advanceNativeID)
//            .ForUnifiedNativeAd()
//            .Build();
//            adLoader.OnUnifiedNativeAdLoaded += this.HandleUnifiedNativeAdLoaded;
//            adLoader.LoadAd(new AdRequest.Builder().Build());
//        }
//        else 
//        {
//            AdLoader adLoader = new AdLoader.Builder("ca-app-pub-3940256099942544/2247696110")
//                        .ForUnifiedNativeAd()
//                        .Build();
//            adLoader.OnUnifiedNativeAdLoaded += this.HandleUnifiedNativeAdLoaded;
//            adLoader.LoadAd(new AdRequest.Builder().Build());
//        }
//    }


//    #region Native Ads  Event Handlers
//    private void HandleUnifiedNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
//    {
//        this.nativeAd = args.nativeAd;
//        nativeLoaded = true;
//    }
//    #endregion End Native Ads Event Handlers
//    private void Update()
//    {
//        if (nativeLoaded)
//        {
//            nativeLoaded = false;
//            Texture2D iconTexture = this.nativeAd.GetIconTexture();
//            Texture2D iconAdChoices = this.nativeAd.GetAdChoicesLogoTexture();
//            string headline = this.nativeAd.GetHeadlineText();
//            string cta = this.nativeAd.GetCallToActionText();
//            string advertiser = this.nativeAd.GetAdvertiserText();

//            adIcon.texture = iconTexture;
//            adChoices.texture = iconAdChoices;
//            adHeadline.text = headline;
//            adAdvertiser.text = advertiser;
//            adCallToAction.text = cta;
//            nativeAd.RegisterIconImageGameObject(adIcon.gameObject);
//            nativeAd.RegisterAdChoicesLogoGameObject(adChoices.gameObject);
//            nativeAd.RegisterHeadlineTextGameObject(adHeadline.gameObject);
//            nativeAd.RegisterCallToActionGameObject(adCallToAction.gameObject);
//            nativeAd.RegisterAdvertiserTextGameObject(adAdvertiser.gameObject);
//            adNativePanel.SetActive(true);
//        }

//    }



//    #endregion
//}
