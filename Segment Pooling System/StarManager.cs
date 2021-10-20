using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

namespace SegmentPoolingSystem {
public class StarManager : MonoBehaviour {
    [SerializeField] Transform prefab;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] GameObject collected;
    [SerializeField] GameObject[] UIStars;

    [SerializeField] Sound starPopupSound;
    [SerializeField] Sound starCollectSound;

    [SerializeField] GameObject watchAdForStars;
    
    Transform[] stars;
    Collider[] player = new Collider[1];

    int totalStarsSpawned;
    int totalStarsPickedUp;
    int currentStarsPickedUp;

    float time;
    bool active;

    RewardedAdManager rewardedAdManager;

    private void Start() {
        stars = new Transform[5];
        for (int i = 0; i < 5; i++) {
            stars[i] = Instantiate(prefab);
            stars[i].gameObject.SetActive(false);
        }
        GameManager.Instance.MoveToNextSegmentEvent += Spawn;
        GameManager.Instance.GameCompleteEvent += CountStars;

        rewardedAdManager = new RewardedAdManager("ca-app-pub-1207640803653889/8118487655");
        rewardedAdManager.OnUserEarnedRewardEvent += GainUnpickedStars;
    }

    void Update() {
        if (!active) {
            return;
        }
        else if (time < 0) {
            active = false;
            timeText.gameObject.SetActive(false);
            totalStarsPickedUp += currentStarsPickedUp;
            for (int i = 0; i < 5; i++) {
                stars[i].gameObject.SetActive(false);
            }
        }
        
        time -= Time.deltaTime;
        timeText.text = "Collect the stars in " + Mathf.CeilToInt(time).ToString();

        for (int i = 0; i < stars.Length - currentStarsPickedUp; i++) {
            // rotate
            stars[i].Rotate(0, 0, Time.deltaTime * 100f);

            // detect collision
            player[0] = null;
            Physics.OverlapSphereNonAlloc(stars[i].position, 1f, player, LayerMask.GetMask("Player"));
            if (player[0] != null) {
                AudioManager.Instance.Play(starCollectSound.Audio, starCollectSound.Volume);
                currentStarsPickedUp++;
                stars[i].gameObject.SetActive(false);
                
                var temp = stars[stars.Length - currentStarsPickedUp];
                stars[stars.Length - currentStarsPickedUp] = stars[i];
                stars[i] = temp;
                
                break;
            }
        }
    }

    void Spawn() {
        active = true;
        timeText.gameObject.SetActive(true);
        currentStarsPickedUp = 0;
        totalStarsSpawned += 5;
        time = 10f;

        Vector3 position = Vector3.right * (GameManager.Instance.Increment * GameManager.Instance.CurrentSegment - 15f);
        for (int i = 0; i < 5; i++) {
            stars[i].position = position + new Vector3(-5f + 2.5f * i, 5f, i%2 == 0 ? 2 : -2);
            stars[i].gameObject.SetActive(true);
            stars[i].DOMoveY(1f, 1f);
        }
        AnimateBrightness();
    }

    void CountStars() {
        PlayerStats.Instance.AddLevelStarsCount(GameConfiguration.LevelIndex, totalStarsPickedUp);
        if (GameConfiguration.LevelIndex == PlayerStats.Instance.UnlockedLevelCount - 1) {
            PlayerStats.Instance.IncreaseUnlockedLevelCount();
        }

        StartCoroutine(CountStarsCoroutine());
    }

    void AnimateCounting(Transform transform) {
        transform.localScale = Vector3.one * 2f;
        transform.DOScale(1, .5f);
    }

    void AnimateBrightness() {
        Sequence s = DOTween.Sequence();
        s.Append(timeText.fontSharedMaterial.DOFloat(1f, ShaderUtilities.ID_GlowOuter, 1f));
        s.Append(timeText.fontSharedMaterial.DOFloat(.1f, ShaderUtilities.ID_GlowOuter, 1f));
        s.OnComplete(OnTransition);
    }

    void OnTransition() {
        if (active)
            AnimateBrightness();
    }

    public void WatchAdForStars() {
        rewardedAdManager.ShowRewardedAd();
    }

    void GainUnpickedStars() {
        StartCoroutine(GainUnpickedStarsCoroutine());
    }

    int uiStarsIndex;
    int starsCounted;
    int pickedUpToStar;
    float timePerStar;

    IEnumerator CountStarsCoroutine() {
        timePerStar = 2f / totalStarsPickedUp;
        yield return new WaitForSeconds(2f);
        starsCounted = 0;
        uiStarsIndex = 0;
        pickedUpToStar = Mathf.FloorToInt((float) totalStarsSpawned / 3f);

        if (totalStarsPickedUp != totalStarsSpawned) {
            rewardedAdManager.RequestAndLoadRewardedAd();
        }

        while (starsCounted < totalStarsPickedUp) {
            starsCounted++;
            countText.text = starsCounted.ToString();
            AnimateCounting(countText.transform);

            if (starsCounted % pickedUpToStar == 0) {
                UIStars[uiStarsIndex].SetActive(true);
                AudioManager.Instance.Play(starPopupSound.Audio, starPopupSound.Volume);
                uiStarsIndex++;
            }
            yield return new WaitForSeconds(timePerStar);
        }

        if (totalStarsPickedUp != totalStarsSpawned) {
            watchAdForStars.SetActive(true);
        }
    }

    IEnumerator GainUnpickedStarsCoroutine() {
        while (starsCounted < totalStarsSpawned) {
            starsCounted++;
            countText.text = starsCounted.ToString();
            AnimateCounting(countText.transform);

            if (starsCounted % pickedUpToStar == 0) {
                UIStars[uiStarsIndex].SetActive(true);
                AudioManager.Instance.Play(starPopupSound.Audio, starPopupSound.Volume);
                uiStarsIndex++;
            }
            yield return new WaitForSeconds(timePerStar);
        }
        watchAdForStars.SetActive(false);
    }
}
}