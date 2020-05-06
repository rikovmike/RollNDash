using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAds : MonoBehaviour {

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;

    #if UNITY_IOS
        public const  string appId = "ca-app-pub-6898320612235081~7740774287";
    #elif UNITY_ANDROID
        public const  string appId = "ca-app-pub-6898320612235081~7740774287";
    #elif UNITY_EDITOR
        string appId = "unexpected_platform";
    #endif


        
    void Start () {
                // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        this.RequestBanner();
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;


        this.RequestRewardBasedVideo();
    }

    void Update(){

		if (GameManager.gameRestarts>8){
			GameManager.gameRestarts=0;
			this.RequestInterstitial();
		}
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-6898320612235081/8815070854";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-6898320612235081/8815070854";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    }



private void RequestInterstitial()
{
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6898320612235081/3371172484";
    #elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-6898320612235081/3371172484";
    #else
        string adUnitId = "unexpected_platform";
    #endif

    // Initialize an InterstitialAd.
    this.interstitial = new InterstitialAd(adUnitId);
}





    private void RequestRewardBasedVideo()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-6898320612235081/5805764135";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-6898320612235081/5805764135";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }






   public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        //
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
       // MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
        GameManager.RewardedSkipped();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
         GameManager.RewardedSuccess();
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
       GameManager.RewardedSkipped();
    }


	public void RewardedVideo(String none){
        if (rewardBasedVideo.IsLoaded()) {
            rewardBasedVideo.Show();
        }
	}


}