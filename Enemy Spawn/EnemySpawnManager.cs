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

    private void Start() {
        enemyDetector = FindObjectOfType<EnemyDetector>();
        enemyDetector.OnAfterRemoveEnemy += IncreaseKillCount;

        EnemySpawner spawner = GetComponent<EnemySpawner>();
        spawner.SpawnCheckEvent += CheckIfCanSpawn;
        spawner.EnemySpawnEvent += OnSpawningEnemy;

        FindObjectOfType<SegmentMoveHandler>().ArrivedInSegmentEvent += ArrivedInSegmentCallback;
        
        toKillCount = MapAttributes.Instance.EnemiesToKillCount;
        curFloorMaxInGameCount = MapAttributes.Instance.MaxInGameCount;
        enemiesToKill = MapAttributes.Instance.GetRandomizedEnemyNames();

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
            GameManager.Instance.MoveToNextSegment();
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
}