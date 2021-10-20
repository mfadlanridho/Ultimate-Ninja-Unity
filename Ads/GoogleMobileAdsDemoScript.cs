using UnityEngine;

public class GoogleMobileAdsDemoScript : MonoBehaviour {
    private void Start() {
        // Load an app open ad when the scene starts
        AppOpenAdManager.Instance.LoadAd();
    }

    private void OnApplicationPause(bool paused) {
        // Display the app open ad when the app is foregrounded
        if (!paused) {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }
}