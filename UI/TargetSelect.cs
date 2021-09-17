using UnityEngine;

// put this code to player
public class TargetSelect : MonoBehaviour {
    // EnemyDetector enemyDetector;
    // EnemyTarget enemyTarget;
    // Transform player;
    
    // private void Start() {
    //     enemyDetector = FindObjectOfType<EnemyDetector>();
    //     enemyTarget = FindObjectOfType<EnemyTarget>();
    //     player = GameObject.FindWithTag("Player").transform;

    //     enemyDetector.OnAfterRemoveEnemy += SelectClosestTarget;
    //     enemyDetector.OnFoundEnemy += SelectClosestTarget;
    // }

    // public void SelectClosestTarget() {
    //     var indexes = FindClosestIndexes();
    //     int firstIdx = indexes.Item1;
    //     int secondIdx = indexes.Item2;

    //     if (firstIdx == enemyTarget.CurrentEnemyIndex && secondIdx != -1) {
    //         enemyTarget.ChangeEnemyIndex(secondIdx);
    //         Debug.Log("Picked second " + secondIdx);
    //     }
    //     else {
    //         enemyTarget.ChangeEnemyIndex(firstIdx);
    //         Debug.Log("Picked first " + firstIdx);
    //     }
    // }

    // (int, int) FindClosestIndexes() {
    //     float first = 1000f;
    //     float second = 1000f;

    //     int firstIdx = -1;
    //     int secondIdx = -1;

    //     for (int i = 0; i < enemyDetector.Enemies.Count; i++) {
    //         Transform enemy = enemyDetector.Enemies[i].transform;
            
    //         float distance = Vector3.SqrMagnitude(player.position - enemy.position);
    //         if (distance < first) {
    //             second = first;

    //             first = distance;
    //             firstIdx = i;
    //         }
    //         else if (distance < second) {
    //             second = distance;
    //             secondIdx = i;
    //         }
    //     }
    //     Debug.Log("Indexes : " + firstIdx + " and " + secondIdx);
    //     return (firstIdx, secondIdx);
    // }
}