using UnityEngine;

public class LevelSystem : MonoBehaviour {
    public int Level;
    public int Experience;
    public int ExperienceToNextLevel;

    public System.Action AfterAddExp;
    public System.Action AfterLevelUp;

    # region Singleton
    public static LevelSystem Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            ExperienceToNextLevel = GetLevelExp(Level);
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    # endregion

    # region public methods
    public void AddExperience(int amount) {
        Experience += amount;
        while (Experience >= ExperienceToNextLevel) {
            LevelUp();
        }

        if (AfterAddExp != null) {
            AfterAddExp();
        }
    }
    #endregion

    // Disgea
    int GetLevelExp(int level) {
        return Mathf.RoundToInt(.04f * Mathf.Pow(level, 3) + .8f * Mathf.Pow(level, 2) + 2 * level);
    }

    void LevelUp() {
        Level ++;
        Experience -= ExperienceToNextLevel;
        ExperienceToNextLevel = GetLevelExp(Level);

        if (AfterLevelUp != null) {
            AfterLevelUp();
        }
        AddSkillPoint();
    }

    void AddSkillPoint() {
        // PlayerStats.Instance.SkillPoints += 1;  
    }
}