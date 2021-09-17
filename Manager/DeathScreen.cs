using UnityEngine;

public class DeathScreen : MonoBehaviour {
    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject deathUIWithoutAds;
    [SerializeField] GameObject watchAdToContinueUI;

    [SerializeField] Sound deathSound;

    public bool WatchedAd{get; private set;}

    Player player;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.OnDeath += InitializeDeathScreen;
    }

    private void OnDisable() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.OnDeath -= InitializeDeathScreen;
    }

    void InitializeDeathScreen() {
        StopTime();

        if (deathSound != null) {
            AudioManager.Instance.Play(deathSound.Audio, deathSound.Volume);
        }

        if (!WatchedAd) {
            WatchedAd = true;
            deathUI.SetActive(true);
        } else {
            deathUIWithoutAds.SetActive(true);
        }
    }

    void StopTime() {
        // Time.timeScale = 0;
    }

    // used in button
    public void WatchAd() {
        AdManager.Instance.InterstitialAdReward += Revive;
        AdManager.Instance.ShowRewardedInterstitialAd();
    }

    void Revive() {
        deathUI.SetActive(false);
        player.Revive();
        AdManager.Instance.InterstitialAdReward -= Revive;
    }
}