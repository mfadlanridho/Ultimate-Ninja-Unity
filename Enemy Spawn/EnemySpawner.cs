using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] Vector2Int timePerSpawn;
    [SerializeField] float xSpawnOffsetToCamera;
    [SerializeField] Vector2 zPosition;

    [Space]
    [SerializeField] EnemySpawnDictionary enemiesDictionary;
    public IDictionary<EnemyName, EnemySpawnConfiguration> EnemiesDictionary
	{
		get { return enemiesDictionary; }
		set { enemiesDictionary.CopyFrom (value); }
	}

    Transform cam;
    float countdown;

    EnemyDeath enemyDeath;
    EnemyDetector enemyDetector;

    #region events
    public delegate bool SpawnCheckAction();
    public SpawnCheckAction SpawnCheckEvent;

    public delegate EnemyName EnemySpawnAction();
    public EnemySpawnAction EnemySpawnEvent;
    #endregion

    private void Start() {
        enemyDeath = FindObjectOfType<EnemyDeath>();
        enemyDetector = FindObjectOfType<EnemyDetector>();

        cam = Camera.main.transform;
    }

    private void Update() {        
        if (countdown <= 0) {
            if (SpawnCheckEvent != null && !SpawnCheckEvent())
                return;
            
            Vector3 position = GetSpawnPosition();
            if (EnemySpawnEvent == null) {
                #if UNITY_EDITOR
                Debug.LogError("Spawner doesnt know what to spawn");
                #endif
            }
            EnemyName enemyToSpawn = EnemySpawnEvent();
            enemiesDictionary[enemyToSpawn].SpawnEnemy(position, enemyDeath, enemyDetector);

            // reset countdown
            countdown = Random.Range(timePerSpawn.x, timePerSpawn.y);
        } else {
            countdown -= Time.deltaTime;
        }
    }

    Vector3 GetSpawnPosition() {
        Vector3 position = default;

        if (Random.Range(0, 2) == 0) {
            position = Vector3.right * (cam.position.x - xSpawnOffsetToCamera);
        } else {
            position = Vector3.right * (cam.position.x + xSpawnOffsetToCamera);
        }

        float zPos = Random.Range(zPosition.x, zPosition.y);
        position += Vector3.forward * zPos;

        return position;
    }

    [System.Serializable]
    public class EnemySpawnConfiguration {
        [SerializeField] GameObject prefab;
        [SerializeField] List<Enemy> Pool = new List<Enemy>();

        public void SpawnEnemy(Vector3 position, EnemyDeath enemyDeath, EnemyDetector enemyDetector) {
            foreach (Enemy enemy in Pool) {
                if (!enemy.InGame) {
                    enemy.GetInGame(position);
                    return;
                }
            }

            // instantiate enemy if every enemy is in game
            Enemy newEnemy = InstantiateFromPrefab(enemyDeath, enemyDetector);
            PrepareAndGetInGame(newEnemy, position);
        }

        Enemy InstantiateFromPrefab(EnemyDeath enemyDeath, EnemyDetector enemyDetector) {
            Enemy enemy = Instantiate(prefab).GetComponent<Enemy>();
            enemy.enemyDeath = enemyDeath;
            enemy.enemyDetector = enemyDetector;

            return enemy;
        }

        void PrepareAndGetInGame(Enemy enemy, Vector3 position) {
            Pool.Add(enemy);
            enemy.GetInGame(position);
        }

        // for finding enemies in resources
        public void SetPrefab(GameObject prefab) {
            this.prefab = prefab;
        }
    }

#if UNITY_EDITOR
    public void FindEnemiesInResources() {
        EnemiesDictionary.Clear();
        Enemy[] enemies = Resources.LoadAll("Enemies", typeof(Enemy)).Cast<Enemy>().ToArray();

        for (int i = 0; i < enemies.Length; i++) {
            Enemy enemy = enemies[i];
            EnemiesDictionary[enemy.EnemyName] = new EnemySpawnConfiguration();
            EnemiesDictionary[enemy.EnemyName].SetPrefab(enemy.gameObject);
        }
    }
#endif
}