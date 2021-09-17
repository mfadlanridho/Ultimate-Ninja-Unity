using UnityEngine;
using System.Collections.Generic;

public class AISurroundFormationMaker : MonoBehaviour {
    [SerializeField] bool displayPositions;
    [SerializeField] LayerMask edgeLayer;

    Transform player;

    [Space]
    public RangePositionsDictionary AIPositions;

#region unity methods
    void Start() {
        player = GameObject.FindWithTag("Player").transform;
        FindPositions();
    }
    
#if UNITY_EDITOR
    void OnDrawGizmos() {
        Color color = Color.gray;
        if (displayPositions) {
            foreach (var pos in AIPositions) {
                switch(pos.Key) {
                    case EnemyRange.Melee:
                        color = Color.red;
                        break;
                    case EnemyRange.Ranged:
                        color = Color.green;
                        break;
                }

                foreach (int index in pos.Value.OccupiedIndexes) {
                    Gizmos.DrawCube(pos.Value.Positions[index].ActualPosition + Vector3.up, Vector3.one / 2f);
                }
            }
        }
    }
#endif
#endregion

#region public methods
    public void UpdatePosition(Vector3 posDiffToInitialPos, EnemyRange type, int index) {
        AIPositions[type].Positions[index].SetPosition(AIPositions[type].Positions[index].InitialPosition + posDiffToInitialPos);
    }

    // very heavy, use this for debug only
    public void UpdateAllPositions(Vector3 posDiffToInitialPos) {
        foreach (var pos in AIPositions) {
            foreach (Position position in pos.Value.Positions) {
                position.SetPosition(position.InitialPosition + posDiffToInitialPos);
            }
        }
    }
#endregion
    void FindPositions() {
        ClearPositions();

        foreach (var pos in AIPositions) {
            pos.Value.RotationDifference = GetRotationDifference(pos.Value.InitialPositionCount);
            for (float angle = 0; angle < 361f; angle += pos.Value.RotationDifference) {
                Vector3 position = AngleToVector3(angle, pos.Key);
                pos.Value.Positions.Add( new Position(position));
            }
        }
    }

    float GetRotationDifference(int count) {
        return 360f / (float) count;
    }

    Vector3 AngleToVector3(float targetRot, EnemyRange type) {
        Vector3 forwardPos = Vector3.forward * AIPositions[type].Config.WaitDist;
        Vector3 targetPos = player.position + Quaternion.Euler(0, targetRot, 0) * forwardPos;
        return targetPos;
    }

    void ClearPositions() {
        foreach (var pos in AIPositions) {
            pos.Value.OccupiedIndexes.Clear();
            pos.Value.Positions.Clear();
        }
    }
}

[System.Serializable] 
public class AIPosition {
    public List<Position> Positions = new List<Position>();
    public EnemyAIConfig Config;
    public int InitialPositionCount;
    public HashSet<int> OccupiedIndexes = new HashSet<int>();
    public float RotationDifference;
}

public class Position {
    readonly Vector3 initialPosition;
    Vector3 actualPosition;

    public Vector3 ActualPosition {
        get {
            return actualPosition;
        }
    }

    public Vector3 InitialPosition {
        get {
            return initialPosition;
        }
    }

    public Position(Vector3 initialPosition) {
        this.initialPosition = initialPosition;
        actualPosition = initialPosition;
    }

    public void SetPosition(Vector3 actualPosition) {
        this.actualPosition = actualPosition;
    }
}