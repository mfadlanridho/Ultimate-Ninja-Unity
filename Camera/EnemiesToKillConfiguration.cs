using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesToKillConfiguration")]
public class EnemiesToKillConfiguration : ScriptableObject {
    public int EnemiesToKillCount;
    public int MaxInGameCount;
    public EnemiesToSpawnConfig EnemiesToSpawnConfig;
}