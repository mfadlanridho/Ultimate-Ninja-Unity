using UnityEngine;

public class EnemyStepTowardsPlayerEvent : MonoBehaviour {
    [SerializeField] float speed = 10f;
    float attackDistToPlayer = 1.25f;
    
    Transform player;
    bool steppingFwd;

    Vector3 targetPos;

    CollisionDetection collisionDetection;
    private void Start() {
        player = GameObject.FindWithTag("Player").transform;
        collisionDetection = GetComponent<CollisionDetection>();
    }

    private void Update() {
        if (steppingFwd) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    void StartStepFwd() {
        if (collisionDetection.Colliding) {
            return;
        }
        targetPos = GetTargetPosition();
        steppingFwd = true;
    }

    void StopStepFwd() {
        steppingFwd = false;
    }

    Vector3 GetTargetPosition() {
        Vector3 targetPos = player.position;
        Vector3 path = player.position - transform.position;

        if (Vector3.SqrMagnitude(path) < 1) {
            return transform.position;
        }

        targetPos -= path.normalized * attackDistToPlayer;
        return targetPos;
    }

    public void MakeSureStepReset() {
        steppingFwd = false;
    }
}