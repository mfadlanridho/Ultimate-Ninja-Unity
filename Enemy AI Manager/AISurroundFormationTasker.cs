using UnityEngine;

public class AISurroundFormationTasker : MonoBehaviour {
    EnemyDetector enemyDetector;
    AISurroundFormationMaker formationMaker;
    AISurroundFormationClosestRangeFinder closestRangeFinder;
    AISurroundFormationUpdater updater;

#region unity methods
    void Start() {
        enemyDetector = FindObjectOfType<EnemyDetector>();
        formationMaker = GetComponent<AISurroundFormationMaker>();
        closestRangeFinder = GetComponent<AISurroundFormationClosestRangeFinder>();
        updater = GetComponent<AISurroundFormationUpdater>();

        enemyDetector.OnAddingEnemy += TaskEnemy;
    }
#endregion

#region public methods
    public void TaskAllEnemies() {
        ResetPositionOccupation();

        foreach (Enemy enemy in enemyDetector.Enemies) {
            TaskEnemy(enemy);
        }
    }
#endregion

    void TaskEnemy(Enemy enemy) {
        AIMoveToFormation enemyFormation = enemy.GetComponent<AIMoveToFormation>();
        if (!enemyFormation)
            return;
        
        AIPosition positions = formationMaker.AIPositions[enemy.Range];
        
        int index = closestRangeFinder.GetClosestAvailableIndex(enemy.transform.position, enemy.Range);
        
        if (index == -1)
            return;

        enemyFormation.OccupiedPositionIndex = index;
        positions.OccupiedIndexes.Add(index);

        updater.UpdatePosition(enemy.Range, index);
        enemyFormation.SetTargetPosition(positions.Positions[index].ActualPosition);
    }

    void ResetPositionOccupation() {
        foreach (var pos in formationMaker.AIPositions){
            pos.Value.OccupiedIndexes.Clear();
        }
    }
}