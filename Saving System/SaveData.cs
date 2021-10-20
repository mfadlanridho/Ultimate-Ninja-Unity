using UnityEngine;

public class SaveData {
    public int UnlockedLevelCount;
    
    public int StarPoints;
    public int UsedStars;

    public int BasicAttackCount;
    public int ComboAttackCount;

    public int SkinIndex;

    public int[] UnlockedSkinsIndexes;

    public int[] LevelStarsIndex;
    public int[] LevelStarsCount;

    public bool MusicDisabled;

    public string ToJson() {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json) {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}