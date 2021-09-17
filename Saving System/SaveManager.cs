using UnityEngine;

public class SaveManager : MonoBehaviour {
    bool loaded;

    void Start() {
        LoadJsonData();
    }

    void SaveJsonData() {
        if (!loaded)
            return;

        SaveData sd = new SaveData();
        PopulateSaveData(sd);

        if (FileManager.WriteToFile("SaveData.dat", sd.ToJson())) {
            Debug.Log("Save successful");
        }
    }

    void LoadJsonData() {
        if (FileManager.LoadFromFile("SaveData.dat", out var json)) {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            LoadFromSaveData(sd);
            Debug.Log("Load complete");
        } else {
            LoadDefault();
        }
        loaded = true;
    }

    void LoadFromSaveData(SaveData sd) {
        if (PlayerStats.Instance) {
            PlayerStats playerStats = PlayerStats.Instance;
            
            playerStats.SetUnlockedLevelCount(Mathf.Max(sd.UnlockedLevelCount, 1));

            playerStats.SetStarPoints(sd.StarPoints);
            playerStats.SetUsedStars(sd.UsedStars);

            playerStats.SetAttackCount(SkillType.BasicAttack, Mathf.Max(sd.BasicAttackCount, 3));
            playerStats.SetAttackCount(SkillType.ComboAttack, Mathf.Max(sd.ComboAttackCount, 4));

            playerStats.SetSkinIndex(sd.SkinIndex);
            playerStats.SetUnlockedSkillIndexes(sd.UnlockedSkinsIndexes);
        }
    }

    void PopulateSaveData(SaveData sd) {            
        if (PlayerStats.Instance) {
            PlayerStats playerStats = PlayerStats.Instance;

            sd.UnlockedLevelCount = playerStats.UnlockedLevelCount;

            sd.StarPoints = playerStats.StarPoints;
            sd.UsedStars = playerStats.UsedStars;

            sd.BasicAttackCount = playerStats.BasicAttackCount;
            sd.ComboAttackCount = playerStats.ComboAttackCount;

            sd.SkinIndex = playerStats.SkinIndex;
            sd.UnlockedSkinsIndexes = playerStats.UnlockedSkinsIndexes.ToArray();
        }
    }

    void LoadDefault() {
        PlayerStats playerStats = PlayerStats.Instance;
        playerStats.SetUnlockedLevelCount(1);

        playerStats.SetStarPoints(0);
        playerStats.SetUsedStars(0);

        playerStats.SetAttackCount(SkillType.BasicAttack, 3);
        playerStats.SetAttackCount(SkillType.ComboAttack, 4);

        playerStats.SetSkinIndex(0);
        playerStats.SetUnlockedSkillIndexes(new int[] {0});
    }

    void OnApplicationQuit() {
        SaveJsonData();
    }

    void OnApplicationPause(bool pauseStatus) {
        SaveJsonData();
    }

    void OnApplicationFocus(bool focusStatus) {
        SaveJsonData();
    }

    public void Save() {
        SaveJsonData();
    }
}