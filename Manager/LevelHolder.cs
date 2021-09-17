using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LevelHolder : MonoBehaviour {
    [SerializeField] Sound levelSelectedSound;
    [SerializeField] LevelSOHolder[] levelHolders;

    [Header("Grid mover")]
    [SerializeField] Button leftArrow;
    [SerializeField] Button rightArrow;
    [SerializeField] int gridCount;
    [SerializeField] int moveDistance;
    [SerializeField] int duration;

    [Header("To create buttons")]
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;
    [SerializeField] int buttonCount;

    [Space]
    [SerializeField] TextMeshProUGUI starPointsText;

    bool moving;
    int curIndex;

    private void Start() {
        for (int i = 0; i < PlayerStats.Instance.UnlockedLevelCount; i++) {
            LevelSOHolder level = levelHolders[i];

            level.Button.interactable = true;
            level.Button.onClick.AddListener(level.Level.SetGameConfigurations);
            level.Button.onClick.AddListener(PlaySound);
        }

        // grid mover
        leftArrow.onClick.AddListener(MoveLeft);
        rightArrow.onClick.AddListener(MoveRight);

        UpdateAvailableDirections();
        UpdateStarPoints();
    }

    void PlaySound() {
        AudioManager.Instance.Play(levelSelectedSound.Audio, levelSelectedSound.Volume);
    }

    void MoveRight() {
        if (moving)
            return;

        moving = true;
        curIndex = (curIndex + 1) % gridCount;

        transform.DOLocalMoveX(transform.localPosition.x - moveDistance, duration)
        .OnComplete(delegate{moving=false;});

        UpdateAvailableDirections();
    }

    void MoveLeft() {
        if (moving)
            return;

        moving = true;
        curIndex = curIndex - 1 < 0 ? gridCount - 1 : curIndex - 1;

        transform.DOLocalMoveX(transform.localPosition.x + moveDistance, duration)
        .OnComplete(delegate{moving=false;});

        UpdateAvailableDirections();
    }

    void UpdateAvailableDirections() {
        leftArrow.gameObject.SetActive(curIndex != 0);
        rightArrow.gameObject.SetActive(curIndex != gridCount -1);
    }
    
#if UNITY_EDITOR
    public void FindLevelsInResources() {        
        Object[] levelObjects = Resources.LoadAll("Levels");
        Level[] levels = new Level[levelObjects.Length];
        levelObjects.CopyTo(levels, 0);
        System.Array.Sort(levels, delegate(Level level1, Level level2) {
                    return int.Parse(level1.name).CompareTo(int.Parse(level2.name));
                  });
        
        levelHolders = new LevelSOHolder[levels.Length];
        for (int i = 0; i < levels.Length; i++) {
            levelHolders[i] = new LevelSOHolder();
            levelHolders[i].Level = levels[i];
            levelHolders[i].Level.Holder = levelHolders[i];

            if (i+1 < levels.Length)
                levelHolders[i].Level.NextLevel = levels[i+1];
            
            UnityEditor.EditorUtility.SetDirty(levels[i]);
        }
    }

    public void FindButtonsInChildren() {
        Button[] buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++) {
            levelHolders[i].Button = buttons[i];
        }
    }

    public void CreateButtonsInChildren() {
        for (int i = 0; i < buttonCount; i++) {
            GameObject b = Instantiate(buttonPrefab, buttonParent);

            string n = (i + 1).ToString();
            b.name = n;
            b.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = n;
        }
    }
#endif

    void UpdateStarPoints() {
        starPointsText.text = PlayerStats.Instance.StarPoints.ToString();
    }
}

[System.Serializable]
public class LevelSOHolder {
    public Level Level;
    public Button Button;
}