using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {
    [SerializeField] SkillTypeCountDictionary skillTypeCount;

    public System.Action OnSkillPointsChange;

    # region Singleton
    public static PlayerStats Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    public float MaxHealth;
    public float Speed = 5;
    public float Damage = 5;

    [Space]
    public int StarPoints;
    public int UsedStars;

    public int AvailableStarPoints {
        get {
            return StarPoints - UsedStars;
        }
    }

    [Space]
    public List<int> UnlockedSkinsIndexes;
    public int UnlockedLevelCount {get; private set;}

    public GameState GameState;

    public SkillTypeCountDictionary SkillTypeCount => skillTypeCount;
    public int BasicAttackCount => skillTypeCount[SkillType.BasicAttack];
    public int ComboAttackCount => skillTypeCount[SkillType.ComboAttack];

    public int SkinIndex;

    private void Start() {
        SkinsHolder skinsHolder = FindObjectOfType<SkinsHolder>();
        PlayerSkin skin = skinsHolder.Skins[SkinIndex]; 
        skin.gameObject.SetActive(true);
        SetMaxHealth(skin.MaxHealth);
        SetSpeed(skin.Speed);
    }

    public void SetAttackCount(SkillType type, int count) {
        skillTypeCount[type] = count;
    }

    public void UseStar(int count = 1) {
        UsedStars += count;
    }

    public void IncreaseStarPoints(int value) {
        StarPoints += value;
    }

    public void IncreaseUnlockedLevelCount() {
        UnlockedLevelCount++;
        Debug.Log("Unlocked level count is now " + UnlockedLevelCount);
    }

    public void SetStarPoints(int value) {
        StarPoints = value;
    }

    public void SetUsedStars(int value) {
        UsedStars = value;
    }

    public void SetUnlockedLevelCount(int value) {
        UnlockedLevelCount = value;
    }

    public void SetSkinIndex(int index) {
        SkinIndex = index;
    }

    public void SetMaxHealth(float value) {
        MaxHealth = value;
        GameObject.FindWithTag("Player").GetComponent<Player>().SetMaxHealth(value);
    }

    public void SetSpeed(float value) {
        Speed = value;
    }

    public void AddUnlockedSkillIndex(int index) {
        UnlockedSkinsIndexes.Add(index);
    }

    public void SetUnlockedSkillIndexes(int[] indexes) {
        UnlockedSkinsIndexes = new List<int>(indexes);

        if (!UnlockedSkinsIndexes.Contains(0)) {
            UnlockedSkinsIndexes.Add(0);
        }
    }
}

public enum GameState {
    InMenu,
    InGame
}