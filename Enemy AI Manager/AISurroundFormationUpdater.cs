using UnityEngine;

public class AISurroundFormationUpdater : MonoBehaviour {
    [SerializeField] float posDiffToUpdate;

    AISurroundFormationMaker formationMaker;
    AISurroundFormationTasker formationTasker;

    Transform player;
    Vector3 lastPosition;
    Vector3 initialPlayerPos;

#region unity methods
    void Start() {
        formationMaker = GetComponent<AISurroundFormationMaker>();
        formationTasker = GetComponent<AISurroundFormationTasker>();

        player = GameObject.FindWithTag("Player").transform;
        initialPlayerPos = player.position;
    }

    void Update() {
        Vector3 playerPos = player.position;

        if (Vector3.Distance(playerPos, lastPosition) > posDiffToUpdate) {
            formationTasker.TaskAllEnemies();
            lastPosition = playerPos;
        }
    }
#endregion

#region public methods
    public void UpdatePosition(EnemyRange type, int index) {
        Vector3 posDiff = GetPositionDifferenceToInitialPos(player.position);
        formationMaker.UpdatePosition(posDiff, type, index);
    }
#endregion

    void UpdateAllPositions(Vector3 posDiff) {
        formationMaker.UpdateAllPositions(posDiff);
    }

    Vector3 GetPositionDifferenceToInitialPos(Vector3 newPosition) {
        return newPosition - initialPlayerPos;
    }
}