using UnityEngine;
using TMPro;

public class LevelSystemUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI levelUI;
    [SerializeField] TextMeshProUGUI expUI;
    [SerializeField] TextMeshProUGUI pointsUI;

    private void Start() {
        // LevelSystem.Instance.AfterAddExp += UpdateLevelUI;

        if (pointsUI != null) {
            UpdatePointsUI();
            PlayerStats.Instance.OnSkillPointsChange += UpdatePointsUI;   
        }

        if (levelUI != null) {
            Invoke("UpdateLevelUI", .1f); 
        }
    }

    void UpdateLevelUI() {
        levelUI.text = "LV: " + LevelSystem.Instance.Level.ToString();
        expUI.text = "XP: " + LevelSystem.Instance.Experience.ToString() + " / " + LevelSystem.Instance.ExperienceToNextLevel.ToString();
    }

    void UpdatePointsUI() {
        // pointsUI.text = "Skill Points : " + PlayerStats.Instance.SkillPoints.ToString();
    }

    private void OnDisable() {
        // LevelSystem.Instance.AfterAddExp -= UpdateLevelUI;

        if (pointsUI != null) {
            PlayerStats.Instance.OnSkillPointsChange -= UpdatePointsUI;   
        }
    }
}