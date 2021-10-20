using GoogleMobileAds.Api;

public class RewardedAdManager {
    public System.Action OnUserEarnedRewardEvent;

    private RewardedAd rewardedAd;
    private string adUnitId;

    public RewardedAdManager(string adUnitId) {
        this.adUnitId = adUnitId;
    }

    public void RequestAndLoadRewardedAd() {
        rewardedAd = new RewardedAd(adUnitId);
        rewardedAd.OnUserEarnedReward += (sender, args) => OnUserEarnedRewardEvent.Invoke();

        // Create empty ad request
        rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowRewardedAd() {
        if (rewardedAd != null) {
            rewardedAd.Show();
        }
    }

    AdRequest CreateAdRequest() {
        return new AdRequest.Builder().Build();
    }
}