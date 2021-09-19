using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MapConfiguration")]
public class MapConfiguration : ScriptableObject {
    public MapConfiguration NextMap;
    // public int XIncrement;
    public bool Unlocked;

    [SerializeField] EnemiesToKillConfiguration enemiesToKillConfiguration;
    public int MaxInGameCount {get{return enemiesToKillConfiguration.MaxInGameCount;}}
    public int EnemiesToKillCount {get{return enemiesToKillConfiguration.EnemiesToKillCount;}}

    public FloorConfig FloorConfiguration;

    public List<EnemyName> GetRandomizedEnemyNames() {
        List<EnemyName> enemyNames = new List<EnemyName>();
        foreach (var entry in enemiesToKillConfiguration.EnemiesToSpawnConfig.EnemiesToKill) {
            for (int i = 0; i < entry.Value; i++) {
                enemyNames.Add(entry.Key);
            }
        }
        enemyNames = Utility.Shuffle(enemyNames);
        return enemyNames;
    }
    
    public void Unlock() {
        Unlocked = true;
    }

    public void UnlockNextMap() {
        NextMap?.Unlock();
    }
}

[System.Serializable]
public enum FloorType {
    Default,

    Spikes1,
    Spikes2,
    Spikes3,

    Flames1,
    Flames2,
    Flames3,

    Lasers1,
    Lasers2,
    Lasers3,

    Turrets1,
    Turrets2,
    Turrets3,

    SpikesxFlames1,
    SpikesxFlames2,
    SpikesxFlames3,

    SpikesxLasers1,
    SpikesxLasers2,
    SpikesxLasers3,

    SpikesxTurrets1,
    SpikesxTurrets2,
    SpikesxTurrets3,

    FlamesxLasers1,
    FlamesxLasers2,
    FlamesxLasers3,

    FlamesxTurrets1,
    FlamesxTurrets2,
    FlamesxTurrets3,

    LasersxTurrets1,
    LasersxTurrets2,
    LasersxTurrets3,
}