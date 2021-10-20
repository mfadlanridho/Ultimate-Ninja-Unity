using UnityEngine;
using TMPro;
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

        GameManager.Instance.GameCompleteEvent += MapCompleted;
    }

    void MapCompleted() {
        // MapAttributes.Instance.MapIsComplete();

        AudioManager.Instance.Play(sound.Audio, sound.Volume, player.transform.position);
        PlayerStateMachine.Instance.SetState(PlayerState.Victory);
        playerAnimator.Play("Victory");
        
        FindObjectOfType<SaveManager>().Save();
        Invoke("ShowCompletedUI", 2f);
    }

    void ShowCompletedUI() {
        completedUI?.SetActive(true);
        gameplayUI?.SetActive(false);
    }
}