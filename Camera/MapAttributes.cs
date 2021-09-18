using UnityEngine;
using System.Collections.Generic;

public class MapAttributes : MonoBehaviour {    
    public AvailableFloorConfiguration AvailableFloorConfiguration;

    # region Singleton
    public static MapAttributes Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    // public bool IsNightTime;
    
    // public int EnemiesToKillCount;
    // public int MaxInGameCount;

    // public Level Level;
    // public void SetLevel(Level level) {
    //     this.Level = level;
    //     int levelIndex = level.LevelIndex;

    //     MaxInGameCount = MapData.GetMaxInGameCount(levelIndex);
    //     EnemiesToKillCount = MapData.GetEnemiesToKillCount(levelIndex);
    // }

    // public void MapIsComplete() {
    //     Level.LevelComplete();
    // }

    // public bool LatestMap {
    //     get{
    //         return Level.LevelIndex == PlayerStats.Instance.UnlockedLevelCount - 1;
    //     }
    // }
}