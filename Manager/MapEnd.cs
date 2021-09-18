using UnityEngine;
using TMPro;
using System.Collections;
using SegmentPoolingSystem;

public class MapEnd : MonoBehaviour {
    [SerializeField] GameObject completedUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] Sound sound;

    [Space]
    [SerializeField] TextMeshProUGUI text;

    [Space]
    [SerializeField] GameObject[] stars;
    [SerializeField] Sound starPopupSound;

    Player player;
    Animator playerAnimator;
    int finalPosition;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerAnimator = player.GetComponent<Animator>();

        GameManager.Instance.FinalSegmentEvent += MapCompleted;
    }

    void MapCompleted() {
        MapIsCompleted();

        SetPlayerStateToVictorious();
        PlayPlayersVictoriousAnimation();
        UpdatePlayerStatsInfo();
        
        FindObjectOfType<SaveManager>().Save();
        Invoke("ShowCompletedUI", 2f);
    }

    bool latestMap {
        get {
            return GameConfiguration.LevelIndex == PlayerStats.Instance.UnlockedLevelCount - 1;
        }
    }

    void UpdatePlayerStatsInfo() {
        if (latestMap) {
            PlayerStats.Instance.IncreaseStarPoints(GameManager.Instance.StarsPickedUp);
            PlayerStats.Instance.IncreaseUnlockedLevelCount();
        }
    }

    void ShowCompletedUI() {
        completedUI?.SetActive(true);
        gameplayUI?.SetActive(false);

        SetVictoriousDescription();
    }

    void SetVictoriousDescription() {
        int finalStarCount = Mathf.CeilToInt( 3f * (float) GameManager.Instance.StarsPickedUp / (float) GameManager.Instance.TotalStars);
        int starIncreaseDiff = Mathf.FloorToInt( (float) GameManager.Instance.TotalStars / 3f );
        StartCounting(GameManager.Instance.StarsPickedUp, starIncreaseDiff);
    }

    void PlayPlayersVictoriousAnimation() {
        AudioManager.Instance.Play(sound.Audio, sound.Volume, GameObject.FindWithTag("Player").transform.position);
        playerAnimator.Play("Victory");
    }

    void SetPlayerStateToVictorious() {
        PlayerStateMachine.Instance.SetState(PlayerState.Victory);
    }

    void MapIsCompleted() {
        // MapAttributes.Instance.MapIsComplete();
    }

    void StartCounting(int totalCount, int countDiff) {
        StartCoroutine(CountCoroutine(totalCount, countDiff));
    }

    IEnumerator CountCoroutine(int totalCount, int countDiff) {
        int currentCount = 0;
        int currentStarIndex = 0;

        while (currentCount < totalCount) {
            currentCount++;
            text.text = "Collected " + currentCount.ToString() + " Stars";

            if (currentCount % countDiff == 0) {
                stars[currentStarIndex].SetActive(true);
                AudioManager.Instance.Play(starPopupSound.Audio, starPopupSound.Volume);
                currentStarIndex++;
            }
            yield return new WaitForSeconds(.15f);
        }
        yield return null;
    }
}