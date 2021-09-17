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
    
    public int XIncrement = 20;
    public int EnemiesToKillCount;
    public int MaxInGameCount;
    public int SpawnableFloorTypeCount;
    public int FloorCountToBeSpawned;
    public EnemyCountDictionary EnemiesToSpawn;

    public List<EnemyName> GetRandomizedEnemyNames() {
        List<EnemyName> enemyNames = new List<EnemyName>();
        foreach (var entry in EnemiesToSpawn) {
            for (int i = 0; i < entry.Value; i++) {
                enemyNames.Add(entry.Key);
            }
        }
        enemyNames = Utility.Shuffle(enemyNames);
        return enemyNames;
    }

    public Level Level;
    public void SetLevel(Level level) {
        this.Level = level;
        int levelIndex = level.LevelIndex;

        FloorCountToBeSpawned = MapData.GetFloorCountToBeSpawned(levelIndex);
        SpawnableFloorTypeCount =  MapData.GetSpawnableFloorTypeCount(levelIndex);
        EnemiesToSpawn = MapData.EnemiesToKillArray[MapData.GetEnemySpawnDictionaryIndex(levelIndex)];
        MaxInGameCount = MapData.GetMaxInGameCount(levelIndex);
        EnemiesToKillCount = MapData.GetEnemiesToKillCount(levelIndex);

        // IsNightTime = (levelIndex + 1) % 2 == 1;

// #if UNITY_EDITOR
//         Debug.Log(FloorCountToBeSpawned);
//         Debug.Log(SpawnableFloorTypeCount);
//         Debug.Log(EnemiesToSpawn.Values);
//         Debug.Log(MaxInGameCount);
//         Debug.Log(EnemiesToKillCount);
//         Debug.Log(IsNightTime);
// #endif
    }

    public void MapIsComplete() {
        Level.LevelComplete();
    }

    public bool LatestMap {
        get{
            return Level.LevelIndex == PlayerStats.Instance.UnlockedLevelCount - 1;
        }
    }
}