using UnityEngine;

public class SaveData {
    public int UnlockedLevelCount;
    
    public int StarPoints;
    public int UsedStars;

    public int BasicAttackCount;
    public int ComboAttackCount;

    public int SkinIndex;

    public int[] UnlockedSkinsIndexes;

    public string ToJson() {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json) {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}