using UnityEngine;
using TMPro;
using System.Collections;

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

        // EdgeMove edgeMove = FindObjectOfType<EdgeMove>();
        // edgeMove.OnFinalPosition += MapCompleted;
    }

    void MapCompleted() {
        MapIsCompleted();

        SetPlayerStateToVictorious();
        PlayPlayersVictoriousAnimation();
        UpdatePlayerStatsInfo();
        
        FindObjectOfType<SaveManager>().Save();
        Invoke("ShowCompletedUI", 2f);
    }

    void UpdatePlayerStatsInfo() {
        if (MapAttributes.Instance.LatestMap) {
            PlayerStats.Instance.IncreaseStarPoints(MapProgression.Instance.StarsPickedUp);
            PlayerStats.Instance.IncreaseUnlockedLevelCount();
        }
    }

    void ShowCompletedUI() {
        completedUI?.SetActive(true);
        gameplayUI?.SetActive(false);

        SetVictoriousDescription();
    }

    void SetVictoriousDescription() {
        int finalStarCount = Mathf.CeilToInt( 3f * (float) MapProgression.Instance.StarsPickedUp / (float) MapProgression.Instance.TotalStars);
        int starIncreaseDiff = Mathf.FloorToInt( (float) MapProgression.Instance.TotalStars / 3f );
        StartCounting(MapProgression.Instance.StarsPickedUp, starIncreaseDiff);
    }

    void PlayPlayersVictoriousAnimation() {
        AudioManager.Instance.Play(sound.Audio, sound.Volume, GameObject.FindWithTag("Player").transform.position);
        playerAnimator.Play("Victory");
    }

    void SetPlayerStateToVictorious() {
        PlayerStateMachine.Instance.SetState(PlayerState.Victory);
    }

    void MapIsCompleted() {
        MapAttributes.Instance.MapIsComplete();
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