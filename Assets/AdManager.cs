using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour,IUnityAdsListener
{
    #if UNITY_IOS
        private string gameId = "4432496";
        private string interstitialAd = "Interstitial_iOS";
        private string bannerAd = "Banner_iOS";
        private string rewardAd = "Rewarded_iOS";
    #else
        private string gameId = "4432497";
        private string interstitialAd = "Interstitial_Android";
        private string bannerAd = "Banner_Android";
        private string rewardedAd = "Rewarded_Android";
    #endif

        // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
        this.playBannerAd();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void playAd()
    {
        if (Advertisement.IsReady(interstitialAd))
        {
            Advertisement.Show(interstitialAd);
        }
    }
    
    public void playBannerAd()
    {
        if (Advertisement.IsReady(bannerAd))
        {
            Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
            Advertisement.Show(bannerAd);
        }
        else
        {
            RepeatShowBanner();
        }
        
    }

    IEnumerator RepeatShowBanner()
    {
        yield return new WaitForSeconds(1);
        playBannerAd();
    }

    public void playRewardAd()
    {
        if (Advertisement.IsReady(rewardedAd))
        {
            Advertisement.Show(rewardedAd);
        }
        else
        {
            Debug.Log("Rewarded ad not ready");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedAd && showResult == ShowResult.Finished)
        {
            Controller.Instance.gameOverReward();
        }
    }
}
