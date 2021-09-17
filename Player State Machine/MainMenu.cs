using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject levelSelectionUI;
    [SerializeField] GameObject skillUI;

    [Header("In menu transform")]
    [SerializeField] Vector3 playerPosition;
    [SerializeField] Vector3 playerRotation;

    Animator playerAnimator;

# region singleton
    public static MainMenu Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
# endregion

    private void Start() {
        PlayerStateMachine.Instance.SetState(PlayerState.InMenu);
        playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        playerAnimator.transform.position = playerPosition;
        playerAnimator.transform.eulerAngles = playerRotation;

        if (ComingFromGame) {
            mainUI.SetActive(false);
            skillUI.SetActive(false);
            levelSelectionUI.SetActive(true);
        }

        playerAnimator.Play("In Menu");
        PlayerStats.Instance.GameState = GameState.InMenu;
    }

    public void PlayGame() {
        playerAnimator.Play("Movement");
        PlayerStats.Instance.GameState = GameState.InGame;
        PlayerStateMachine.Instance.SetState(PlayerState.None);
        LevelLoader.Instance.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    bool ComingFromGame => PlayerStats.Instance.GameState == GameState.InGame;
}

[System.Serializable]
public enum MapScene {
    MAP1,
    MAP2
}