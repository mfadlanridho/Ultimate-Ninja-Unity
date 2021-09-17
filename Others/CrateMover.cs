using UnityEngine;
using DG.Tweening;

public class CrateMover : MonoBehaviour {
    bool atTarget;
    [SerializeField] Vector2 xBoundaries;
    [SerializeField] Vector2 zBoundaries;

    public System.Action AtTargetAction;

    void StopMoving(Transform mover) {
        if (atTarget) {
            return;
        }
    }
    
    public void Move(Transform mover) {
        if (atTarget) {
            return;
        }

        Vector3 posDiff = transform.position - mover.position;
        float xDiff = posDiff.x;
        float zDiff = posDiff.z;

        if (Mathf.Abs(xDiff) > Mathf.Abs(zDiff)) {
            if (xDiff > 0) {
                // Move right
                MoveHorizontally(1, mover);
            } else {
                // Move left
                MoveHorizontally(-1, mover);
            }
        } else {
            if (zDiff > 0) {
                // Move up
                MoveVertically(1, mover);
            } else {
                // Move down
                MoveVertically(-1, mover);
            }
        }
    }

    void MoveHorizontally(int x, Transform mover) {
        Vector3 targetPos = transform.localPosition + Vector3.right * x;

        if (targetPos.x < xBoundaries.x || targetPos.x > xBoundaries.y) {
            return;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, 2f * Time.deltaTime);
    }

    void MoveVertically(int z, Transform mover) {
        Vector3 targetPos = transform.localPosition + Vector3.forward * z;
        if (targetPos.z < zBoundaries.x || targetPos.z > zBoundaries.y) {
            return;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, 2f * Time.deltaTime);
    }

    public void AtTarget() {
        atTarget = true;

        if (AtTargetAction != null) {
            AtTargetAction();
        }
    }
}