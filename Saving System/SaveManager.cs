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

            playerStats.SetLevelStarsCount(sd.LevelStarsIndex, sd.LevelStarsCount);

            playerStats.SetMusicDisabled(sd.MusicDisabled);
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


            sd.LevelStarsIndex = new int[playerStats.LevelStarsCount.Count];
            sd.LevelStarsCount = new int[playerStats.LevelStarsCount.Count];

            int i = 0;
            foreach (var item in playerStats.LevelStarsCount) {
                sd.LevelStarsIndex[i] = item.Key;
                sd.LevelStarsCount[i] = item.Value;
                i++;
            }

            sd.MusicDisabled = playerStats.MusicDisabled;
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

        playerStats.SetLevelStarsCount(new int[] {}, new int[] {});
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