using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {

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
    
    public int LevelIndex {get; private set;}

    public float MaxHealth {get; private set;}
    public float Speed {get; private set;}
    public float Damage {get {return 25000f;}}

    public int StarPoints {get; private set;}
    public int UsedStars {get; private set;}
    public int AvailableStarPoints {get{return StarPoints - UsedStars;}}

    public int UnlockedLevelCount {get; private set;}

    public GameState GameState {get; private set;}

    public int BasicAttackCount {get; private set;}
    public int ComboAttackCount {get; private set;}

    public List<int> UnlockedSkinsIndexes {get; private set;}
    public int SkinIndex {get; private set;}

    public Dictionary<int, int> LevelStarsCount {get; private set;}

    public bool MusicDisabled {get; private set;}

    private void Start() {
        Invoke("ShitCode", .1f);
    }

    void ShitCode() {
        SkinsHolder skinsHolder = FindObjectOfType<SkinsHolder>();
        PlayerSkin skin = skinsHolder.Skins[SkinIndex]; 
        skin.gameObject.SetActive(true);
        SetMaxHealth(skin.MaxHealth);
        SetSpeed(skin.Speed);
    }

    public void SetGameState(GameState state) {
        GameState = state;
    }

    public void IncreaseAttackCount(SkillType type, int count = 1) {
        if (type == SkillType.BasicAttack) {
            BasicAttackCount += count;
        } else {
            ComboAttackCount += count;
        }
    }

    public void SetAttackCount(SkillType type, int value) {
        if (type == SkillType.BasicAttack) {
            BasicAttackCount = value;
        } else {
            ComboAttackCount = value;
        }
    }

    public void UseStar(int count = 1) {
        UsedStars += count;
    }

    public void IncreaseUnlockedLevelCount() {
        UnlockedLevelCount++;
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

    public void AddLevelStarsCount(int levelIndex, int count) {
        if (levelIndex < LevelStarsCount.Count) {
            int additionalStars = count - LevelStarsCount[levelIndex];
            if (additionalStars > 0) {
                LevelStarsCount[levelIndex] = count;
                StarPoints += additionalStars;
            }
        } else {
            LevelStarsCount.Add(levelIndex, count);
            StarPoints += count;
        }
    }

    public void SetLevelStarsCount(int[] levelStarsIndex, int[] levelStarsCount) {
        this.LevelStarsCount = new Dictionary<int, int>();

        for (int i = 0; i < levelStarsIndex.Length; i++) {
            this.LevelStarsCount.Add(levelStarsIndex[i], levelStarsCount[i]);
        }
    }

    public void SetMusicDisabled(bool value) {
        MusicDisabled = value;
    }
}

public enum GameState {
    InMenu,
    InGame
}