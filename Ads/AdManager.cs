using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour {
    // InterstitialAd interstitialAd;
    // RewardedAd rewardedAd;
    // RewardedInterstitialAd rewardedInterstitialAd;

    void Start() {
        MobileAds.Initialize(InitializationStatus => {  });
    }

    // AdRequest CreateAdRequest() {
    //     return new AdRequest.Builder().Build();
    // }

// #region interstitial ads
//     public void ShowInterstitialAd() {
//         RequestInterstitialAd();
//         if (interstitialAd.IsLoaded()) {
//             interstitialAd.Show();
//         } else {
//             #if UNITY_EDITOR
//             Debug.Log("Interstitial not ready yet");
//             #endif
//         }
//     }

//     void RequestInterstitialAd() {
//         string adUnitId = "none";     

//         if (interstitialAd != null)
//             interstitialAd.Destroy();
        
//         interstitialAd = new InterstitialAd(adUnitId);
//         interstitialAd.LoadAd(CreateAdRequest());
//     }
// #endregion

// #region rewarded ads
//     public System.Action RewardedAdReward;

//     public void UserChoseToWatchAd() {
//         RequestRewardedAd();
//         if (rewardedAd.IsLoaded()) {
//             rewardedAd.Show();
//         } else {
//             #if UNITY_EDITOR
//             Debug.Log("Rewarded not ready yet");
//             #endif
//         }
//     }

//     void RequestRewardedAd() {
//         string adUnitId = "ca-app-pub-1207640803653889/8118487655";

//         rewardedAd = new RewardedAd(adUnitId);
//         rewardedAd.LoadAd(CreateAdRequest());

//         // Called when an ad request has successfully loaded.
//         this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
//         // Called when an ad is shown.
//         this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
//         // Called when the user should be rewarded for interacting with the ad.
//         this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//         // Called when the ad is closed.
//         this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
//     }

//     public void HandleRewardedAdLoaded(object sender, EventArgs args)
//     {
//         MonoBehaviour.print("HandleRewardedAdLoaded event received");
//     }

//     public void HandleRewardedAdOpening(object sender, EventArgs args)
//     {
//         MonoBehaviour.print("HandleRewardedAdOpening event received");
//     }

//     public void HandleRewardedAdClosed(object sender, EventArgs args)
//     {
//         MonoBehaviour.print("HandleRewardedAdClosed event received");
//     }

//     public void HandleUserEarnedReward(object sender, Reward args)
//     {
//         string type = args.Type;
//         double amount = args.Amount;
//         MonoBehaviour.print(
//             "HandleRewardedAdRewarded event received for "
//                         + amount.ToString() + " " + type);

//         if (RewardedAdReward != null) {
//             RewardedAdReward();
//         }
//     }
//     #endregion

// #region rewarded interstitial
//     public System.Action InterstitialAdReward;

//     public void ShowRewardedInterstitialAd() {
//         RequestRewardedInterstitialAd();
//         if (rewardedInterstitialAd != null) {
//             rewardedInterstitialAd.Show(userEarnedRewardCallback);
//         }
//     }

//     private void userEarnedRewardCallback(Reward reward) {
//         if (InterstitialAdReward != null) {
//             InterstitialAdReward();
//         }
//         Debug.Log("Rewarded");
//     }

//     void RequestRewardedInterstitialAd() {
//         string adUnitId = "ca-app-pub-1207640803653889/7611772535";
//         adLoadCallback += AdLoadCallback;
//         RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), adLoadCallback);
//     }

//     Action<RewardedInterstitialAd, AdFailedToLoadEventArgs> adLoadCallback;
//     void AdLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error) {
//         if (error == null) {
//             rewardedInterstitialAd = ad;

//             rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresent;
//             rewardedInterstitialAd.OnAdDidPresentFullScreenContent += HandleAdDidPresent;
//             rewardedInterstitialAd.OnAdDidDismissFullScreenContent += HandleAdDidDismiss;
//             rewardedInterstitialAd.OnPaidEvent += HandlePaidEvent;
//         }
//     }

//     void HandleAdFailedToPresent(object sender, AdErrorEventArgs args) {
//         MonoBehaviour.print("Rewarded interstitial ad has failed to present.");
//     }

//     void HandleAdDidPresent(object sender, EventArgs args) {
//         MonoBehaviour.print("Rewarded interstitial ad has presented.");
//     }

//     void HandleAdDidDismiss(object sender, EventArgs args) {
//         MonoBehaviour.print("Rewarded interstitial ad has dismissed presentation.");
//     }

//     void HandlePaidEvent(object sender, AdValueEventArgs args) {
//         MonoBehaviour.print(
//             "Rewarded interstitial ad has received a paid event.");
//     }
// #endregion
}