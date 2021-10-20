using UnityEngine;
using GoogleMobileAds.Common;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class RewardedInterstitialAdManager : MonoBehaviour {
    RewardedInterstitialAd rewardedInterstitialAd;
    public System.Action RewardedInterstitialAdReward;
    
    [SerializeField] Text statusText;

    public void RequestAndLoadRewardedInterstitialAd() {
        string adUnitId = "ca-app-pub-1207640803653889/7611772535";

        // Create an interstitial.
        RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), (rewardedInterstitialAd, error) => {
            if (error != null) {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "RewardedInterstitialAd load failed, error: " + error;
                });                
                return;
            }

            this.rewardedInterstitialAd = rewardedInterstitialAd;

            MobileAdsEventExecutor.ExecuteInUpdate(() => {
                statusText.text = "RewardedInterstitialAd loaded";
            });

            // Register for ad events.
            this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "Rewarded Interstitial presented.";
                });
            };
            this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "Rewarded Interstitial dismissed.";
                });
                this.rewardedInterstitialAd = null;
            };
            this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "Rewarded Interstitial failed to present.";
                });
                this.rewardedInterstitialAd = null;
            };
        });
    }

    public void ShowRewardedInterstitialAd() {
        if (rewardedInterstitialAd != null) {
            rewardedInterstitialAd.Show((reward) => {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    statusText.text = "User Rewarded: " + reward.Amount;
                });
            });
            if (RewardedInterstitialAdReward != null) {
                RewardedInterstitialAdReward();
            }
        }
        else {
            statusText.text = "Rewarded ad is not ready yet.";
        }
    }

    private AdRequest CreateAdRequest() {
        return new AdRequest.Builder().Build();
    }
}