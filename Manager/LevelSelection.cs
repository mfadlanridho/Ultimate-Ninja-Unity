using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {
    // [SerializeField] EnemyConfiguration enemyConfiguration;
    [SerializeField] MapConfiguration mapConfiguration;
    
    Button levelButton;
    
    private void Start() {
        levelButton = GetComponent<Button>();
        if (mapConfiguration.Unlocked)
            levelButton.interactable = true;
    }

    public void OnClick() {
        // MapAttributes.Instance.Configuration = mapConfiguration;
        // EnemyAttributes.Instance.Configuration = enemyConfiguration;
    }

    void AccessButton() {
        levelButton.interactable = true;
    }
}