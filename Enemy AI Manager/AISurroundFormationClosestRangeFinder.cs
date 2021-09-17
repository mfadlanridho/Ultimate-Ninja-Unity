using UnityEngine;
using System.Collections.Generic;

public class AISurroundFormationClosestRangeFinder : MonoBehaviour {
    AISurroundFormationMaker formationMaker;
    Transform player;

    private void Start() {
        player = GameObject.FindWithTag("Player").transform;
        formationMaker = GetComponent<AISurroundFormationMaker>();
    }

    public int GetClosestAvailableIndex(Vector3 position, EnemyRange type) {
        float angleToPlayer = GetAngleIn360(Vector3.forward, position - player.position);
        float floatIndex = GetFloatIndexFromAngle(angleToPlayer, type);
        int index = GetIndexFromFloatIndex(floatIndex, type);

        bool goRight = floatIndex < formationMaker.AIPositions[type].Positions.Capacity + .5f ? floatIndex > index : false;

        index = FindAvailableIndex(index, type, floatIndex > index);
        return index;
    }

    float GetAngleIn360(Vector3 from, Vector3 to) {
        float signedAngle = Vector3.SignedAngle(from, to, Vector3.up);
        return signedAngle >= 0 ? signedAngle : signedAngle + 360;
    }

    bool IsOccupied(int index, EnemyRange type) {
        return formationMaker.AIPositions[type].OccupiedIndexes.Contains(index);
    }

    int GetIndexFromFloatIndex(float floatAngle, EnemyRange type) {
        int indexInPositions = Mathf.RoundToInt(floatAngle);
        return indexInPositions < formationMaker.AIPositions[type].Positions.Count ? indexInPositions : indexInPositions - 1;
    }

    float GetFloatIndexFromAngle(float angle, EnemyRange type) {
        return angle / formationMaker.AIPositions[type].RotationDifference;
    }

    int FindAvailableIndex(int index, EnemyRange type, bool goRight) {
        int totalIndexesChecked = 0;
        Queue<int> queue = new Queue<int>();
        while ( IsOccupied(index, type) ) {
            int positionsCount = formationMaker.AIPositions[type].Positions.Count;
            if (totalIndexesChecked >= positionsCount)    
                return -1;

            int rIndex = (index + 1) % positionsCount;
            int lIndex = index - 1 >= 0 ?  index - 1 : positionsCount - 1;

            queue.Enqueue( goRight ? rIndex : lIndex );
            queue.Enqueue( goRight ? lIndex : rIndex);
            index = queue.Dequeue();
            totalIndexesChecked++;
        }
        return index;
    }
}