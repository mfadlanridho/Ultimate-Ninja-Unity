using UnityEngine;
using System.Collections;
using TMPro;

public class DeathScreen : MonoBehaviour {
    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject interstitialUI;
    
    [SerializeField] TextMeshProUGUI reviveText;
    [SerializeField] Sound deathSound;

    Player player;
    int countdown;
    bool stopped;

    RewardedInterstitialAdManager rewardedInterstitialAdManager;
    RewardedAdManager rewardedAdManager;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.OnDeath += InitializeDeathScreen;

        rewardedInterstitialAdManager = GetComponent<RewardedInterstitialAdManager>();
        rewardedInterstitialAdManager.RewardedInterstitialAdReward += Revive;
        rewardedInterstitialAdManager.RequestAndLoadRewardedInterstitialAd();

        rewardedAdManager = new RewardedAdManager("ca-app-pub-1207640803653889/6675814989");
        rewardedAdManager.OnUserEarnedRewardEvent += Revive;
        rewardedAdManager.RequestAndLoadRewardedAd();
    }

    private void OnDisable() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.OnDeath -= InitializeDeathScreen;
    }

    void InitializeDeathScreen() {
        if (deathSound != null) {
            AudioManager.Instance.Play(deathSound.Audio, deathSound.Volume);
        }

        rewardedInterstitialAdManager.RequestAndLoadRewardedInterstitialAd();
        countdown = 6;
        StartCoroutine(CountdownAdCoroutine());
        deathUI.SetActive(true);
    }

    IEnumerator CountdownAdCoroutine() {
        while (countdown > 0 && !stopped) {
            countdown -= 1;
            reviveText.text = "Play ad to revive in " + countdown.ToString();
            yield return new WaitForSeconds(1f);
        }

        if (countdown == 0) {
            WatchAd();
        }
        stopped = false;
    }

    // used in button
    public void WatchAd() {
        countdown = -1;
        Stop();
        rewardedInterstitialAdManager.ShowRewardedInterstitialAd();
        rewardedInterstitialAdManager.RequestAndLoadRewardedInterstitialAd();
    }

    void Revive() {
        Invoke("InvokeRevive", .5f);
    }

    void InvokeRevive() {
        deathUI.SetActive(false);
        interstitialUI.SetActive(true);
        player.Revive();
        stopped = false;
    }

    public void Stop() {
        stopped = true;
        interstitialUI.SetActive(false);
        Debug.Log("Stoppped");
    }

    public void WatchOptAd() {
        rewardedAdManager.ShowRewardedAd();
        rewardedAdManager.RequestAndLoadRewardedAd();
        stopped = false;
    }
}