using UnityEngine;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using SegmentPoolingSystem;

public class EnemySpawnManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI killCountText;
    [SerializeField] Sound UIActivationSound;
    [SerializeField] Sound enemyDeathSound;

    EnemyDetector enemyDetector;

    int totalSpawnCount;

    int curFloorMaxInGameCount;
    int curFloorEnemiesSpawned;
    int curFloorKillCount;

    int toKillCount;
    List<EnemyName> enemiesToKill;

    int enemySpawnDictionaryIndex {
        get {return Mathf.Min(Mathf.FloorToInt((GameConfiguration.LevelIndex+2)/ 3 ), EnemiesToKillArray.Length - 1);}
    }

    int enemiesToKillCount { 
        get { return 2 * Mathf.FloorToInt((GameConfiguration.LevelIndex)/ 10) + 3;}
    }

    int maxInGameCount {
        get {return Mathf.Min(Mathf.RoundToInt((GameConfiguration.LevelIndex+1)/ 10) + 2, 4);}
    }

    private void Awake() {
        Dictionary<EnemyName, int> EnemiesToSpawn = EnemiesToKillArray[enemySpawnDictionaryIndex];
        enemiesToKill = new List<EnemyName>();
        foreach (var entry in EnemiesToSpawn) {
            for (int i = 0; i < entry.Value; i++) {
                enemiesToKill.Add(entry.Key);
            }
        }
        enemiesToKill = Utility.Shuffle(enemiesToKill);
    }

    private void Start() {
        enemyDetector = FindObjectOfType<EnemyDetector>();
        enemyDetector.OnAfterRemoveEnemy += IncreaseKillCount;

        EnemySpawner spawner = GetComponent<EnemySpawner>();
        spawner.SpawnCheckEvent += CheckIfCanSpawn;
        spawner.EnemySpawnEvent += OnSpawningEnemy;

        FindObjectOfType<SegmentMoveHandler>().ArrivedInSegmentEvent += ArrivedInSegmentCallback;
        
        toKillCount = enemiesToKillCount;
        curFloorMaxInGameCount = maxInGameCount;

        ActivateUI();
    }

    void ActivateUI() {
        AudioManager.Instance.Play(UIActivationSound.Audio, UIActivationSound.Volume);
        killCountText.gameObject.SetActive(true);
        UpdateKillCountTextUI();
        Animate(killCountText.transform);
    }

    void DeactivateUI() {
        killCountText.gameObject.SetActive(false);
    }

    bool CheckIfCanSpawn() {
        if (enemyDetector.Enemies.Count >= curFloorMaxInGameCount) {
            return false;
        }

        if (curFloorEnemiesSpawned >= toKillCount) {
            return false;
        }

        return true;
    }

    void IncreaseKillCount() {
        curFloorKillCount++; 

        if (curFloorKillCount == toKillCount) {
            AudioManager.Instance.Play(enemyDeathSound.Audio, enemyDeathSound.Volume);
            DeactivateUI();
            GameManager.Instance.ContinueSegment();
        }
        UpdateKillCountTextUI();
    }
    
    void ArrivedInSegmentCallback() {
        curFloorKillCount = 0;
        curFloorEnemiesSpawned = 0;
        ActivateUI();
    }

    EnemyName OnSpawningEnemy() {
        EnemyName enemyToSpawn = enemiesToKill[totalSpawnCount % enemiesToKill.Count];

        curFloorEnemiesSpawned++;
        totalSpawnCount++;
        return enemyToSpawn;
    }

    void UpdateKillCountTextUI() {
        if (curFloorKillCount != 0) {
            AudioManager.Instance.Play(enemyDeathSound.Audio, enemyDeathSound.Volume);
        }
        int enemiesLeft = toKillCount - curFloorKillCount;
        killCountText.text = "Enemies to kill: " + enemiesLeft;
        Animate(killCountText.transform);
    }

    void Animate(Transform transform) {
        transform.localScale = Vector3.one * 1.5f;
        transform.DOScale(1, .5f);
    }

    Dictionary<EnemyName, int>[] EnemiesToKillArray = {
        new Dictionary<EnemyName, int>() {{EnemyName.Sword1, 1}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword1, 3}, {EnemyName.Rifle1, 2}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword1, 3}, {EnemyName.Rifle1, 2}, {EnemyName.Longsword1, 2}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword1, 2}, {EnemyName.Rifle1, 2}, {EnemyName.Longsword1, 3}, {EnemyName.Minigun1, 1}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword1, 3}, {EnemyName.Rifle1, 3}, {EnemyName.Longsword1, 3}, {EnemyName.Minigun1, 2}, {EnemyName.GreatSword1, 1}},

        new Dictionary<EnemyName, int>() {{EnemyName.Sword2, 3}, {EnemyName.Rifle1, 3}, {EnemyName.Longsword1, 3}, {EnemyName.Minigun1, 2}, {EnemyName.GreatSword1, 1}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword2, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword1, 3}, {EnemyName.Minigun1, 2}, {EnemyName.GreatSword1, 1}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword2, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun1, 2}, {EnemyName.GreatSword1, 1}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword2, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword1, 1}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword2, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword2, 1}},

        new Dictionary<EnemyName, int>() {{EnemyName.Sword3, 3}, {EnemyName.Rifle2, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword2, 1}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword3, 3}, {EnemyName.Rifle3, 3}, {EnemyName.Longsword2, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword2, 1}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword3, 3}, {EnemyName.Rifle3, 3}, {EnemyName.Longsword3, 3}, {EnemyName.Minigun2, 2}, {EnemyName.GreatSword2, 1}},
        new Dictionary<EnemyName, int>() {{EnemyName.Sword3, 3}, {EnemyName.Rifle3, 3}, {EnemyName.Longsword3, 3}, {EnemyName.Minigun3, 2}, {EnemyName.GreatSword2, 1}},
    };
}