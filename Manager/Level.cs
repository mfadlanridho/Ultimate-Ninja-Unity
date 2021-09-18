using UnityEngine;
using UnityEngine.UI;
using SegmentPoolingSystem;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject {
    public Level NextLevel;
    public bool Unlocked;
    public LevelSOHolder Holder;

    public void Unlock() {
        Unlocked = true;
    }

    public void SetGameConfigurations() {
        GameConfiguration.SetLevel(LevelIndex);
        MainMenu.Instance.PlayGame();
    }

    public void LevelComplete() {
        NextLevel?.Unlock();
    }

    public int LevelIndex {
        get {
            return int.Parse(name) - 1;
        }
    }
}