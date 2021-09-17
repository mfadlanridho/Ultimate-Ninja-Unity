using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelConfiguration")]
public class LevelConfiguration : ScriptableObject {
    public static EnemyCountDictionary[] EnemiesToKillArray = {
        new EnemyCountDictionary() {{EnemyName.Sword1, 1}},
        new EnemyCountDictionary() {{EnemyName.Sword1, 3}, {EnemyName.Rifle1, 2}},
        new EnemyCountDictionary() {{EnemyName.Sword1, 3}, {EnemyName.Rifle1, 2}, {EnemyName.Longsword1, 2}},
        new EnemyCountDictionary() {{EnemyName.Sword1, 2}, {EnemyName.Rifle1, 2}, {EnemyName.Longsword1, 3}, {EnemyName.Minigun1, 1}},
        new EnemyCountDictionary() {{EnemyName.Sword1, 3}, {EnemyName.Rifle1, 3}, {EnemyName.Longsword1, 3}, {EnemyName.Minigun1, 2}, {EnemyName.GreatSword1, 1}},

        new EnemyCountDictionary() {{EnemyName.Sword2, 3}, {EnemyName.Rifle1, 3}, {EnemyName.Longsword1, 3}, {EnemyName.Minigun1, 2}, {EnemyName.GreatSword1, 1}},
        new EnemyCountDictionary() {{EnemyName.Sword2, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword1, 3}, {EnemyName.Minigun1, 2}, {EnemyName.GreatSword1, 1}},
        new EnemyCountDictionary() {{EnemyName.Sword2, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun1, 2}, {EnemyName.GreatSword1, 1}},
        new EnemyCountDictionary() {{EnemyName.Sword2, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword1, 1}},
        new EnemyCountDictionary() {{EnemyName.Sword2, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword2, 1}},

        new EnemyCountDictionary() {{EnemyName.Sword3, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword2, 1}},
        new EnemyCountDictionary() {{EnemyName.Sword3, 3}, {EnemyName.Rifle3, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword2, 1}},
        new EnemyCountDictionary() {{EnemyName.Sword3, 3}, {EnemyName.Rifle3, 3}, {EnemyName.Longsword3, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword2, 1}},
        new EnemyCountDictionary() {{EnemyName.Sword3, 3}, {EnemyName.Rifle3, 3}, {EnemyName.Longsword3, 3}, {EnemyName.Minigun3, 2}, {EnemyName.GreatSword2, 1}},
    };

    public static EnemiesToKillConfig[] EnemiesToKillConfigArray = {
        new EnemiesToKillConfig(3, 3), 

        new EnemiesToKillConfig(5, 3), 
        new EnemiesToKillConfig(5, 5), 

        new EnemiesToKillConfig(7, 5), 
        new EnemiesToKillConfig(7, 7), 

        new EnemiesToKillConfig(9, 7), 
        new EnemiesToKillConfig(9, 9), 

        new EnemiesToKillConfig(11, 9),
        new EnemiesToKillConfig(11, 11),

        new EnemiesToKillConfig(13, 11),
        new EnemiesToKillConfig(13, 13),

        new EnemiesToKillConfig(15, 13),
        new EnemiesToKillConfig(15, 15),

        new EnemiesToKillConfig(17, 15),
        new EnemiesToKillConfig(17, 17),

        new EnemiesToKillConfig(19, 17),
        new EnemiesToKillConfig(19, 19),

        new EnemiesToKillConfig(21, 19),
        new EnemiesToKillConfig(21, 21),

        new EnemiesToKillConfig(23, 21),
        new EnemiesToKillConfig(23, 23),

        new EnemiesToKillConfig(25, 23),
        new EnemiesToKillConfig(25, 25),
    };

    public LevelConfig[] LevelConfigurations = 
    {
        new LevelConfig(EnemiesToKillConfigArray[0], EnemiesToKillArray[0], 3, 1),
        new LevelConfig(EnemiesToKillConfigArray[0], EnemiesToKillArray[1], 3, 2),
        new LevelConfig(EnemiesToKillConfigArray[1], EnemiesToKillArray[1], 3, 2),
        new LevelConfig(EnemiesToKillConfigArray[1], EnemiesToKillArray[2], 3, 3),
        new LevelConfig(EnemiesToKillConfigArray[2], EnemiesToKillArray[2], 3, 3),

        new LevelConfig(EnemiesToKillConfigArray[2], EnemiesToKillArray[3], 5, 4),
        new LevelConfig(EnemiesToKillConfigArray[3], EnemiesToKillArray[3], 5, 4),
        new LevelConfig(EnemiesToKillConfigArray[3], EnemiesToKillArray[4], 5, 5),
        new LevelConfig(EnemiesToKillConfigArray[4], EnemiesToKillArray[4], 5, 5),
        new LevelConfig(EnemiesToKillConfigArray[4], EnemiesToKillArray[5], 5, 6),

        new LevelConfig(EnemiesToKillConfigArray[5], EnemiesToKillArray[3], 7, 6),
        new LevelConfig(EnemiesToKillConfigArray[5], EnemiesToKillArray[3], 7, 7),
        new LevelConfig(EnemiesToKillConfigArray[6], EnemiesToKillArray[4], 7, 7),
        new LevelConfig(EnemiesToKillConfigArray[6], EnemiesToKillArray[4], 7, 8),
        new LevelConfig(EnemiesToKillConfigArray[7], EnemiesToKillArray[5], 7, 8),
    };
}

[System.Serializable]
public class LevelConfig {
    public LevelConfig(EnemiesToKillConfig EnemiesToKillConfig, EnemyCountDictionary EnemiesToKill, int FloorCount, int AvailableFloorCount) {
        this.EnemiesToKillConfig = EnemiesToKillConfig;
        this.EnemiesToKill = EnemiesToKill;
        this.FloorCount = FloorCount;
        this.AvailableFloorCount = AvailableFloorCount;
    }

    public EnemiesToKillConfig EnemiesToKillConfig;
    public EnemyCountDictionary EnemiesToKill;

    public int FloorCount;
    public int AvailableFloorCount;

    public List<EnemyName> GetRandomizedEnemyNames() {
        List<EnemyName> enemyNames = new List<EnemyName>();
        foreach (var entry in EnemiesToKill) {
            for (int i = 0; i < entry.Value; i++) {
                enemyNames.Add(entry.Key);
            }
        }
        enemyNames = Utility.Shuffle(enemyNames);
        return enemyNames;
    }
}

public class EnemiesToKillConfig {
    public EnemiesToKillConfig(int EnemiesToKillCount, int MaxInGameCount) {
        this.EnemiesToKillCount = EnemiesToKillCount;
        this.MaxInGameCount = MaxInGameCount;
    }

    public EnemiesToSpawnConfig EnemiesToSpawnConfig;
    public int EnemiesToKillCount;
    public int MaxInGameCount;
}

public class FloorConfig {
    public FloorConfig(int FloorCount, int AvailableFloorCount) {
        this.AvailableFloorCount = AvailableFloorCount;
        this.FloorCount = FloorCount;
    }

    public int AvailableFloorCount;
    public int FloorCount;
}