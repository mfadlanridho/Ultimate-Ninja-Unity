using UnityEngine;

public class EnemyTarget : MonoBehaviour {
    EnemyDetector detector;
    Transform player;

    private Transform currentTarget;
    public Transform CurrentTarget {
        get {
            if ((currentTarget == null && detector.Enemies.Count == 0) || (currentTarget != null && !currentTarget.gameObject.activeSelf)) {
                SelectAnotherEnemy();
            }
            return currentTarget;
        }
    }
    int enemyIndex;

    void Start() {
        detector = FindObjectOfType<EnemyDetector>();
        player = GameObject.FindWithTag("Player").transform;
        detector.OnFoundEnemy += SelectAnotherEnemy;
        detector.OnBeforeRemoveEnemy += OnRemoveEnemy;
    }

    void OnRemoveEnemy(Enemy enemy) {
        if (detector.Enemies.Count > 1 && enemy.transform == currentTarget) {
            SelectClosestTarget(enemy.transform);
        }
    }

    void ChangeEnemyIndex(int index) {
        enemyIndex = index % detector.Enemies.Count;
    }

    void ChangeCurrentTarget(Transform target) {
        currentTarget = target;
    }

    public void SelectAnotherEnemy() {
        if (detector.Enemies.Count <= 0) {
            return;
        }
        int newIndex = (enemyIndex + 1) % detector.Enemies.Count;
        ChangeEnemyIndex(newIndex);
        ChangeCurrentTarget(detector.Enemies[newIndex].transform);
    }

    void SelectClosestTarget(Transform deadEnemy) {
        float closest = 10000f;
        int selectedIndex = -1;
        int deadEnemyIndex = -1;

        for (int i = 0; i < detector.Enemies.Count; i++) {
            Transform enemy = detector.Enemies[i].transform;
            if (enemy == deadEnemy) {
                deadEnemyIndex = i;
                continue;
            }
            
            float distance = Vector3.SqrMagnitude(player.position - enemy.position);
            if (distance < closest) {
                closest = distance;
                selectedIndex = i;
            }
        }

        ChangeCurrentTarget(detector.Enemies[selectedIndex].transform);
        if (selectedIndex > deadEnemyIndex)
            selectedIndex--;

        ChangeEnemyIndex(selectedIndex);
    }

}